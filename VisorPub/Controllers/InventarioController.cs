using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADPostgres;
using VisorPub.Models.Publicacion;
using Newtonsoft.Json;
using EPostgres;
using EPostgres.Helper;
using LNPostgres;
using VisorPub.Models;
using Seguridad.filters;
using System.Data;
using System.IO;
using ExcelDataReader;
using SendEmail;
using System.Threading.Tasks;
using VisorPub.Models.Inventario;

namespace VisorPub.Controllers
{
    public class InventarioController : Controller
    {
        // GET: Inventario
        public ActionResult MisInventariosVegetacion()
        {
            return View();
        }

        public ActionResult RevisionInventariosVegetacion()
        {
            ViewBag.nTipoForm = (int)FormTipoInventario.RevisionInventarios;
            return View("MisInventariosVegetacion");
        }

        public string ListarMisInventariosVegetacion(int nInvEst, int nTipoForm, int nPage = 1, int nSize = 10, string cNombreProy = "", string cAno = "")
        {
            InventarioVegetacionLN oInvVeg = new InventarioVegetacionLN();
            Usuario oUsuario = new Usuario();
            oUsuario = (Usuario)Session["Datos"]; //Agregar verificacíon de sessión
            var nUsuario = oUsuario.nUsuarioId;
            // Si es supervisor no filtra por usuario
            if (oUsuario.nRolId == (int)RolId.Supervisor && nTipoForm == (int)FormTipoInventario.RevisionInventarios)
            {
                nUsuario = 0;
            }

            ListaPaginada oLista = oInvVeg.ListarMisInventariosVegetacion(nInvEst, nPage, nSize, cNombreProy, cAno, nUsuario);

            return JsonConvert.SerializeObject(oLista, Formatting.None,
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
        }

        public string CargaDatosInventarioVegetacion(int nVegId)
        {
            InventarioVegetacionLN oInvVegLN = new InventarioVegetacionLN();
            InventarioVegetacion oInvVeg = new InventarioVegetacion();

            if(nVegId != 0)
                oInvVeg = oInvVegLN.CargaDatosInventarioVegetacion(nVegId);

            return JsonConvert.SerializeObject(oInvVeg, Formatting.None,
                 new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
        }

        public int InsertaActualizaInventarioVegetacionParcela(VegetacionParcela oVegPar)
        {
            int id;
            VegetacionParcelaLN oVegParLN = new VegetacionParcelaLN();

            if (oVegPar.nParcelaId == 0)
            {
                id = oVegParLN.RegistraInventarioVegetacionParcela(oVegPar);
            }
            else
            {
                id = oVegParLN.ActualizaInventarioVegetacionParcela(oVegPar);
            }

            return id;
        }

        public int EliminaInventarioVegetacionParcelas(VegetacionParcela oVegPar)
        {
            int id;
            VegetacionParcelaLN oVegParLN = new VegetacionParcelaLN();

            id = oVegParLN.EliminaInventarioVegetacionParcela(oVegPar);

            return id;
        }

        public int GuardarInventarioVegetacion(InventarioVegetacion oInvVeg)
        {
            int id;
            InventarioVegetacionLN oInvVegLN = new InventarioVegetacionLN();
            Usuario oUsuario = new Usuario();
            oUsuario = (Usuario)Session["Datos"];

            oInvVeg.nUsuarioId = oUsuario.nUsuarioId;

            if (oInvVeg.nVegetacionId == 0)
            {
                id = oInvVegLN.RegistraInventarioVegetacion(oInvVeg);
            }
            else
            {
                id = oInvVegLN.ActualizaInventarioVegetacion(oInvVeg);
            }

            return id;
        }

        public int EliminaInventarioVegetacion(InventarioVegetacion oInvVeg)
        {
            int id;
            InventarioVegetacionLN oInvVegLN = new InventarioVegetacionLN();

            id = oInvVegLN.EliminaInventarioVegetacion(oInvVeg);

            return id;
        }

        public async Task<JsonResult> EnviarSolicitudInventarioVegetacion(InventarioVegetacion oInvVeg)
        {
            InventarioVegetacionLN oInvVegLN = new InventarioVegetacionLN();

            //Actualiza estado inventario de Vegetacion
            var nAct = oInvVegLN.ActualizaEstadoInventarioVegetacion(oInvVeg.nVegetacionId, (int) EstadoSolicitud.Solicitado);

            if (oInvVeg.nVegetacionId != 0)
            {
                //Codificar url historial
                HistorialLN oHistorialLN = new HistorialLN();
                Historial oHistorial = new Historial
                {
                    nRefId = oInvVeg.nVegetacionId,
                    nTipoReferencia = (int)TipoReferencia.InventarioVegetaciones,
                    nEstado = 1
                };

                var nHistId = oHistorialLN.CrearHistorial(oHistorial);

                Usuario oUsuarioReg = ((Usuario)Session["Datos"]);

                // Registra historial - Solicitud Registrada
                RegistrarHistorial(nHistId, oUsuarioReg.nUsuarioId, EstadoSolicitud.Solicitado);

                //Obtener Unique Historial
                var cHistUniqueId = oHistorialLN.GetRecordUniqueIdByReferenciaId(oInvVeg.nVegetacionId, TipoReferencia.InventarioVegetaciones);

                string cHistorialUrl = Url.Action("InventarioVegetaciones", "Historial", null, protocol: Request.Url.Scheme) + "/";

                //Enviar correo usuario registra
                await GmailClient.SendEmailAsync(oUsuarioReg.cEmail, $"Solicitud Inventario de Vegetación N° {oInvVeg.nVegetacionId} - Nueva Solicitud | VISOR IIAP", "<p>Estimado/a " + oUsuarioReg.cNombres + $"</p><p> Se ha registrado su solicitud de Inventario de Vegetación N° {oInvVeg.nVegetacionId} y estará en proceso de evaluación. Muchas Gracias </p><a href='{cHistorialUrl}" + cHistUniqueId + "' target='_blank'> Revisar Historial </a>", "");

                //Enviar alerta a los supervisores
                RolLN oRol = new RolLN();
                var oSupervisores = oRol.CargarUsuariosPorRol(RolId.Supervisor);

                if (oSupervisores.Count() > 0)
                {
                    foreach (Usuario u in oSupervisores)
                    {
                        RegistrarAlerta(u.nUsuarioId, $"Solicitud Inventario de Vegetación N° {oInvVeg.nVegetacionId} - Nueva Solicitud", "Se ha registrado una nueva solicitud para su revisión.", $"/Inventario/RevisionInventariosVegetacion/{oInvVeg.nVegetacionId}", AlertIcon.Solicitado, AlertColor.Solicitado);
                    }

                    //Enviar correo usuarios perfil supervisor
                    var cSupervisorEmails = String.Join(", ", oSupervisores.Select(u => u.cEmail).ToArray());
                    await GmailClient.SendEmailAsync(cSupervisorEmails, $"Solicitud Inventario de Vegetación N° {oInvVeg.nVegetacionId} - Nueva Solicitud | VISOR IIAP", "<p> Se ha registrado una nueva solicitud de Publicación con código  " + oInvVeg.nVegetacionId + $", favor de ingresar a la intranet para proceder con su validación. </p><a href='{cHistorialUrl}" + cHistUniqueId + "' target='_blank'> Revisar Historial </a>", "");
                }

            }

            return Json(JsonConvert.SerializeObject(nAct));
        }

        public async Task<JsonResult> EnviarCorrecionInventarioVegetacion(InventarioVegetacion oInvVeg)
        {
            InventarioVegetacionLN oInvVegLN = new InventarioVegetacionLN();
            var cHistUniqueId = oInvVeg.oHist.cUniqueId;

            //Actualiza estado inventario de Vegetacion
            var nAct = oInvVegLN.ActualizaEstadoInventarioVegetacion(oInvVeg.nVegetacionId, (int)EstadoSolicitud.Solicitado);

            if (oInvVeg.nVegetacionId != 0)
            {
                //Obtener datos usuario de la publicación
                Usuario oUsuarioReg = ((Usuario)Session["Datos"]);

                // Registra historial - Solicitud Registrada
                RegistrarHistorial(oInvVeg.oHist.nHistorialId, oUsuarioReg.nUsuarioId, EstadoSolicitud.Solicitado);

                string cHistorialUrl = Url.Action("InventarioVegetaciones", "Historial", null, protocol: Request.Url.Scheme) + "/";

                //Enviar correo usuario registra
                await GmailClient.SendEmailAsync(oUsuarioReg.cEmail, $"Solicitud Inventario de Vegetación N° {oInvVeg.nVegetacionId} - Corregida | VISOR IIAP", "<p>Estimado/a " + oUsuarioReg.cNombres + $"</p><p> Se ha registrado su solicitud de Inventario de Vegetación N° {oInvVeg.nVegetacionId} y estará en proceso de evaluación. Muchas Gracias </p><a href='{cHistorialUrl}" + cHistUniqueId + "' target='_blank'> Revisar Historial </a>", "");

                //Enviar alerta a los supervisores
                RolLN oRol = new RolLN();
                var oSupervisores = oRol.CargarUsuariosPorRol(RolId.Supervisor);

                if (oSupervisores.Count() > 0)
                {
                    foreach (Usuario u in oSupervisores)
                    {
                        RegistrarAlerta(u.nUsuarioId, $"Solicitud Inventario de Vegetación N° {oInvVeg.nVegetacionId} - Corregida", "Se ha registrado una nueva solicitud para su revisión.", $"/Inventario/RevisionInventariosVegetacion/{oInvVeg.nVegetacionId}", AlertIcon.Solicitado, AlertColor.Solicitado);
                    }

                    //Enviar correo usuarios perfil supervisor
                    var cSupervisorEmails = String.Join(", ", oSupervisores.Select(u => u.cEmail).ToArray());
                    await GmailClient.SendEmailAsync(cSupervisorEmails, $"Solicitud Inventario de Vegetación N° {oInvVeg.nVegetacionId} - Corregida | VISOR IIAP", "<p> Se ha registrado una nueva solicitud de Publicación con código  " + oInvVeg.nVegetacionId + $", favor de ingresar a la intranet para proceder con su validación. </p><a href='{cHistorialUrl}" + cHistUniqueId + "' target='_blank'> Revisar Historial </a>", "");
                }

            }

            return Json(JsonConvert.SerializeObject(nAct));
        }
        

        public async Task<JsonResult> RegistrarObservacionInvVeg(int nInvVegId, int nUsuId, int nHistId, string nUHistId, string cMensaje)
        {
            Usuario oUsuarioReg = ((Usuario)Session["Datos"]);

            //Cambiar estado inventario de vegetación
            InventarioVegetacionLN oInvVeg = new InventarioVegetacionLN();
            oInvVeg.ActualizaEstadoInventarioVegetacion(nInvVegId, (int)EstadoSolicitud.Observado);

            //Obtener datos usuario del inventario

            UsuarioLN oUsuarioLN = new UsuarioLN();
            var oUsuarioPub = oUsuarioLN.CargarDatosUsuario(nUsuId);

            string cHistorialUrl = Url.Action("InventarioVegetaciones", "Historial", null, protocol: Request.Url.Scheme) + "/";

            // Registra historial - Observación
            var nHisDet = RegistrarHistorial(nHistId, oUsuarioReg.nUsuarioId, EstadoSolicitud.Observado, cMensaje);

            //Registrar alerta para el usuario
            RegistrarAlerta(oUsuarioPub.nUsuarioId, $"Solicitud Inventario de Vegetación N° {nInvVegId} - Observada", "Se ha encontrado observaciones en su solicitud. Favor de revisar los detalles en el historial.", $"/Inventario/MisInventariosVegetacion/{nInvVegId}", AlertIcon.Observado, AlertColor.Observado);

            HistorialLN oHistorialLN = new HistorialLN();
            //Supervisor envia correo a usuario registro
            await GmailClient.SendEmailAsync(oUsuarioPub.cEmail, $"Solicitud Inventario de Vegetación N° {nInvVegId} - Observada | VISOR IIAP", "<p>Estimado/a " + oUsuarioPub.cNombres + "</p><p> Se ha encontrado observaciones a la solicitud realizada. Agradeceremos levantar las observaciones para proceder a su aprobación. </p>"+ $"<a href='{cHistorialUrl}" + nUHistId + "' target='_blank'> Ver Detalle </a>", "");

            return Json(JsonConvert.SerializeObject(nHisDet));
        }

        public async Task<JsonResult> RegistrarRechazoInvVeg(int nInvVegId, int nUsuId, int nHistId, string nUHistId, string cMensaje)
        {
            Usuario oUsuarioReg = ((Usuario)Session["Datos"]);

            //Cambiar estado inventario de vegetación
            InventarioVegetacionLN oInvVeg = new InventarioVegetacionLN();
            oInvVeg.ActualizaEstadoInventarioVegetacion(nInvVegId, (int)EstadoSolicitud.Rechazado);

            //Obtener datos usuario del inventario

            UsuarioLN oUsuarioLN = new UsuarioLN();
            var oUsuarioPub = oUsuarioLN.CargarDatosUsuario(nUsuId);

            // Registra historial - Rechazado
            var nHisDet = RegistrarHistorial(nHistId, oUsuarioReg.nUsuarioId, EstadoSolicitud.Rechazado, cMensaje);

            string cHistorialUrl = Url.Action("InventarioVegetaciones", "Historial", null, protocol: Request.Url.Scheme) + "/";

            //Registrar alerta para el usuario
            RegistrarAlerta(oUsuarioPub.nUsuarioId, $"Solicitud Inventario de Vegetación N° {nInvVegId} - Rechazada", "Se ha rechazado su solicitud. Por favor de revisar los comentarios en el historial.", $"/Inventario/MisInventariosVegetacion/{nInvVegId}", AlertIcon.Rechazado, AlertColor.Rechazado);

            HistorialLN oHistorialLN = new HistorialLN();
            //Supervisor envia correo a usuario registro
            await GmailClient.SendEmailAsync(oUsuarioPub.cEmail, $"Solicitud Inventario de Vegetación N° {nInvVegId} - Rechazada | VISOR IIAP", "<p>Estimado/a " + oUsuarioPub.cNombres + "</p><p>Agradecemos su colaboración, sin embargo, se ha rechazado su solicitud. Agradeceremos ponerse en contacto con nosotros de existir algún mal entendido. Muchas gracias. </p>"+ $"<a href='{cHistorialUrl}" + nUHistId + "' target='_blank'> Ver Detalle </a>", "");

            return Json(JsonConvert.SerializeObject(nHisDet));
        }

        public async Task<JsonResult> RegistrarAprobacionInvVeg(int nInvVegId, int nUsuId, int nHistId, string nUHistId)
        {
            Usuario oUsuarioReg = ((Usuario)Session["Datos"]);

            //Cambiar estado inventario de vegetación
            InventarioVegetacionLN oInvVeg = new InventarioVegetacionLN();
            oInvVeg.ActualizaEstadoInventarioVegetacion(nInvVegId, (int)EstadoSolicitud.Aprobado);

            //Obtener datos usuario de la publicación
            UsuarioLN oUsuarioLN = new UsuarioLN();
            var oUsuarioPub = oUsuarioLN.CargarDatosUsuario(nUsuId);

            // Registra historial - Aprobado
            var nHisDet = RegistrarHistorial(nHistId, oUsuarioReg.nUsuarioId, EstadoSolicitud.Aprobado);

            string cHistorialUrl = Url.Action("InventarioVegetaciones", "Historial", null, protocol: Request.Url.Scheme) + "/";

            //Registrar alerta para el usuario
            RegistrarAlerta(oUsuarioPub.nUsuarioId, $"Solicitud Inventario de Vegetación N° {nInvVegId} - ¡Aprobada!", "Se ha aprobado su solicitud. Muchas gracias por su gran trabajo.", $"/Inventario/MisInventariosVegetacion/{nInvVegId}", AlertIcon.Aprobado, AlertColor.Aprobado);

            HistorialLN oHistorialLN = new HistorialLN();
            //Supervisor envia correo a usuario registro
            await GmailClient.SendEmailAsync(oUsuarioPub.cEmail, $"Solicitud Inventario de Vegetación N° {nInvVegId} - ¡Aprobada! | VISOR IIAP", "<p>Estimado/a " + oUsuarioPub.cNombres + $"</p><p>Su solicitud de Publicación N° {nInvVegId} ha sido aprobada. Muchas gracias por su colaboración. </p>"+$"<a href='{cHistorialUrl}" + nUHistId + "' target='_blank'> Ver Detalle </a>", "");

            return Json(JsonConvert.SerializeObject(nHisDet));
        }

        // INVENTARIO DE SUELOS

        public ActionResult MisInventariosSuelos()
        {
            return View();
        }

        public ActionResult RevisionInventariosSuelos()
        {
            ViewBag.nTipoForm = (int)FormTipoInventario.RevisionInventarios;
            return View("MisInventariosSuelos");
        }

        public string ListarMisInventariosSuelos(int nInvEst, int nTipoForm, int nPage = 1, int nSize = 10, string cNombreProy = "", string cAno = "", string cNombreColector = "")
        {
            InventarioSuelosLN oInvSue = new InventarioSuelosLN();
            Usuario oUsuario = new Usuario();
            oUsuario = (Usuario)Session["Datos"]; //Agregar verificacíon de sessión

            var nUsuario = oUsuario.nUsuarioId;
            // Si es supervisor no filtra por usuario
            if (oUsuario.nRolId == (int)RolId.Supervisor && nTipoForm == (int)FormTipoInventario.RevisionInventarios)
            {
                nUsuario = 0;
            }

            ListaPaginada oLista = oInvSue.ListarMisInventariosSuelos(nInvEst, nPage, nSize, cNombreProy, cAno, cNombreColector, nUsuario);

            return JsonConvert.SerializeObject(oLista, Formatting.None,
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
        }

        public string CargaDatosInventarioSuelos(int nSueId)
        {
            InventarioSuelosLN oInvSueLN = new InventarioSuelosLN();
            InventarioSuelos oInvSue = new InventarioSuelos();

            if (nSueId != 0)
                oInvSue = oInvSueLN.CargaDatosInventarioSuelos(nSueId);

            return JsonConvert.SerializeObject(oInvSue, Formatting.None,
                 new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
        }

        public int InsertaActualizaInventarioSuelosPerfilModal(SuelosPerfilModal oSuePerMod)
        {
            int id;
            SuelosPerfilModalLN oSuePerModLN = new SuelosPerfilModalLN();

            if (oSuePerMod.nPerfilId == 0)
            {
                id = oSuePerModLN.RegistraInventarioSuelosPerfilModal(oSuePerMod);
            }
            else
            {
                id = oSuePerModLN.ActualizaInventarioSuelosPerfilModal(oSuePerMod);
            }

            return id;
        }

        public int EliminaInventarioSuelosPerfilModal(SuelosPerfilModal oSuePerMod)
        {
            int id;
            SuelosPerfilModalLN oSuePerModLN = new SuelosPerfilModalLN();

            id = oSuePerModLN.EliminaInventarioSuelosPerfilModal(oSuePerMod);

            return id;
        }

        public int GuardarInventarioSuelos(InventarioSuelos oInvSue)
        {
            int id;
            InventarioSuelosLN oInvSueLN = new InventarioSuelosLN();
            Usuario oUsuario = new Usuario();
            oUsuario = (Usuario)Session["Datos"];

            oInvSue.nUsuarioId = oUsuario.nUsuarioId;

            if (oInvSue.nSuelosId == 0)
            {
                id = oInvSueLN.RegistraInventarioSuelos(oInvSue);
            }
            else
            {
                id = oInvSueLN.ActualizaInventarioSuelos(oInvSue);
            }

            return id;
        }

        public int EliminaInventarioSuelos(InventarioSuelos oInvSue)
        {
            int id;
            InventarioSuelosLN oInvSueLN = new InventarioSuelosLN();

            id = oInvSueLN.EliminaInventarioSuelos(oInvSue);

            return id;
        }

        public async Task<JsonResult> EnviarSolicitudInventarioSuelos(InventarioSuelos oInvSue)
        {
            InventarioSuelosLN oInvVegLN = new InventarioSuelosLN();

            //Actualiza estado inventario de Vegetacion
            var nAct = oInvVegLN.ActualizaEstadoInventarioSuelos(oInvSue.nSuelosId, (int)EstadoSolicitud.Solicitado);

            if (oInvSue.nSuelosId != 0)
            {
                //Codificar url historial
                HistorialLN oHistorialLN = new HistorialLN();
                Historial oHistorial = new Historial
                {
                    nRefId = oInvSue.nSuelosId,
                    nTipoReferencia = (int)TipoReferencia.InventarioSuelos,
                    nEstado = 1
                };

                var nHistId = oHistorialLN.CrearHistorial(oHistorial);

                Usuario oUsuarioReg = ((Usuario)Session["Datos"]);

                // Registra historial - Solicitud Registrada
                RegistrarHistorial(nHistId, oUsuarioReg.nUsuarioId, EstadoSolicitud.Solicitado);

                //Obtener Unique Historial
                var cHistUniqueId = oHistorialLN.GetRecordUniqueIdByReferenciaId(oInvSue.nSuelosId, TipoReferencia.InventarioSuelos);

                string cHistorialUrl = Url.Action("InventarioSuelos", "Historial", null, protocol: Request.Url.Scheme) + "/";

                //Enviar correo usuario registra
                await GmailClient.SendEmailAsync(oUsuarioReg.cEmail, $"Solicitud Inventario de Suelos N° {oInvSue.nSuelosId} - Nueva Solicitud | VISOR IIAP", "<p>Estimado/a " + oUsuarioReg.cNombres + $"</p><p> Se ha registrado su solicitud de Inventario de Suelos N° {oInvSue.nSuelosId} y estará en proceso de evaluación. Muchas Gracias </p><a href='{cHistorialUrl}" + cHistUniqueId + "' target='_blank'> Revisar Historial </a>", "");

                //Enviar alerta a los supervisores
                RolLN oRol = new RolLN();
                var oSupervisores = oRol.CargarUsuariosPorRol(RolId.Supervisor);

                if (oSupervisores.Count() > 0)
                {
                    foreach (Usuario u in oSupervisores)
                    {
                        RegistrarAlerta(u.nUsuarioId, $"Solicitud Inventario de Suelos N° {oInvSue.nSuelosId} - Nueva Solicitud", "Se ha registrado una nueva solicitud para su revisión.", $"/Inventario/RevisionInventariosSuelos/{oInvSue.nSuelosId}", AlertIcon.Solicitado, AlertColor.Solicitado);
                    }

                    //Enviar correo usuarios perfil supervisor
                    var cSupervisorEmails = String.Join(", ", oSupervisores.Select(u => u.cEmail).ToArray());
                    await GmailClient.SendEmailAsync(cSupervisorEmails, $"Solicitud Inventario de Suelos N° {oInvSue.nSuelosId} - Nueva Solicitud | VISOR IIAP", "<p> Se ha registrado una nueva solicitud de Inventario de  Suelos con código  " + oInvSue.nSuelosId + $", favor de ingresar a la intranet para proceder con su validación. </p><a href='{cHistorialUrl}" + cHistUniqueId + "' target='_blank'> Revisar Historial </a>", "");
                }

            }

            return Json(JsonConvert.SerializeObject(nAct));
        }

        public async Task<JsonResult> EnviarCorrecionInventarioSuelos(InventarioSuelos oInvSue)
        {
            InventarioSuelosLN oInvSueLN = new InventarioSuelosLN();
            var cHistUniqueId = oInvSue.oHist.cUniqueId;

            //Actualiza estado inventario de Suelos
            var nAct = oInvSueLN.ActualizaEstadoInventarioSuelos(oInvSue.nSuelosId, (int)EstadoSolicitud.Solicitado);

            if (oInvSue.nSuelosId != 0)
            {
                //Obtener datos usuario de la publicación
                Usuario oUsuarioReg = ((Usuario)Session["Datos"]);

                // Registra historial - Solicitud Registrada
                RegistrarHistorial(oInvSue.oHist.nHistorialId, oUsuarioReg.nUsuarioId, EstadoSolicitud.Solicitado);

                string cHistorialUrl = Url.Action("InventarioSuelos", "Historial", null, protocol: Request.Url.Scheme) + "/";

                //Enviar correo usuario registra
                await GmailClient.SendEmailAsync(oUsuarioReg.cEmail, $"Solicitud Inventario de Suelos N° {oInvSue.nSuelosId} - Corregida | VISOR IIAP", "<p>Estimado/a " + oUsuarioReg.cNombres + $"</p><p> Se ha registrado su solicitud de Inventario de Suelos N° {oInvSue.nSuelosId} y estará en proceso de evaluación. Muchas Gracias </p><a href='{cHistorialUrl}" + cHistUniqueId + "' target='_blank'> Revisar Historial </a>", "");

                //Enviar alerta a los supervisores
                RolLN oRol = new RolLN();
                var oSupervisores = oRol.CargarUsuariosPorRol(RolId.Supervisor);

                if (oSupervisores.Count() > 0)
                {
                    foreach (Usuario u in oSupervisores)
                    {
                        RegistrarAlerta(u.nUsuarioId, $"Solicitud Inventario de Suelos N° {oInvSue.nSuelosId} - Corregida", "Se ha registrado una nueva solicitud para su revisión.", $"/Inventario/RevisionInventariosSuelos/{oInvSue.nSuelosId}", AlertIcon.Solicitado, AlertColor.Solicitado);
                    }

                    //Enviar correo usuarios perfil supervisor
                    var cSupervisorEmails = String.Join(", ", oSupervisores.Select(u => u.cEmail).ToArray());
                    await GmailClient.SendEmailAsync(cSupervisorEmails, $"Solicitud Inventario de Suelos N° {oInvSue.nSuelosId} - Corregida | VISOR IIAP", "<p> Se ha registrado una nueva solicitud de Inventario de Suelos con código  " + oInvSue.nSuelosId + $", favor de ingresar a la intranet para proceder con su validación. </p><a href='{cHistorialUrl}" + cHistUniqueId + "' target='_blank'> Revisar Historial </a>", "");
                }

            }

            return Json(JsonConvert.SerializeObject(nAct));
        }

        public async Task<JsonResult> RegistrarObservacionInvSue(int nInvSueId, int nUsuId, int nHistId, string nUHistId, string cMensaje)
        {
            Usuario oUsuarioReg = ((Usuario)Session["Datos"]);

            //Cambiar estado inventario de suelos
            InventarioSuelosLN oInvSue= new InventarioSuelosLN();
            oInvSue.ActualizaEstadoInventarioSuelos(nInvSueId, (int)EstadoSolicitud.Observado);

            //Obtener datos usuario del inventario

            UsuarioLN oUsuarioLN = new UsuarioLN();
            var oUsuarioPub = oUsuarioLN.CargarDatosUsuario(nUsuId);

            // Registra historial - Observación
            var nHisDet = RegistrarHistorial(nHistId, oUsuarioReg.nUsuarioId, EstadoSolicitud.Observado, cMensaje);

            string cHistorialUrl = Url.Action("InventarioSuelos", "Historial", null, protocol: Request.Url.Scheme) + "/";

            //Registrar alerta para el usuario
            RegistrarAlerta(oUsuarioPub.nUsuarioId, $"Solicitud Inventario de Suelos N° {nInvSueId} - Observada", "Se ha encontrado observaciones en su solicitud. Favor de revisar los detalles en el historial.", $"/Inventario/MisInventariosSuelos/{nInvSueId}", AlertIcon.Observado, AlertColor.Observado);

            HistorialLN oHistorialLN = new HistorialLN();
            //Supervisor envia correo a usuario registro
            await GmailClient.SendEmailAsync(oUsuarioPub.cEmail, $"Solicitud Inventario de Suelos N° {nInvSueId} - Observada | VISOR IIAP", "<p>Estimado/a " + oUsuarioPub.cNombres + "</p><p> Se ha encontrado observaciones a la solicitud realizada. Agradeceremos levantar las observaciones para proceder a su aprobación. </p>" + $"<a href='{cHistorialUrl}" + nUHistId + "' target='_blank'> Ver Detalle </a>", "");

            return Json(JsonConvert.SerializeObject(nHisDet));
        }

        public async Task<JsonResult> RegistrarRechazoInvSue(int nInvSueId, int nUsuId, int nHistId, string nUHistId, string cMensaje)
        {
            Usuario oUsuarioReg = ((Usuario)Session["Datos"]);

            //Cambiar estado inventario de suelos
            InventarioSuelosLN oInvVeg = new InventarioSuelosLN();
            oInvVeg.ActualizaEstadoInventarioSuelos(nInvSueId, (int)EstadoSolicitud.Rechazado);

            //Obtener datos usuario del inventario

            UsuarioLN oUsuarioLN = new UsuarioLN();
            var oUsuarioPub = oUsuarioLN.CargarDatosUsuario(nUsuId);

            // Registra historial - Rechazado
            var nHisDet = RegistrarHistorial(nHistId, oUsuarioReg.nUsuarioId, EstadoSolicitud.Rechazado, cMensaje);

            string cHistorialUrl = Url.Action("InventarioSuelos", "Historial", null, protocol: Request.Url.Scheme) + "/";

            //Registrar alerta para el usuario
            RegistrarAlerta(oUsuarioPub.nUsuarioId, $"Solicitud Inventario de Suelos N° {nInvSueId} - Rechazada", "Se ha rechazado su solicitud. Por favor de revisar los comentarios en el historial.", $"/Inventario/MisInventariosSuelos/{nInvSueId}", AlertIcon.Rechazado, AlertColor.Rechazado);

            HistorialLN oHistorialLN = new HistorialLN();
            //Supervisor envia correo a usuario registro
            await GmailClient.SendEmailAsync(oUsuarioPub.cEmail, $"Solicitud Inventario de Suelos N° {nInvSueId} - Rechazada | VISOR IIAP", "<p>Estimado/a " + oUsuarioPub.cNombres + "</p><p>Agradecemos su colaboración, sin embargo, se ha rechazado su solicitud. Agradeceremos ponerse en contacto con nosotros de existir algún mal entendido. Muchas gracias. </p>" + $"<a href='{cHistorialUrl}" + nUHistId + "' target='_blank'> Ver Detalle </a>", "");

            return Json(JsonConvert.SerializeObject(nHisDet));
        }

        public async Task<JsonResult> RegistrarAprobacionInvSue(int nInvSueId, int nUsuId, int nHistId, string nUHistId)
        {
            Usuario oUsuarioReg = ((Usuario)Session["Datos"]);

            //Cambiar estado inventario de vegetación
            InventarioSuelosLN oInvVeg = new InventarioSuelosLN();
            oInvVeg.ActualizaEstadoInventarioSuelos(nInvSueId, (int)EstadoSolicitud.Aprobado);

            //Obtener datos usuario de la publicación
            UsuarioLN oUsuarioLN = new UsuarioLN();
            var oUsuarioPub = oUsuarioLN.CargarDatosUsuario(nUsuId);

            // Registra historial - Aprobado
            var nHisDet = RegistrarHistorial(nHistId, oUsuarioReg.nUsuarioId, EstadoSolicitud.Aprobado);

            string cHistorialUrl = Url.Action("InventarioSuelos", "Historial", null, protocol: Request.Url.Scheme) + "/";

            //Registrar alerta para el usuario
            RegistrarAlerta(oUsuarioPub.nUsuarioId, $"Solicitud Inventario de Suelos N° {nInvSueId} - ¡Aprobada!", "Se ha aprobado su solicitud. Muchas gracias por su gran trabajo.", $"/Inventario/MisInventariosSuelos/{nInvSueId}", AlertIcon.Aprobado, AlertColor.Aprobado);

            HistorialLN oHistorialLN = new HistorialLN();
            //Supervisor envia correo a usuario registro
            await GmailClient.SendEmailAsync(oUsuarioPub.cEmail, $"Solicitud Inventario de Suelos N° {nInvSueId} - ¡Aprobada! | VISOR IIAP", "<p>Estimado/a " + oUsuarioPub.cNombres + $"</p><p>Su solicitud de Inventario de Suelos N° {nInvSueId} ha sido aprobada. Muchas gracias por su colaboración. </p>" + $"<a href='{cHistorialUrl}" + nUHistId + "' target='_blank'> Ver Detalle </a>", "");

            return Json(JsonConvert.SerializeObject(nHisDet));
        }


        public int CargarExcelVegetacion(ExcelViewModel model)
        {
            
            string filename = Guid.NewGuid() + Path.GetExtension(model.File.FileName);
            string filepath = "/ExcelInventarios/" + filename;

            model.File.SaveAs(Path.Combine(Server.MapPath("/ExcelInventarios"), filename));
            InsertExcelDataVegetacion(filepath, filename, model.Id);

            return 1;
        }

        private void InsertExcelDataVegetacion(string fileepath, string filename, int nVegetacionId)
        {
            VegetacionParcelaLN oVegParLN = new VegetacionParcelaLN();
            string fullpath = Server.MapPath("/ExcelInventarios/") + filename;

            using (var stream = System.IO.File.Open(fullpath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();
                    var dataTable = result.Tables[0];

                    for (var i = 1; i < dataTable.Rows.Count; i++)
                    {
                        var data = dataTable.Rows[i][0];

                        VegetacionParcela oVegPar = new VegetacionParcela();

                        oVegPar.nVegetacionId = nVegetacionId;
                        oVegPar.cDepartamento = dataTable.Rows[i][0].ToString();
                        oVegPar.cRegistrador = dataTable.Rows[i][1].ToString();
                        oVegPar.cLongitud = dataTable.Rows[i][2].ToString();
                        oVegPar.cLatitud = dataTable.Rows[i][3].ToString();
                        oVegPar.cCodigoMuestra = dataTable.Rows[i][4].ToString(); ;
                        oVegPar.cAltitud = dataTable.Rows[i][5].ToString(); ;
                        oVegPar.cPrecision = dataTable.Rows[i][6].ToString();
                        oVegPar.cTipoVegetacion = dataTable.Rows[i][7].ToString();
                        oVegPar.cClaseFisonomica = dataTable.Rows[i][8].ToString();
                        oVegPar.cCobertura = dataTable.Rows[i][9].ToString();
                        oVegPar.cConfianzaClasificacion = dataTable.Rows[i][10].ToString();
                        oVegPar.cClaseHidrologica = dataTable.Rows[i][11].ToString();
                        oVegPar.cFisiografia = dataTable.Rows[i][12].ToString();
                        oVegPar.cAltitudSistemaHidrico = dataTable.Rows[i][13].ToString();

                        oVegParLN.RegistraInventarioVegetacionParcela(oVegPar);
                    }
                }
            }
        }


        public int CargarExcelSuelos(ExcelViewModel model)
        {

            string filename = Guid.NewGuid() + Path.GetExtension(model.File.FileName);
            string filepath = "/ExcelInventarios/" + filename;

            model.File.SaveAs(Path.Combine(Server.MapPath("/ExcelInventarios"), filename));
            InsertExcelDataSuelos(filepath, filename, model.Id);

            return 1;
        }

        private void InsertExcelDataSuelos(string fileepath, string filename, int nSuelosId)
        {
            SuelosPerfilModalLN oSuePerModLN = new SuelosPerfilModalLN();
            string fullpath = Server.MapPath("/ExcelInventarios/") + filename;

            using (var stream = System.IO.File.Open(fullpath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();
                    var dataTable = result.Tables[0];

                    for (var i = 1; i < dataTable.Rows.Count; i++)
                    {
                        var data = dataTable.Rows[i][0];

                        SuelosPerfilModal oSuePerMod = new SuelosPerfilModal();

                        oSuePerMod.nSuelosId = nSuelosId;
                        oSuePerMod.cDepartamento = dataTable.Rows[i][0].ToString();
                        oSuePerMod.cLongitud = dataTable.Rows[i][1].ToString();
                        oSuePerMod.cLatitud = dataTable.Rows[i][2].ToString();                                                     
                        oSuePerMod.cPerfilModal = dataTable.Rows[i][3].ToString();                        
                        oSuePerMod.cNroCalicata = dataTable.Rows[i][4].ToString();                        
                        oSuePerMod.cMsnm = dataTable.Rows[i][5].ToString();                               
                        oSuePerMod.cZonaMuestreo = dataTable.Rows[i][6].ToString();                       
                        oSuePerMod.cClasificacionNatural = dataTable.Rows[i][7].ToString();               
                        oSuePerMod.cFisiografia = dataTable.Rows[i][8].ToString();                        
                        oSuePerMod.cPendiente = dataTable.Rows[i][9].ToString();                          
                        oSuePerMod.cRelieve = dataTable.Rows[i][10].ToString();                            
                        oSuePerMod.cClima = dataTable.Rows[i][11].ToString();                              
                        oSuePerMod.cZonaVida = dataTable.Rows[i][12].ToString();                           
                        oSuePerMod.cMaterialParental = dataTable.Rows[i][13].ToString();                   
                        oSuePerMod.cVegetacion = dataTable.Rows[i][14].ToString();                         
                        oSuePerMod.cModoColecta = dataTable.Rows[i][15].ToString();                        
                        oSuePerMod.cDrenaje = dataTable.Rows[i][16].ToString();                            
                        oSuePerMod.cProfundidadEfectiva = dataTable.Rows[i][17].ToString();                
                        oSuePerMod.cFactorLimitante = dataTable.Rows[i][18].ToString();           

                        oSuePerModLN.RegistraInventarioSuelosPerfilModal(oSuePerMod);
                    }
                }
            }
        }

        public void RegistrarAlerta(int nUsuarioId, string cTitulo, string cMensaje, string cUrl, string AlertaIcono, string cAlertaColor)
        {
            AlertaLN oAlertaLN = new AlertaLN();
            Alerta oAlerta = new Alerta();

            oAlerta.nUsuarioId = nUsuarioId;
            oAlerta.cTitulo = cTitulo;
            oAlerta.cMensaje = cMensaje;
            oAlerta.cUrl = cUrl;
            oAlerta.cAlertaIcono = AlertaIcono;
            oAlerta.cAlertaColor = cAlertaColor;
            oAlerta.nEstado = 1;

            var nAlertaId = oAlertaLN.RegistrarAlerta(oAlerta);
        }

        public int RegistrarHistorial(int nHistId, int nUsuarioId, EstadoSolicitud nEstado, string cMensaje = "")
        {

            HistorialDetalleLN oHistDetLN = new HistorialDetalleLN();
            HistorialDetalle oHistDet = new HistorialDetalle
            {
                nHistorialId = nHistId,
                nUsuarioRegistra = nUsuarioId,
                cDescripcion = cMensaje,
                Estado = (int)nEstado
            };

            var nHisDet = oHistDetLN.RegistrarHistorialDetalle(oHistDet);
            return nHisDet;
        }
    }
}
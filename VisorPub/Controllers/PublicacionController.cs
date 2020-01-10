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
using SendEmail;
using System.Threading.Tasks;

namespace VisorPub.Controllers
{
    public class PublicacionController : Controller
    {
        //
        // GET: /publicacion/

        public ActionResult MisPublicaciones()
        {
            TipoAD handTipo = new TipoAD();
            TemaAD handTema = new TemaAD();
            RegistrarViewModel modelo = new RegistrarViewModel();
            modelo.lsTipos = handTipo.obtener();
            modelo.lsTemas = handTema.ObtenerTema();
            return View(modelo);
        }

        public ActionResult RevisionDeSolicitudes()
        {
            TipoAD handTipo = new TipoAD();
            TemaAD handTema = new TemaAD();
            RegistrarViewModel modelo = new RegistrarViewModel();
            modelo.lsTipos = handTipo.obtener();
            modelo.lsTemas = handTema.ObtenerTema();
            return View(modelo);
        }

        public string ListarMisPublicaciones(int nPubEst, int nPage = 1, int nSize = 10, int nPubId = -1, string cPubTitulo = "", string cInst = "")
        {
            PublicacionLN oPublica = new PublicacionLN();
            Usuario oUsuario = new Usuario();
            oUsuario = (Usuario)Session["Datos"]; //Agregar verificacíon de sessión

            ListaPaginada oListaPublicaciones = oPublica.ListarMisPublicaciones(nPubEst, oUsuario.nUsuarioId, nPage, nSize, nPubId, cPubTitulo, cInst);

            return JsonConvert.SerializeObject(oListaPublicaciones, Formatting.None,
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
        }

        public string ListarRevPublicaciones(int nPubEst, int nPage = 1, int nSize = 10, int nPubId = -1, string cPubTitulo = "", string cInst = "", string cNom = "")
        {
            PublicacionLN oPublica = new PublicacionLN();
            Usuario oUsuario = new Usuario();
            oUsuario.cDni = "";

            ListaPaginada oListaPublicaciones = oPublica.ListarRevPublicaciones(nPubEst, nPage, nSize, nPubId, cPubTitulo, oUsuario.cDni, cInst, cNom);

            return JsonConvert.SerializeObject(oListaPublicaciones, Formatting.None,
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
        }

        public ActionResult Index()
        {
            return View();
        }
        //[RequiresAuthenticationAttribute]
        public ActionResult Registrar() //Eliminar - no se usará
        {
            TipoAD handTipo = new TipoAD();
            TemaAD handTema = new TemaAD();
            RegistrarViewModel modelo = new RegistrarViewModel();
            modelo.lsTipos = handTipo.obtener();
            modelo.lsTemas = handTema.ObtenerTema();
            return View(modelo);
        }
        //[RequiresAuthenticationAttribute]
        public async Task<JsonResult> RegistrarPublicacion(int pub_anopublicacion, string pub_referenciabibliografica, string pub_enlace, int tip_idtipo, string ls_tem_idtema, string features, string pub_titulo)
        {

            RespuestaViewModel respuesta = new RespuestaViewModel();
            List<Feature> lsFeatures = new List<Feature>();
            JsonConvert.PopulateObject(features, lsFeatures);
            foreach (Feature itemF in lsFeatures)
            {
                itemF.Info = itemF.Info.Replace('|', ' ');
                if (itemF.Tipo == 3)
                {
                    itemF.Info = itemF.Info + "," + itemF.Info.Substring(0, itemF.Info.IndexOf(','));
                }
            }

            string[] ls_tem_idtemaArr = ls_tem_idtema.Split(',');

            Usuario oUsuarioReg = ((Usuario)Session["Datos"]);

            Publicacion oPubli = new Publicacion();
            oPubli.oTipo = new Tipo();
            oPubli.ListaTemas = new List<Tema>();

            oPubli.nUsuId = oUsuarioReg.nUsuarioId;
            oPubli.nPubliAno = pub_anopublicacion;
            oPubli.cRefBiblio = pub_referenciabibliografica;
            oPubli.cEnlace = pub_enlace;
            oPubli.oTipo.nTipoId = tip_idtipo;
            oPubli.ListaFeatures = lsFeatures;
            oPubli.cTitulo = pub_titulo;
            
            foreach (string itemTema in ls_tem_idtemaArr)
            {
                Tema itemTemaO = new Tema();
                itemTemaO.nTemaId = Int32.Parse(itemTema);
                oPubli.ListaTemas.Add(itemTemaO);
            }

            PublicacionLN oPubliLN = new PublicacionLN();
            oPubli = oPubliLN.RegistrarPublicacion(oPubli);

            if (oPubli.nPubliId != 0)
            {
                respuesta.estado = 1;
                respuesta.mensaje = "Se ha registrado la publicación";

                //Codificar url historial
                HistorialLN oHistorialLN = new HistorialLN();
                Historial oHistorial = new Historial
                {
                    nRefId = oPubli.nPubliId,
                    nTipoReferencia = (int)TipoReferencia.Publicaciones,
                    nEstado = 1
                };

                var nHistId = oHistorialLN.CrearHistorial(oHistorial);

                // Registra historial - Solicitud Registrada
                RegistrarHistorial(nHistId, oUsuarioReg.nUsuarioId, EstadoSolicitud.Solicitado);

                //Obtener Unique Historial
                var cHistUniqueId = oHistorialLN.GetRecordUniqueIdByReferenciaId(oPubli.nPubliId, TipoReferencia.Publicaciones);

                //Enviar correo usuario registra
                await GmailClient.SendEmailAsync(oUsuarioReg.cEmail, $"Solicitud de Publicación N° {oPubli.nPubliId} - Nueva Solicitud | VISOR IIAP", "<p>Estimado/a " + oUsuarioReg.cNombres + $"</p><p> Se ha registrado su solicitud de Publicación N° {oPubli.nPubliId} y estará en proceso de evaluación. Muchas Gracias </p><a href='http://localhost:59423/Historial/Publicaciones/" + cHistUniqueId + "' target='_blank'> Revisar Historial </a>", "");

                //Enviar alerta a los supervisores
                RolLN oRol = new RolLN();
                var oSupervisores = oRol.CargarUsuariosPorRol(RolId.Supervisor);

                if(oSupervisores.Count() > 0) { 
                    foreach (Usuario u in oSupervisores)
                    {
                        RegistrarAlerta(u.nUsuarioId, $"Solicitud de Publicación N° {oPubli.nPubliId} - Nueva Solicitud", "Se ha registrado una nueva solicitud para su revisión.", $"/Publicacion/RevisionDeSolicitudes/{oPubli.nPubliId}", AlertIcon.Solicitado, AlertColor.Solicitado);
                    }

                    //Enviar correo usuarios perfil supervisor
                    var cSupervisorEmails = String.Join(", ", oSupervisores.Select(u => u.cEmail).ToArray());
                    await GmailClient.SendEmailAsync(cSupervisorEmails, $"Solicitud de Publicación N° {oPubli.nPubliId} - Nueva Solicitud | VISOR IIAP", "<p> Se ha registrado una nueva solicitud de Publicación con código  " + oPubli.nPubliId + ", favor de ingresar a la intranet para proceder con su validación. </p><a href='http://localhost:59423/Historial/Publicaciones/" + cHistUniqueId + "' target='_blank'> Revisar Historial </a>", "");
                }
            }
            return Json(JsonConvert.SerializeObject(respuesta));
        }

        public async Task<JsonResult> RegistrarCorreccion(int nPubId, int nUsuId, int nHistId, string nUHistId, int pub_anopublicacion, string pub_referenciabibliografica, string pub_enlace, int tip_idtipo, string ls_tem_idtema, string features, string pub_titulo)
        {

            RespuestaViewModel respuesta = new RespuestaViewModel();
            List<Feature> lsFeatures = new List<Feature>();
            JsonConvert.PopulateObject(features, lsFeatures);
            foreach (Feature itemF in lsFeatures)
            {
                itemF.Info = itemF.Info.Replace('|', ' ');
                if (itemF.Tipo == 3)
                {
                    itemF.Info = itemF.Info + "," + itemF.Info.Substring(0, itemF.Info.IndexOf(','));
                }
            }

            string[] ls_tem_idtemaArr = ls_tem_idtema.Split(',');

            Publicacion oPubli = new Publicacion();
            oPubli.oTipo = new Tipo();
            oPubli.ListaTemas = new List<Tema>();

            oPubli.nPubliId = nPubId;
            oPubli.nPubliAno = pub_anopublicacion;
            oPubli.cRefBiblio = pub_referenciabibliografica;
            oPubli.cEnlace = pub_enlace;
            oPubli.oTipo.nTipoId = tip_idtipo;
            oPubli.ListaFeatures = lsFeatures;
            oPubli.cTitulo = pub_titulo;

            foreach (string itemTema in ls_tem_idtemaArr)
            {
                Tema itemTemaO = new Tema();
                itemTemaO.nTemaId = Int32.Parse(itemTema);
                oPubli.ListaTemas.Add(itemTemaO);
            }

            PublicacionLN oPubliLN = new PublicacionLN();
            oPubli = oPubliLN.EditarPublicacion(oPubli);

            if (oPubli.nPubliId != 0)
            {
                //Cambiar estado publicación
                PublicacionLN oPubLN = new PublicacionLN();
                oPubLN.ActualizaEstadoPublicacion(nPubId, (int)EstadoSolicitud.Solicitado);

                //Obtener datos usuario de la publicación
                Usuario oUsuarioReg = ((Usuario)Session["Datos"]);

                // Registra historial - Rechazado
                RegistrarHistorial(nHistId, oUsuarioReg.nUsuarioId, EstadoSolicitud.Solicitado);

                //Enviar correo usuario registra
                await GmailClient.SendEmailAsync(oUsuarioReg.cEmail, $"Solicitud de Publicación N° {oPubli.nPubliId} - Corregida | VISOR IIAP", "<p>Estimado/a " + oUsuarioReg.cNombres + $"</p><p> Se ha registrado su solicitud de Publicación N° {oPubli.nPubliId} y estará en proceso de evaluación. Muchas Gracias </p><a href='http://localhost:59423/Historial/Publicaciones/" + nUHistId + "' target='_blank'> Revisar Historial </a>", "");

                //Enviar alerta a los supervisores
                RolLN oRol = new RolLN();
                var oSupervisores = oRol.CargarUsuariosPorRol(RolId.Supervisor);

                if (oSupervisores.Count() > 0)
                {
                    foreach (Usuario u in oSupervisores)
                    {
                        RegistrarAlerta(u.nUsuarioId, $"Solicitud de Publicación N° {oPubli.nPubliId} - Corregida", "Se ha registrado una nueva solicitud para su revisión.", $"/Publicacion/RevisionDeSolicitudes/{oPubli.nPubliId}", AlertIcon.Solicitado, AlertColor.Solicitado);
                    }

                    //Enviar correo usuarios perfil supervisor
                    var cSupervisorEmails = String.Join(", ", oSupervisores.Select(u => u.cEmail).ToArray());
                    await GmailClient.SendEmailAsync(cSupervisorEmails, $"Solicitud de Publicación N° {oPubli.nPubliId} - Corregida | VISOR IIAP", "<p> Se ha registrado la correción de una solicitud de Publicación con código " + oPubli.nPubliId + ", favor de ingresar a la intranet para proceder con su validación. </p><a href='http://localhost:59423/Historial/Publicaciones/" + nUHistId + "' target='_blank'> Revisar Historial </a>", "");
                }

                respuesta.estado = (int)EstadoSolicitud.Solicitado;
                respuesta.mensaje = "Se ha enviado las correcciones realizadas.";

            }
            return Json(JsonConvert.SerializeObject(respuesta));
        }

        public async Task<JsonResult> RegistrarObservacion(int nPubId, int nUsuId, int nHistId, string nUHistId, string cMensaje)
        {
            Usuario oUsuarioReg = ((Usuario)Session["Datos"]);

            //Cambiar estado publicación
            PublicacionLN oPubLN = new PublicacionLN();
            oPubLN.ActualizaEstadoPublicacion(nPubId, (int)EstadoSolicitud.Observado);

            //Obtener datos usuario de la publicación

            UsuarioLN oUsuarioLN = new UsuarioLN();
            var oUsuarioPub = oUsuarioLN.CargarDatosUsuario(nUsuId);

            // Registra historial - Observación
            var nHisDet = RegistrarHistorial(nHistId, oUsuarioReg.nUsuarioId, EstadoSolicitud.Observado, cMensaje);

            //Registrar alerta para el usuario
            RegistrarAlerta(oUsuarioPub.nUsuarioId, $"Solicitud de Publicación N° {nPubId} - Observada", "Se ha encontrado observaciones en su solicitud. Favor de revisar los detalles en el historial.", $"/Publicacion/MisPublicaciones/{nPubId}", AlertIcon.Observado, AlertColor.Observado);

            HistorialLN oHistorialLN = new HistorialLN();
            //Supervisor envia correo a usuario registro
            await GmailClient.SendEmailAsync(oUsuarioPub.cEmail, $"Solicitud de Publicación N° {nPubId} - Observada | VISOR IIAP", "<p>Estimado/a " + oUsuarioPub.cNombres + "</p><p> Se ha encontrado observaciones a la solicitud realizada. Agradeceremos levantar las observaciones para proceder a su aprobación. </p><a href='http://localhost:59423/Historial/Publicaciones/" + nUHistId + "' target='_blank'> Ver Detalle </a>", "");

            return Json(JsonConvert.SerializeObject(nHisDet));
        }

        public async Task<JsonResult> RegistrarRechazo(int nPubId, int nUsuId, int nHistId, string nUHistId, string cMensaje)
        {
            Usuario oUsuarioReg = ((Usuario)Session["Datos"]);

            //Cambiar estado publicación
            PublicacionLN oPubLN = new PublicacionLN();
            oPubLN.ActualizaEstadoPublicacion(nPubId, (int)EstadoSolicitud.Rechazado);

            //Obtener datos usuario de la publicación

            UsuarioLN oUsuarioLN = new UsuarioLN();
            var oUsuarioPub = oUsuarioLN.CargarDatosUsuario(nUsuId);

            // Registra historial - Rechazado
            var nHisDet = RegistrarHistorial(nHistId, oUsuarioReg.nUsuarioId, EstadoSolicitud.Rechazado, cMensaje);

            //Registrar alerta para el usuario
            RegistrarAlerta(oUsuarioPub.nUsuarioId, $"Solicitud de Publicación N° {nPubId} - Rechazada", "Se ha rechazado su solicitud. Por favor de revisar los comentarios en el historial.", $"/Publicacion/MisPublicaciones/{nPubId}", AlertIcon.Rechazado, AlertColor.Rechazado);

            HistorialLN oHistorialLN = new HistorialLN();
            var cHistUniqueId = oHistorialLN.GetRecordUniqueIdByReferenciaId(nPubId, TipoReferencia.Publicaciones);
            //Supervisor envia correo a usuario registro
            await GmailClient.SendEmailAsync(oUsuarioPub.cEmail, $"Solicitud de Publicación N° {nPubId} - Rechazada | VISOR IIAP", "<p>Estimado/a " + oUsuarioPub.cNombres + "</p><p>Agradecemos su colaboración, sin embargo, se ha rechazado su solicitud. Agradeceremos ponerse en contacto con nosotros de existir algún mal entendido. Muchas gracias. </p><a href='http://localhost:59423/Historial/Publicaciones/" + cHistUniqueId + "' target='_blank'> Ver Detalle </a>", "");

            return Json(JsonConvert.SerializeObject(nHisDet));
        }

        public async Task<JsonResult> RegistrarAprobacion(int nPubId, int nUsuId, int nHistId, string nUHistId)
        {
            Usuario oUsuarioReg = ((Usuario)Session["Datos"]);

            //Cambiar estado publicación
            PublicacionLN oPubLN = new PublicacionLN();
            oPubLN.ActualizaEstadoPublicacion(nPubId, (int)EstadoSolicitud.Aprobado);

            //Obtener datos usuario de la publicación

            UsuarioLN oUsuarioLN = new UsuarioLN();
            var oUsuarioPub = oUsuarioLN.CargarDatosUsuario(nUsuId);

            // Registra historial - Aprobado
            var nHisDet = RegistrarHistorial(nHistId, oUsuarioReg.nUsuarioId, EstadoSolicitud.Aprobado);

            //Registrar alerta para el usuario
            RegistrarAlerta(oUsuarioPub.nUsuarioId, $"Solicitud de Publicación N° {nPubId} - ¡Aprobada!", "Se ha aprobado su solicitud. Muchas gracias por su gran trabajo.", $"/Publicacion/MisPublicaciones/{nPubId}", AlertIcon.Aprobado, AlertColor.Aprobado);

            HistorialLN oHistorialLN = new HistorialLN();
            var cHistUniqueId = oHistorialLN.GetRecordUniqueIdByReferenciaId(nPubId, TipoReferencia.Publicaciones);
            //Supervisor envia correo a usuario registro
            await GmailClient.SendEmailAsync(oUsuarioPub.cEmail, $"Solicitud de Publicación N° {nPubId} - ¡Aprobada! | VISOR IIAP", "<p>Estimado/a " + oUsuarioPub.cNombres + $"</p><p>Su solicitud de Publicación N° {nPubId} ha sido aprobada. Muchas gracias por su colaboración. </p><a href='http://localhost:59423/Historial/Publicaciones/" + cHistUniqueId + "' target='_blank'> Ver Detalle </a>", "");

            return Json(JsonConvert.SerializeObject(nHisDet));
        }

        //[RequiresAuthenticationAttribute]
        public ActionResult Listar()
        {
            TipoAD handTipo = new TipoAD();
            TemaAD handTema = new TemaAD();
            PublicacionAD handPub = new PublicacionAD();
            ListarViewModel modelo = new ListarViewModel();
            modelo.lsTipos = handTipo.obtener();
            modelo.lsTemas = handTema.ObtenerTema();
            modelo.lsPublicaciones = handPub.listar();
            return View(modelo);
        }

        public string CargaDatosPublicacion(int nPubId)
        {
            PublicacionLN oPublicacion = new PublicacionLN();
            Publicacion oPubli = new Publicacion();
            oPubli = oPublicacion.CargaDatosPublicacion(nPubId);

            return JsonConvert.SerializeObject(oPubli, Formatting.None,
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
        }


        public JsonResult EditarPublicacion
           (
               int pub_idpublicacion,
               int pub_anopublicacion,
               string pub_referenciabibliografica,
               string pub_enlace,
               int tip_idtipo,
               string ls_tem_idtema,
               string features,
               string pub_titulo
           )
        {

            RespuestaViewModel respuesta = new RespuestaViewModel();
            List<Feature> lsFeatures = new List<Feature>();
            JsonConvert.PopulateObject(features, lsFeatures);
            foreach (Feature itemF in lsFeatures)
            {
                itemF.Info = itemF.Info.Replace('|', ' ');
                if (itemF.Tipo == 3)
                {
                    itemF.Info = itemF.Info + "," + itemF.Info.Substring(0, itemF.Info.IndexOf(','));
                }
            }


            string[] ls_tem_idtemaArr = ls_tem_idtema.Split(',');
            Publicacion item = new Publicacion();
            PublicacionAD hand = new PublicacionAD();

            item.nPubliId = pub_idpublicacion;
            item.nPubliAno = pub_anopublicacion;
            item.cRefBiblio = pub_referenciabibliografica;
            item.cEnlace = pub_enlace;
            item.oTipo = new Tipo();
            item.oTipo.nTipoId = tip_idtipo;
            item.ListaFeatures = lsFeatures;
            item.cTitulo = pub_titulo;
            item.ListaTemas = new List<Tema>();
            foreach (string itemTema in ls_tem_idtemaArr)
            {
                Tema itemTemaO = new Tema();
                itemTemaO.nTemaId = Int32.Parse(itemTema);
                item.ListaTemas.Add(itemTemaO);
            }
            item = hand.EditarPublicacion(item);
            if (item.nPubliId != 0)
            {
                respuesta.estado = 1;
                respuesta.mensaje = "Se ha editado la publicación";
            }
            return Json(JsonConvert.SerializeObject(respuesta));
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

        public int RegistrarHistorial(int nHistId, int nUsuarioId, EstadoSolicitud nEstado, string cMensaje = "") { 

        // Registra historial - Rechazado
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

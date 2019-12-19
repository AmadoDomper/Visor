using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADPostgres;
using VisorPub.Models.Publicacion;
using Newtonsoft.Json;
using EPostgres;
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

            ListaPaginada oListaPublicaciones = oPublica.ListarMisPublicaciones(nPubEst, nPage, nSize, nPubId, cPubTitulo, oUsuario.cDni, cInst);

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

                //Enviar correo usuario registra
                await GmailClient.SendEmailAsync(oUsuarioReg.cEmail, "Registro de Publicación #000 ", "<p>Estimado/a" + oUsuarioReg.cNombres +  "</p><p> Se ha registrado su solicitud y estará en proceso de evaluación. Muchas Gracias </p>", "");
                //Enviar correo usuarios perfil supervisor
                RolLN oRol = new RolLN();
                var cSupervisorEmails = oRol.GetSupervisorEmails();
                await GmailClient.SendEmailAsync(cSupervisorEmails, "Registro de Publicación #000 ", "<p> Se ha registrado una nueva solicitud de Publicación con código 000, favor de ingresar a la intranet para proceder con su validación. </p>", "");
            }
            return Json(JsonConvert.SerializeObject(respuesta));
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
        //[RequiresAuthenticationAttribute]
        //public ActionResult Editar() //Eliminar --no se usara
        //{
        //    int pub_idpublicacion = Int32.Parse(Request["idpub"]);
        //    TipoAD handTipo = new TipoAD();
        //    TemaAD handTema = new TemaAD();
        //    PublicacionAD handPub = new PublicacionAD();
        //    RegistrarViewModel modelo = new RegistrarViewModel();
        //    modelo.lsTipos = handTipo.obtener();
        //    modelo.lsTemas = handTema.ObtenerTema();
        //    modelo.publicacion = handPub.obtener(pub_idpublicacion);
        //    return View(modelo);
        //}

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
            item = hand.editar(item);
            if (item.nPubliId != 0)
            {
                respuesta.estado = 1;
                respuesta.mensaje = "Se ha editado la publicación";
            }
            return Json(JsonConvert.SerializeObject(respuesta));
        }

    }


}

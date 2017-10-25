using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADPostgres;
using VisorPub.Models.Publicacion;
using Newtonsoft.Json;
using EPostgres;
using VisorPub.Models;
using Seguridad.filters;

namespace VisorPub.Controllers
{
    public class PublicacionController : Controller
    {
        //
        // GET: /publicacion/

        public ActionResult Index()
        {
            return View();
        }
        [RequiresAuthenticationAttribute]
        public ActionResult Registrar()
        {
            TipoAD handTipo = new TipoAD();
            TemaAD handTema= new TemaAD();
            RegistrarViewModel modelo = new RegistrarViewModel();
            modelo.lsTipos = handTipo.obtener();
            modelo.lsTemas = handTema.obtener();
            return View(modelo);
        }
        [RequiresAuthenticationAttribute]
        public JsonResult RegistrarPublicacion
            (
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
                itemF.Info = itemF.Info.Replace('|',' ');
                if (itemF.Tipo == 3)
                {
                    itemF.Info = itemF.Info +","+ itemF.Info.Substring(0, itemF.Info.IndexOf(','));
                }
            }


            string[] ls_tem_idtemaArr = ls_tem_idtema.Split(',');
            Publicacion item = new Publicacion();
            PublicacionAD hand = new PublicacionAD();
            item.pub_anopublicacion = pub_anopublicacion;
            item.pub_referenciabibliografica = pub_referenciabibliografica;
            item.pub_enlace = pub_enlace;
            item.oTipo = new Tipo();
            item.oTipo.tip_idtipo = tip_idtipo;
            item.lsFeatures = lsFeatures;
            item.pub_titulo = pub_titulo;
            item.lsTemas = new List<Tema>();
            foreach (string itemTema in ls_tem_idtemaArr)
            {
                Tema itemTemaO = new Tema();
                itemTemaO.tem_idtema =Int32.Parse(itemTema);
                item.lsTemas.Add(itemTemaO);
            }
            item=hand.registrar(item);
            if (item.pub_idpublicacion != 0)
            {
                respuesta.estado = 1;
                respuesta.mensaje = "Se ha registrado la publicación";
            }
            return Json(JsonConvert.SerializeObject(respuesta));
        }

        [RequiresAuthenticationAttribute]
        public ActionResult Listar()
        {
            TipoAD handTipo = new TipoAD();
            TemaAD handTema = new TemaAD();
            PublicacionAD handPub=new PublicacionAD();
            ListarViewModel modelo = new ListarViewModel();
            modelo.lsTipos = handTipo.obtener();
            modelo.lsTemas = handTema.obtener();
            modelo.lsPublicaciones = handPub.listar();
            return View(modelo);
        }
        [RequiresAuthenticationAttribute]
        public ActionResult Editar()
        {
            int pub_idpublicacion = Int32.Parse(Request["idpub"]);
            TipoAD handTipo = new TipoAD();
            TemaAD handTema = new TemaAD();
            PublicacionAD handPub = new PublicacionAD();
            RegistrarViewModel modelo = new RegistrarViewModel();
            modelo.lsTipos = handTipo.obtener();
            modelo.lsTemas = handTema.obtener();
            modelo.publicacion = handPub.obtener(pub_idpublicacion);
            return View(modelo);
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
            item.pub_idpublicacion = pub_idpublicacion;
            item.pub_anopublicacion = pub_anopublicacion;
            item.pub_referenciabibliografica = pub_referenciabibliografica;
            item.pub_enlace = pub_enlace;
            item.oTipo = new Tipo();
            item.oTipo.tip_idtipo = tip_idtipo;
            item.lsFeatures = lsFeatures;
            item.pub_titulo = pub_titulo;
            item.lsTemas = new List<Tema>();
            foreach (string itemTema in ls_tem_idtemaArr)
            {
                Tema itemTemaO = new Tema();
                itemTemaO.tem_idtema = Int32.Parse(itemTema);
                item.lsTemas.Add(itemTemaO);
            }
            item = hand.editar(item);
            if (item.pub_idpublicacion != 0)
            {
                respuesta.estado = 1;
                respuesta.mensaje = "Se ha editado la publicación";
            }
            return Json(JsonConvert.SerializeObject(respuesta));
        }

    }


}

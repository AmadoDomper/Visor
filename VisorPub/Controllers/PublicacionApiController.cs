using System;
using System.Collections.Generic;
using VisorPub.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using EPostgres;
using LNPostgres;
using System.Web.Script.Serialization;
using System.Web.Http.Cors;
using System.Configuration;

namespace VisorPub.Controllers
{
    [EnableCors(origins: "http://visores.iiap.gob.pe", headers: "*", methods: "*")]
    public class PublicacionApiController : ApiController
    {
        readonly string routeJSON;

        public PublicacionApiController()
        {
            routeJSON = ConfigurationManager.AppSettings["routeJSON"].ToString();
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //GET api/<controller>/5
        public IHttpActionResult GetCombos()
        {
            PublicacionLN oPublica = new PublicacionLN();
            ComboViewModel oCombo = new ComboViewModel();

            oCombo.lsTemas = oPublica.ListarTemas();
            oCombo.lsTipos = oPublica.ListarTipos();

            return Json(oCombo, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
        }

        public IHttpActionResult GetDatosPublicacion(int id)
        {
            PublicacionLN oPublicacion = new PublicacionLN();
            Publicacion oPubli = new Publicacion();
            oPubli = oPublicacion.CargaDatosPublicacion(id);

            return Json(oPubli, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
        }


        public IHttpActionResult getAllPublicationPoints()
        {
            PublicacionLN oPubli = new PublicacionLN();
            Object json;

            json = oPubli.GetAllPublicationPoints();

            return Json(json);
        }

        public IHttpActionResult GetAllPublicactionsJSON()
        {
            PublicacionLN oPubli = new PublicacionLN();
            Object json;

            json = oPubli.GetAllPublicactionsJSON();

            return Json(json);
        }

        public IHttpActionResult GetSearchPublicactionPoints(string cPubTexto = "", int nTipo = -1, string cAno = "", int nAreaTem = -1)
        {
            PublicacionLN oPubli = new PublicacionLN();
            Object json;

            json = oPubli.GetSearchPublicactionPoints(cPubTexto, nTipo, cAno, nAreaTem);

            return Json(json);
        }

        public IHttpActionResult GetSearchPublicactionIds(string cPubTexto = "", int nTipo = -1, string cAno = "", int nAreaTem = -1)
        {
            PublicacionLN oPubli = new PublicacionLN();
            Object json;

            json = oPubli.GetSearchPublicactionIds(cPubTexto, nTipo, cAno, nAreaTem);

            return Json(json);
        }

        public IHttpActionResult getRegions()
        {

            string file = System.IO.File.ReadAllText(routeJSON + "GeoJson\\regions-topo.json");

            Object json;

            json = JsonConvert.DeserializeObject(file);
            return Json(json);
        }

        public IHttpActionResult getProvincias()
        {
            string file = System.IO.File.ReadAllText(routeJSON + "GeoJson\\provincias-topo.json");

            Object json;

            json = JsonConvert.DeserializeObject(file);
            return Json(json);
        }

        public IHttpActionResult getDistritos()
        {
            string file = System.IO.File.ReadAllText(routeJSON + "GeoJson\\distritos.json");

            Object json;

            json = JsonConvert.DeserializeObject(file);
            return Json(json);
        }

        public IHttpActionResult getUbigeo()
        {
            string file = System.IO.File.ReadAllText(routeJSON + "GeoJson\\ubigeo.json");

            Object json;

            json = JsonConvert.DeserializeObject(file);
            return Json(json);
        }

        public IHttpActionResult GetHistorialDet(string id)
        {
            HistorialDetalleLN oHistDet = new HistorialDetalleLN();
            Object json;

            json = oHistDet.GetHistorialDetalleJSON(id);

            return Json(json);
        }

        //POST api/<controller>
        //[EnableCors("AllowSpecificOrigin")]
        public IHttpActionResult Post([FromBody]BuscadorApi v)
        {
            PublicacionLN oPublica = new PublicacionLN();
            Usuario oUsuario = new Usuario();
            oUsuario.cDni = "";

            ListaPaginada oListaPublicaciones = oPublica.BuscarPublicacionesPag(v.nPage, v.nPageSize, v.cTexto, v.nTipo, v.cAnoPub, v.nAreaTema);

            return Json(oListaPublicaciones, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
        }

        //PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        //DELETE api/<controller>/5
        public void Delete(int id)
        {
        }


        //ProductViewModel[] products = new ProductViewModel[]
        //{
        //    new ProductViewModel { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
        //    new ProductViewModel { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
        //    new ProductViewModel { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
        //};

        //public IEnumerable<ProductViewModel> GetAllProducts()
        //{
        //    return products;
        //}

        //public IHttpActionResult GetProduct(int id)
        //{
        //    var product = products.FirstOrDefault((p) => p.Id == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(product);
        //}



    }
}
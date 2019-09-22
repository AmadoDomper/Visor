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

namespace VisorPub.Controllers
{
    [EnableCors(origins: "http://visores.iiap.gob.pe", headers: "*", methods: "*")]
    public class PublicacionApiController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //GET api/<controller>/5
        public string GetCombos()
        {
            PublicacionLN oPublica = new PublicacionLN();
            ComboViewModel oCombo = new ComboViewModel();

            oCombo.lsTemas = oPublica.ListarTemas();
            oCombo.lsTipos = oPublica.ListarTipos();

            return JsonConvert.SerializeObject(oCombo, Formatting.None,
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
        }

        public string GetDatosPublicacion(int id)
        {
            PublicacionLN oPublicacion = new PublicacionLN();
            Publicacion oPubli = new Publicacion();
            oPubli = oPublicacion.CargaDatosPublicacion(id);

            return JsonConvert.SerializeObject(oPubli, Formatting.None,
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
        }

        //POST api/<controller>
        //[EnableCors("AllowSpecificOrigin")]
        public string Post([FromBody]BuscadorApi v)
        {
            PublicacionLN oPublica = new PublicacionLN();
            Usuario oUsuario = new Usuario();
            oUsuario.cDni = "";

            ListaPaginada oListaPublicaciones = oPublica.BuscarPublicacionesPag(v.nPage, v.nPageSize, v.cTexto, v.nTipo, v.cAnoPub,v.nAreaTema);

            //ListaPaginada oListaPublicaciones = new ListaPaginada { nPage = 1, nPageSize = 1, nRows = 10, nPageTotal = 10 };

            return JsonConvert.SerializeObject(oListaPublicaciones, Formatting.None,
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
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
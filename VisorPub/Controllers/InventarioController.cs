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

namespace VisorPub.Controllers
{
    public class InventarioController : Controller
    {
        // GET: Inventario
        public ActionResult MisInventarios()
        {
            return View();
        }

        public string ListarMisInventariosVegetacion(int nInvEst, int nPage = 1, int nSize = 10, string cNombreProy = "", string cAno = "")
        {
            InventarioVegetacionLN oInvVeg = new InventarioVegetacionLN();
            Usuario oUsuario = new Usuario();
            oUsuario = (Usuario)Session["Datos"]; //Agregar verificacíon de sessión

            ListaPaginada oListaPublicaciones = oInvVeg.ListarMisInventariosVegetacion(nInvEst, nPage, nSize, cNombreProy, cAno);

            return JsonConvert.SerializeObject(oListaPublicaciones, Formatting.None,
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
        }

        public string CargaDatosInventarioVegetacionParcelas(int nVegId)
        {
            VegetacionParcelaLN oVegParLN = new VegetacionParcelaLN();
            List<VegetacionParcela> oListaVegPar = new List<VegetacionParcela>();
            //int nVegId = 36;

            oListaVegPar = oVegParLN.CargaListaInventarioVegetacionParcelas(nVegId);

            return JsonConvert.SerializeObject(oListaVegPar, Formatting.None,
                 new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
        }

        public int ActualizaInventarioVegetacionParcelas(VegetacionParcela oVegPar)
        {
            int id;
            VegetacionParcelaLN oVegParLN = new VegetacionParcelaLN();

            id = oVegParLN.ActualizaInventarioVegetacionParcela(oVegPar);

            return id;
        }



    }
}
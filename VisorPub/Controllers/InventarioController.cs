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

            ListaPaginada oListaPublicaciones = oInvVeg.ListarMisInventariosVegetacion(nInvEst, nPage, nSize, cNombreProy, cAno, oUsuario.nUsuarioId);

            return JsonConvert.SerializeObject(oListaPublicaciones, Formatting.None,
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


        public int GuardarInventarioVegetacion(InventarioVegetacion oVegPar)
        {
            int id;
            InventarioVegetacionLN oInvVegLN = new InventarioVegetacionLN();
            Usuario oUsuario = new Usuario();
            oUsuario = (Usuario)Session["Datos"];

            oVegPar.nUsuarioId = oUsuario.nUsuarioId;

            if (oVegPar.nVegetacionId == 0)
            {
                id = oInvVegLN.RegistraInventarioVegetacion(oVegPar);
            }
            else
            {
                id = oInvVegLN.ActualizaInventarioVegetacion(oVegPar);
            }

            return id;
        }

        public int EliminaInventarioVegetacion(InventarioVegetacion oVegPar)
        {
            int id;
            InventarioVegetacionLN oInvVegLN = new InventarioVegetacionLN();

            id = oInvVegLN.EliminaInventarioVegetacion(oVegPar);

            return id;
        }


    }
}
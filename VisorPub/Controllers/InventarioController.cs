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

            ListaPaginada oLista = oInvVeg.ListarMisInventariosVegetacion(nInvEst, nPage, nSize, cNombreProy, cAno, oUsuario.nUsuarioId);

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



        // INVENTARIO DE SUELOS

        public ActionResult MisInventariosSuelos()
        {
            return View();
        }

        public string ListarMisInventariosSuelos(int nInvEst, int nPage = 1, int nSize = 10, string cNombreProy = "", string cAno = "", string cNombreColector = "")
        {
            InventarioSuelosLN oInvSue = new InventarioSuelosLN();
            Usuario oUsuario = new Usuario();
            oUsuario = (Usuario)Session["Datos"]; //Agregar verificacíon de sessión

            ListaPaginada oLista = oInvSue.ListarMisInventariosSuelos(nInvEst, nPage, nSize, cNombreProy, cAno, cNombreColector);

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


    }
}
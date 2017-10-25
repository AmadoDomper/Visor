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
    public class ConfigController : Controller
    {
        public ActionResult Config()
        {
            return View();
        }

        public JsonResult ListaRoles()
        {
            RolLN oRolLN = new RolLN();
            List<Rol> ListaRoles = new List<Rol>();
            ListaRoles = oRolLN.ListarRoles();
            return Json(JsonConvert.SerializeObject(ListaRoles, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }

        public JsonResult CargaRolPermisos(int nRolId)
        {
            ConfiguracionLN oConfigLN = new ConfiguracionLN();
            Rol lstRol = new Rol();

            lstRol = oConfigLN.CargaRolPermisos(nRolId);
            return Json(JsonConvert.SerializeObject(lstRol));
        }

        public JsonResult RegistrarRolPermisos(string oJsonRol)
        {
            int nReg = 0;
            ConfiguracionLN ConfLN = new ConfiguracionLN();

            Rol lstRol = JsonConvert.DeserializeObject<Rol>(oJsonRol);
            nReg = ConfLN.RegistrarActualizarRolPermisos(lstRol);

            return Json(nReg);
        }

        public JsonResult EliminarRol(int nRolId)
        {
            int nReg = 0;
            ConfiguracionLN ConfLN = new ConfiguracionLN();
            nReg = ConfLN.EliminarRol(nRolId);

            return Json(nReg);
        }
    }
}
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
using VisorPub.Helper;

namespace VisorPub.Controllers
{
    public class UsuarioController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GestionarUsuarios()
        {
            return View();
        }

        public string ListaUsuariosPag(int nPage = 1, int nSize = 10, int nUsuId = -1, string cUsuDni = "", string cUsuName = "")
        {
            UsuarioLN oUsuarios = new UsuarioLN();
            ListaPaginada ListaUsuariosPag = oUsuarios.ListarUsuariosPag(nPage, nSize, nUsuId, cUsuDni, cUsuName);

            return JsonConvert.SerializeObject(ListaUsuariosPag, Formatting.None,
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
        }


        public string CargarDatosUsuario(int nUsuId)
        {
            UsuarioLN oUsuarioLN = new UsuarioLN();
            Usuario oUsuario = new Usuario();

            oUsuario = oUsuarioLN.CargarDatosUsuario(nUsuId);

            return JsonConvert.SerializeObject(oUsuario, Formatting.None,
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });

        }

        public ActionResult NuevoUsuario()
        {
            return View();
        }


        public string RegistrarModificarUsuario(Usuario oUsu)
        {   
            UsuarioLN oUsuarioLN = new UsuarioLN();
            
            var resultado = oUsuarioLN.RegistrarModificarUsuario(oUsu);

            return JsonConvert.SerializeObject(resultado, Formatting.None,
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
        }

        public string RegistraUsuarioExterno(Usuario oUsu)
        {
            UsuarioLN oUsuarioLN = new UsuarioLN();
            oUsu.nUsuarioId = 0;
            oUsu.nRolId = Constantes.Rol_default;
            oUsu.bEsInterno = false;

            var resultado = oUsuarioLN.RegistrarModificarUsuario(oUsu);

            return JsonConvert.SerializeObject(resultado, Formatting.None,
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });

        }


    }
}
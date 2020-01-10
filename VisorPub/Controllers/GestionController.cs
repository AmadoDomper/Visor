using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Web.Security;
using System.Net;
using Seguridad.filters;
using EPostgres;
using LNPostgres;

namespace VisorPub.Controllers
{
    [RequiresAuthenticationAttribute]
    public class GestionController : Controller
    {
        [RequiresAuthenticationAttribute]
        public ActionResult Index()
        {
            return View();
        }

        public string GetAlertas(int nPage, int nSize)
        {
            Usuario oUsuario = new Usuario();
            oUsuario = (Usuario)Session["Datos"];

            AlertaLN oAlerta = new AlertaLN();
            Object json;

            json = oAlerta.GetAlertasJSON(oUsuario.nUsuarioId, nPage, nSize);

            return json.ToString();
        }


        public string ActualizaAlertaVisto()
        {
            Usuario oUsuario = new Usuario();
            oUsuario = (Usuario)Session["Datos"];

            AlertaLN oAlerta = new AlertaLN();
            Object json;

            json = oAlerta.ActualizaAlertaVisto(oUsuario.nUsuarioId);

            return json.ToString();
        }

        public string ActualizaAlertaRevisado(int nAlertaId)
        {
            Usuario oUsuario = new Usuario();
            oUsuario = (Usuario)Session["Datos"];

            AlertaLN oAlerta = new AlertaLN();
            Object json;

            json = oAlerta.ActualizaAlertaRevisado(oUsuario.nUsuarioId, nAlertaId);

            return json.ToString();
        }

    }
}
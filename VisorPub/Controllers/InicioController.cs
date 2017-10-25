using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADPostgres;
using EPostgres;

namespace VisorPub.Controllers
{
    public class InicioController : Controller
    {
        //
        // GET: /Inicio/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Acerca()
        {
            return View();
        }

        public ActionResult Contacto()
        {
            return View();
        }

    }
}

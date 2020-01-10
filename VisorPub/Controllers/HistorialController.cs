using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VisorPub.Controllers
{
    public class HistorialController : Controller
    {
        // GET: Historial
        public ActionResult Publicaciones()
        {
            return View();
        }

        // GET: Historial
        public ActionResult InventarioVegetaciones()
        {
            return View();
        }

        // GET: Historial
        public ActionResult InventarioSuelos()
        {
            return View();
        }
    }
}
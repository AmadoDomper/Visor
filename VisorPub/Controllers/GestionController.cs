using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Web.Security;
using System.Net;
using Seguridad.filters;

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

    }
}
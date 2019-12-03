using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPostgres;
using LNPostgres;

namespace VisorPub.Controllers
{
    public class JoinController : Controller
    {
        // GET: Join
        public ActionResult Index()
        {
            return View();
        }



        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SaveAccount(Usuario oUser)
        {
            int resultado;
            try
            {
                if (ValidationOK(oUser)) { 
                    UsuarioLN oUsuarioLN = new UsuarioLN();

                    resultado = oUsuarioLN.RegistrarModificarUsuario(oUser);
                }
                else
                {
                    //There is something wrong
                    resultado = -10;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public bool ValidationOK(Usuario oUser)
        {
            if (!ValidateRequired(oUser)) return false;

            return true;
        }


        public bool ValidateRequired(Usuario oUser)
        {

            if (oUser.cNombres == "" || oUser.cApellidoMa == "" || oUser.cApellidoPa == "" || oUser.cInstitucion == "" || oUser.cEmail == "" || oUser.cContrasena == "")
            {
                return false;
            }

            return true;
        }


    }
}
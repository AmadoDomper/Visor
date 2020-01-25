using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using EPostgres;
using EPostgres.Helper;
using LNPostgres;
using SendEmail;

namespace VisorPub.Controllers
{
    public class JoinController : Controller
    {
        // GET: Join
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Activate(string id)
        {
            UsuarioLN oUsuarioLN = new UsuarioLN();
            var res = oUsuarioLN.ActualizaEstadoConfirmacionEmail(id);
            ViewBag.status = res;
            return View();
        }



        [AcceptVerbs(HttpVerbs.Post)]
        [HttpPost]
        public async Task<JsonResult> SaveAccount(Usuario oUser)
        {
            string cUniqueId;
            int res = -1;
            try
            {
                if (ValidationOK(oUser)) { 
                    UsuarioLN oUsuarioLN = new UsuarioLN();

                    //Rol por defecto - Investigador
                    oUser.nRolId = (int)RolId.Investigador;

                    cUniqueId = oUsuarioLN.RegistrarModificarUsuario(oUser);

                    if(cUniqueId != "")
                    {
                        res = 0;

                        var callbackUrl = Url.Action("activate", "Join", null, protocol: Request.Url.Scheme);
                        await GmailClient.SendEmailAsync(oUser.cEmail, "Activación de cuenta | VISOR IIAP", "<p>Estimado/a " + oUser.cNombres + $"</p><p>Muchas gracias por registrar tu cuenta. Necesitamos que confirmes tu dirección de email: </p>" + $"<a href='{callbackUrl}/" + cUniqueId + "' target='_blank'>Verificar Email </a>", "");

                    }
                    
                    
                }
                else
                {
                    //There is something wrong
                    res = -10;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(res, JsonRequestBehavior.AllowGet);
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
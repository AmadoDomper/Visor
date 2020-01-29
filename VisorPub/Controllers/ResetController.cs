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
    public class ResetController : Controller
    {
        // GET: Reset
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewPassword(string id)
        {
            if (id != null)
            {
                UsuarioLN oUsuarioLN = new UsuarioLN();
                var IsActive = oUsuarioLN.IsEstadoPasswordResetActivo(id);

                ViewBag.id = id;
                ViewBag.IsActive = IsActive;
            }
            else
            {
                ViewBag.IsActive = false;
            }

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [HttpPost]
        public async Task<JsonResult> CreatePasswordReset(string cEmail)
        {
            string cResetId;
            int res = -1;
            try
            {
                UsuarioLN oUsuarioLN = new UsuarioLN();

                if (oUsuarioLN.VerificaExisteEmail(cEmail))
                {

                    cResetId = oUsuarioLN.GenerarNuevoResetId(cEmail);

                    if (cResetId != "")
                    {
                        res = 0;

                        var callbackUrl = Url.Action("NewPassword", "Reset", null, protocol: Request.Url.Scheme);
                        await GmailClient.SendEmailAsync(cEmail, "Restablecer constraseña | VISOR IIAP", "¿Olvidaste tu contraseña? No hay problema, sólo presiona el enlace abajo o copia la dirección en tu navegador para restablecerla. Al ir a este enlace, podrás ingresar y confirmar tu nueva contraseña." + $"<a href='{callbackUrl}/" + cResetId + "' target='_blank'>Verificar Email </a>", "");

                    }
                }
                else
                {
                    res = -2;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(res);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [HttpPost]
        public JsonResult ResetPassword(string cResetId, string cPassword)
        {
            int res = -1;
            try
            {
                UsuarioLN oUsuarioLN = new UsuarioLN();
                res = oUsuarioLN.RestablecerPassword(cResetId, cPassword);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(res);
        }


    }
}
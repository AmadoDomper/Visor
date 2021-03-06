﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using VisorPub.Models;
using LNPostgres;
using EPostgres;


namespace VisorPub.Controllers
{
    public class LoginController : Controller
    {
        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            base.Initialize(requestContext);
        }

        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Logear()
        {
            try
            {
                string UserEmail = Request["txtemail"];
                string Password = Request["txtpassword"];
                //validamos usuario
                bool validar = Membership.Provider.ValidateUser(UserEmail, Password);
                if (validar)
                {
                    //registramos usuario
                    FormsService.SignIn(UserEmail, true);
                    Usuario oUsuario = new Usuario();
                    oUsuario = (Usuario)Session["Datos"];

                    return RedirectToAction("/", "Gestion");
                }
                // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult CerrarSession()
        {
            FormsService.SignOut();
            Roles.DeleteCookie();
            Session.RemoveAll();
            return Redirect("http://visores.iiap.gob.pe/publicaciones/");
        }

    }
}

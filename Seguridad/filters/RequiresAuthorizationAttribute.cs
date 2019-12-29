using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Seguridad.filters
{
    public class RequiresAuthorizationAttribute : ActionFilterAttribute
    {
        public string CodigoOpcion { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if ((!filterContext.HttpContext.Request.IsAuthenticated) || (HttpContext.Current.Session["Datos"] == null))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index" }));
            }
            else if (HttpContext.Current.Session[CodigoOpcion] == null)
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Inicio", action = "Index" }));
            base.OnActionExecuting(filterContext);
        }
    }
}

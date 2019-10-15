using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using EPostgres;

namespace ADPostgres.Common
{
    public class FrontUser
    {
        public static bool TienePermiso(RolesPermisos valor)
        {
            return new RolAD().ValidaPermiso(((Usuario)HttpContext.Current.Session["Datos"]).nUsuarioId, (int)valor);
        }
    }
}

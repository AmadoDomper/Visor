using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPostgres;
using ADPostgres;

namespace LNPostgres
{
    public class RolLN
    {
        RolAD oRolAD;

        public RolLN()
        {
            oRolAD = new RolAD();
        }

        public List<Rol> ListarRoles()
        {
            return oRolAD.ListarRoles();
        }

    }
}

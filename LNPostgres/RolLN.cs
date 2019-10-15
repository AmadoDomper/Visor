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

        /// <summary>
        /// Permite identificar si determinado usuario tiene permiso para la operación ingresada
        /// </summary>
        /// <param name="nUsuarioId">Código único del usuario</param>
        /// <param name="nPermId">Código único del permiso</param>
        /// <returns></returns>
        public bool ValidaPermiso(int nUsuarioId, int nPermId)
        {
            return oRolAD.ValidaPermiso(nUsuarioId, nPermId);
        }

    }
}

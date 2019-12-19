using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPostgres;
using ADPostgres;

namespace LNPostgres
{
    public class SeguridadLN
    {
        SeguridadAD oSeguridadAD;

        public SeguridadLN()
        {
            oSeguridadAD = new SeguridadAD();
        }

        public Usuario ValidaAccesoUsuario(string cUsuarioEmail, string cClave)
        {
            try
            {
                return new SeguridadAD().ValidaAccesoUsuario(cUsuarioEmail, cClave);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}

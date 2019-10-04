using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPostgres;
using ADPostgres;

namespace LNPostgres
{
    public class SuelosPerfilModalLN
    {
        SuelosPerfilModalAD oSuelosPerfilModalAD;

        public SuelosPerfilModalLN()
        {
            oSuelosPerfilModalAD = new SuelosPerfilModalAD();
        }

        public int RegistraInventarioSuelosPerfilModal(SuelosPerfilModal oSuePerMod)
        {
            return oSuelosPerfilModalAD.RegistraInventarioSuelosPerfilModal(oSuePerMod);
        }

        public int ActualizaInventarioSuelosPerfilModal(SuelosPerfilModal oSuePerMod)
        {
            return oSuelosPerfilModalAD.ActualizaInventarioSuelosPerfilModal(oSuePerMod);
        }

        public int EliminaInventarioSuelosPerfilModal(SuelosPerfilModal oSuePerMod)
        {
            return oSuelosPerfilModalAD.EliminaInventarioSuelosPerfilModal(oSuePerMod);
        }

        public List<SuelosPerfilModal> CargaListaInventarioSuelosPerfilModal(int nSueId)
        {
            return oSuelosPerfilModalAD.CargaListaInventarioSuelosPerfilModal(nSueId);
        }

        public SuelosPerfilModal CargaInventarioSuelosPerfilModal(int nPerModId)
        {
            return oSuelosPerfilModalAD.CargaInventarioSuelosPerfilModal(nPerModId);
        }
    }
}

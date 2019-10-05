using System;
using System.Collections.Generic;
using EPostgres;
using ADPostgres;

namespace LNPostgres
{
    public class InventarioSuelosLN
    {
        InventarioSuelosAD oInventarioSuelosAD;

        public InventarioSuelosLN()
        {
            oInventarioSuelosAD = new InventarioSuelosAD();
        }

        public int RegistraInventarioSuelos(InventarioSuelos oInvSue)
        {
            return oInventarioSuelosAD.RegistraInventarioSuelos(oInvSue);
        }

        public int ActualizaInventarioSuelos(InventarioSuelos oInvSue)
        {
            return oInventarioSuelosAD.ActualizaInventarioSuelos(oInvSue);
        }

        public int EliminaInventarioSuelos(InventarioSuelos oInvSue)
        {
            return oInventarioSuelosAD.EliminaInventarioSuelos(oInvSue);
        }

        public InventarioSuelos CargaDatosInventarioSuelos(int nSueId)
        {
            return oInventarioSuelosAD.CargaDatosInventarioSuelos(nSueId);
        }

        public ListaPaginada ListarMisInventariosSuelos(int nInvEst, int nPage = 1, int nSize = 10, string cNombreProy = "", string cAno = "", string cNombreColector = "", int nUsuarioId = 0)
        {
            return oInventarioSuelosAD.ListarMisInventariosSuelosPag(nInvEst, nPage, nSize, cNombreProy, cAno, cNombreColector, nUsuarioId);
        }
    }
}

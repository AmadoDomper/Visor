using System;
using System.Collections.Generic;
using EPostgres;
using ADPostgres;

namespace LNPostgres
{
    public class InventarioVegetacionLN
    {
        InventarioVegetacionAD oInventarioVegetacionAD;

        public InventarioVegetacionLN()
        {
            oInventarioVegetacionAD = new InventarioVegetacionAD();
        }

        public int RegistraInventarioVegetacion(InventarioVegetacion oInvVeg)
        {
            return oInventarioVegetacionAD.RegistraInventarioVegetacion(oInvVeg);
        }

        public int ActualizaInventarioVegetacion(InventarioVegetacion oInvVeg)
        {
            return oInventarioVegetacionAD.ActualizaInventarioVegetacion(oInvVeg);
        }

        public int EliminaInventarioVegetacion(InventarioVegetacion oInvVeg)
        {
            return oInventarioVegetacionAD.EliminaInventarioVegetacion(oInvVeg);
        }

        public InventarioVegetacion CargaDatosInventarioVegetacion(int nVegId)
        {
            return oInventarioVegetacionAD.CargaDatosInventarioVegetacion(nVegId);
        }

        public ListaPaginada ListarMisInventariosVegetacion(int nInvEst, int nPage = 1, int nSize = 10, string cNombreProy = "", string cAno = "", int nUsuarioId = 0)
        {
            return oInventarioVegetacionAD.ListarMisInventariosVegetacionPag(nInvEst, nPage, nSize, cNombreProy, cAno, nUsuarioId);
        }


        


    }
}

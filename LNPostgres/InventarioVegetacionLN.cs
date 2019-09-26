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

        public List<InventarioVegetacion> ListarInventarioVegetacion(InventarioVegetacion oInvVeg)
        {
            return oInventarioVegetacionAD.ListarInventarioVegetacion(oInvVeg);
        }


        


    }
}

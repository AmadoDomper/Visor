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

        public InventarioVegetacion CargaDatosInventarioVegetacion(int nVegId)
        {
            return oInventarioVegetacionAD.CargaDatosInventarioVegetacion(nVegId);
        }

    }
}

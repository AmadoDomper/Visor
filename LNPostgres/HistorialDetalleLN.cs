using System;
using System.Collections.Generic;
using EPostgres;
using ADPostgres;

namespace LNPostgres
{
    public class HistorialDetalleLN
    {
        HistorialDetalleAD oHistoriaDetlAD;

        public HistorialDetalleLN()
        {
            oHistoriaDetlAD = new HistorialDetalleAD();
        }


        public int RegistrarHistorialDetalle(HistorialDetalle oHistDet)
        {
            return oHistoriaDetlAD.RegistrarHistorialDetalle(oHistDet);
        }

    }
}

using System;
using System.Collections.Generic;
using EPostgres;
using EPostgres.Helper;
using ADPostgres;


namespace LNPostgres
{
    public class HistorialLN
    {
        HistorialAD oHistorialAD;

        public HistorialLN()
        {
            oHistorialAD = new HistorialAD();
        }

        public int CrearHistorial(Historial oHistorial)
        {
            return oHistorialAD.CrearHistorial(oHistorial);
        }

        public int GetRecordIdByReferenciaId(int nRefId, TipoReferencia nTipoRef)
        {
            return oHistorialAD.GetRecordIdByReferenciaId(nRefId, nTipoRef);
        }

    }
}

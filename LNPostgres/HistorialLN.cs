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

        public string GetRecordUniqueIdByReferenciaId(int nRefId, TipoReferencia nTipoRef)
        {
            return oHistorialAD.GetRecordUniqueIdByReferenciaId(nRefId, nTipoRef);
        }

        public Historial GetHistorialByReferenciaId(int nRefId, TipoReferencia nTipoRef)
        {
            return oHistorialAD.GetHistorialByReferenciaId(nRefId, nTipoRef);
        }

    }
}

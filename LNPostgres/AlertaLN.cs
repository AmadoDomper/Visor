using System;
using System.Collections.Generic;
using EPostgres;
using ADPostgres;

namespace LNPostgres
{
    public class AlertaLN
    {
        AlertaAD oAlertaAD;

        public AlertaLN()
        {
            oAlertaAD = new AlertaAD();
        }

        public int RegistrarAlerta(Alerta oAlerta)
        {
            return oAlertaAD.RegistrarAlerta(oAlerta);
        }

        public int ActualizaAlertaVisto(int nUsuId)
        {
            return oAlertaAD.ActualizaAlertaVisto(nUsuId);
        }

        public int ActualizaAlertaRevisado(int nUsuId, int nAlertaId)
        {
            return oAlertaAD.ActualizaAlertaRevisado(nUsuId, nAlertaId);
        }

        public Object GetAlertasJSON(int nUsuarioId, int nPage, int nSize)
        {
            return oAlertaAD.GetAlertasJSON(nUsuarioId, nPage, nSize);
        }

    }
}

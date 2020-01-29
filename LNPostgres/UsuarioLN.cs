using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPostgres;
using ADPostgres;



namespace LNPostgres
{
    public class UsuarioLN
    {
        UsuarioAD oUsuarioAD;

        public UsuarioLN()
        {
            oUsuarioAD = new UsuarioAD();
        }

        public ListaPaginada ListarUsuariosPag(int nPage = 1, int nSize = 10, int nUsuId = -1, string cUsuDni = "", string cUsuName = "")
        {
            return oUsuarioAD.ListarUsuariosPag(nPage, nSize ,nUsuId, cUsuDni, cUsuName);
        }

        public Usuario CargarDatosUsuario(int nUsuId)
        {
            return oUsuarioAD.CargarDatosUsuario(nUsuId);
        }

        public string RegistrarModificarUsuario(Usuario oUsuario)
        {
            return oUsuarioAD.RegistrarModificarUsuario(oUsuario);
        }

        public int ActualizaEstadoConfirmacionEmail(string nUniqueId)
        {
            return oUsuarioAD.ActualizaEstadoConfirmacionEmail(nUniqueId);
        }

        public string GenerarNuevoResetId(string cEmail)
        {
            return oUsuarioAD.GenerarNuevoResetId(cEmail);
        }

        public bool IsEstadoPasswordResetActivo(string cResetId = "")
        {
            return oUsuarioAD.IsEstadoPasswordResetActivo(cResetId);
        }

        public int RestablecerPassword(string cResetId, string cPassword)
        {
            return oUsuarioAD.RestablecerPassword(cResetId, cPassword);
        }

        public bool VerificaExisteEmail(string cEmail)
        {
            return oUsuarioAD.VerificaExisteEmail(cEmail);
        }

    }
}

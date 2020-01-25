using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ADPostgres.Helper;
using EPostgres;
using Npgsql;


namespace ADPostgres
{
    public class SeguridadAD
    {
        public Usuario ValidaAccesoUsuario(string  cUsuarioEmail, string cClave)
        {
            var oUsuario = new Usuario();
            var conexion = new ConexionPosgreSQL();
            
            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_validaaccesousuario_email))
                    {
                        cmd.Parameters.AddWithValue("_usu_cemail", cUsuarioEmail);
                        cmd.Parameters.AddWithValue("_usu_cContrasena", cClave);

                        NpgsqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            oUsuario.nUsuarioId = (int)reader["usu_nusuarioid"];
                            oUsuario.cDni = (string)reader["usu_cdni"];
                            oUsuario.cNombres = (string)reader["cnombre"];
                            oUsuario.nRolId = (int)reader["nrolid"];
                            oUsuario.cRolDesc = (string)reader["croldesc"];
                            oUsuario.cEmail = (string)reader["usu_cemail"];
                            oUsuario.bEsInterno = (bool)reader["usu_es_interno"];
                            oUsuario.bEmailConfirmed = (bool)reader["usu_email_confirmed"];
                            oUsuario.nEstado = (int)reader["usu_nestado"];
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return oUsuario;
        }
    }
}

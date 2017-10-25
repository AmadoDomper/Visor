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
        public Usuario ValidaAccesoUsuario(string  cUsuario, string cClave)
        {
            var oUsuario = new Usuario();
            var conexion = new ConexionPosgreSQL();
            
            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_ValidaAccesoUsuario))
                    {
                        cmd.Parameters.AddWithValue("_usu_cdni", cUsuario);
                        cmd.Parameters.AddWithValue("_usu_cContrasena", cClave);

                        NpgsqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            oUsuario.nUsuarioId = Int32.Parse(reader[0].ToString());
                            oUsuario.cDni = reader[1].ToString();
                            oUsuario.cNombres = reader[2].ToString();
                            oUsuario.nRolId = Int32.Parse(reader[3].ToString());
                            oUsuario.cRolDesc = reader[4].ToString();
                            oUsuario.bEsInterno = Boolean.Parse(reader[5].ToString());
                            oUsuario.nEstado = Int32.Parse(reader[6].ToString());
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

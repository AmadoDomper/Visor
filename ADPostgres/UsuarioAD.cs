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
    public class UsuarioAD
    {

        public ListaPaginada ListarUsuariosPag(int nPage = 1, int nSize = 10, int nUsuId = -1, string cUsuDni = "", string cUsuName = "")
        {
            var conexion = new ConexionPosgreSQL();
            var ListaUsuPag = new ListaPaginada();

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_ListarUsuariosPaginado))
                    {
                        cmd.Parameters.AddWithValue("_usu_nUsuarioId", nUsuId);
                        cmd.Parameters.AddWithValue("_usu_cdni", cUsuDni);
                        cmd.Parameters.AddWithValue("_cNombre", cUsuName);
                        cmd.Parameters.AddWithValue("_nPage", nPage);
                        cmd.Parameters.AddWithValue("_nSize", nSize);

                        NpgsqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Usuario oUsuario = new Usuario();

                            oUsuario.nUsuarioId = Int32.Parse(reader[0].ToString());
                            oUsuario.cDni = reader[1].ToString();
                            oUsuario.cNombres = reader[2].ToString();
                            oUsuario.cRolDesc = reader[3].ToString();
                            oUsuario.cTipoUsuario = reader[4].ToString();
                            oUsuario.cEmail = reader[5].ToString();

                            ListaUsuPag.oLista.Add(oUsuario);
                        }
                    }

                    ListaUsuPag.nPage = nPage;
                    ListaUsuPag.nPageSize = nSize;
                    ObtenerPaginadoUsuario(ref ListaUsuPag, nSize, nUsuId, cUsuDni, cUsuName);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return ListaUsuPag;
        }

        public int RegistrarModificarUsuario(Usuario oUsu)
        {
            var conexion = new ConexionPosgreSQL();
            var resultado = -1;

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_Crear_o_ModificarUsuario))
                    {
                        cmd.Parameters.AddWithValue("_usu_nusuarioid", oUsu.nUsuarioId);
                        cmd.Parameters.AddWithValue("_usu_cdni", oUsu.cDni);
                        cmd.Parameters.AddWithValue("_usu_contrasena", oUsu.cContrasena);
                        cmd.Parameters.AddWithValue("_usu_cnombres", oUsu.cNombres);
                        cmd.Parameters.AddWithValue("_usu_capellido_paterno", oUsu.cApellidoPa);
                        cmd.Parameters.AddWithValue("_usu_capellido_materno", oUsu.cApellidoMa);
                        cmd.Parameters.AddWithValue("_usu_cinstitucion", oUsu.cInstitucion);
                        cmd.Parameters.AddWithValue("_usu_cemail", oUsu.cEmail);
                        cmd.Parameters.AddWithValue("_rol_nrolid", oUsu.nRolId);
                        cmd.Parameters.AddWithValue("_usu_es_interno", oUsu.bEsInterno);
                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            resultado = (int)reader["id_val"];
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return resultado;
        }

        public Usuario CargarDatosUsuario(int nUsuId)
        {
            var conexion = new ConexionPosgreSQL();
            var oUsu = new Usuario();

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_CargarDatosUsuario))
                    {
                        cmd.Parameters.AddWithValue("_nUsuId", nUsuId);
                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            oUsu.nUsuarioId = (int)reader["usu_nusuarioid"];
                            oUsu.cNombres = (string)reader["usu_cnombres"];
                            oUsu.cApellidoPa = (string)reader["usu_capellido_paterno"];
                            oUsu.cApellidoMa = (string)reader["usu_capellido_materno"];
                            oUsu.cInstitucion = (string)reader["usu_cinstitucion"];
                            oUsu.cEmail = (string)reader["usu_cemail"];
                            oUsu.cFechaNacimiento = (string)reader["usu_dfechanacimiento"];
                            oUsu.cDni = (string)reader["usu_cdni"];
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oUsu;
        }

        public void ObtenerPaginadoUsuario(ref ListaPaginada oLista, int nSize = 10, int nUsuId = -1, string cUsuDni = null, string cUsuName = null)
        {
            var conexion = new ConexionPosgreSQL();

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_ObtenerPaginadoUsuarios))
                    {
                        cmd.Parameters.AddWithValue("_usu_nUsuarioId", nUsuId);
                        cmd.Parameters.AddWithValue("_usu_cdni", cUsuDni);
                        cmd.Parameters.AddWithValue("_cNombre", cUsuName);
                        cmd.Parameters.AddWithValue("_nSize", nSize);

                        NpgsqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            oLista.nRows = Int32.Parse(reader[0].ToString());
                            oLista.nPageTotal = Int32.Parse(reader[1].ToString());
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

    }
}

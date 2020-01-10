using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ADPostgres.Helper;
using EPostgres;
using EPostgres.Helper;
using Npgsql;

namespace ADPostgres
{
    public class RolAD
    {
        public List<Rol> ListarRoles()
        {
            var conexion = new ConexionPosgreSQL();
            List<Rol> lista = new List<Rol>();

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    DataTable datos = ConexionPosgreSQL.ejecutarDT(Procedimiento.usp_ListarRoles);

                    lista = datos.AsEnumerable().Select(row =>
                        new Rol
                        {
                            nRolId = row.Field<int>("nrolid"),
                            cRolDesc = row.Field<string>("croldesc"),
                        }).ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return lista;
        }

        public string GetSupervisorEmails()
        {
            NpgsqlConnection Conex = new NpgsqlConnection(Conexion.cadena);
            NpgsqlCommand cmd = new NpgsqlCommand(Procedimiento.usp_getSupervisorEmails, Conex);
            cmd.CommandType = CommandType.StoredProcedure;
            Conex.Open();

            string emails = "";

            using (var db = Conex)
            {
                try
                {
                    using (cmd)
                    {
                        emails = cmd.ExecuteScalar().ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return emails;
        }

        /// <summary>
        /// Permite identificar si determinado usuario tiene permiso para la operación ingresada
        /// </summary>
        /// <param name="nUsuarioId">Código único del usuario</param>
        /// <param name="nPermId">Código único del permiso</param>
        /// <returns></returns>
        public bool ValidaPermiso(int nUsuarioId, int nPermId)
        {
            var conexion = new ConexionPosgreSQL();
            bool res = false;

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_ValidarPermiso))
                    {
                        cmd.Parameters.AddWithValue("_UsuarioId", nUsuarioId);
                        cmd.Parameters.AddWithValue("_PermId", nPermId);

                        res = (bool)cmd.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return res;
        }


        public List<Usuario> CargarUsuariosPorRol(RolId nRolId)
        {
            var conexion = new ConexionPosgreSQL();
            var ListUsuario = new List<Usuario>();

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_cargar_usuarios_por_rol))
                    {
                        cmd.Parameters.AddWithValue("_nRolId", (int)nRolId);

                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            var oUsu = new Usuario();

                            oUsu.nUsuarioId = (int)reader["usu_nusuarioid"];
                            oUsu.cNombres = (string)reader["usu_cnombres"];
                            oUsu.cApellidoPa = (string)reader["usu_capellido_paterno"];
                            oUsu.cApellidoMa = (string)reader["usu_capellido_materno"];
                            oUsu.cEmail = (string)reader["usu_cemail"];
                            oUsu.cDni = (string)reader["usu_cdni"];

                            ListUsuario.Add(oUsu);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return ListUsuario;
        }



    }
}

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



    }
}

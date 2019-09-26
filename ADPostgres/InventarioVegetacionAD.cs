using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ADPostgres.Helper;
using EPostgres;
using Npgsql;

namespace ADPostgres
{
    public class InventarioVegetacionAD
    {

        /// <summary>
        /// Registra cabecera para un nuevo inventario de vegetación
        /// </summary>
        /// <param name="oInvVeg"></param>
        /// <returns></returns>
        public int RegistraInventarioVegetacion(InventarioVegetacion oInvVeg)
        {
            var conexion = new ConexionPosgreSQL();
            int id = 0;

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_registrar_inventario_vegetacion))
                    {
                        cmd.Parameters.AddWithValue("_NombreProyecto", oInvVeg.cNombreProyecto);
                        cmd.Parameters.AddWithValue("_AnoColecta", oInvVeg.cAnoColecta);
                        cmd.Parameters.AddWithValue("_FechaRegistro", oInvVeg.dFechaRegistro.ToString("yyyyMMdd"));
                        cmd.Parameters.AddWithValue("_UsuarioId", oInvVeg.nUsuarioId);
                        cmd.Parameters.AddWithValue("_Estado", oInvVeg.nEstado);

                        id = (int)cmd.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return id;
        }

        /// <summary>
        /// Actualiza registro de la cabecera del Inventario de Vegetación
        /// </summary>
        /// <param name="oInvVeg"></param>
        /// <returns></returns>
        public int ActualizaInventarioVegetacion(InventarioVegetacion oInvVeg)
        {
            var conexion = new ConexionPosgreSQL();
            int id = 0;

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_actualiza_inventario_vegetacion))
                    {
                        cmd.Parameters.AddWithValue("_VegetacionId", oInvVeg.nVegetacionId);
                        cmd.Parameters.AddWithValue("_NombreProyecto", oInvVeg.cNombreProyecto);
                        cmd.Parameters.AddWithValue("_AnoColecta", oInvVeg.cAnoColecta);
                        cmd.Parameters.AddWithValue("_FechaActualizacion", oInvVeg.dFechaActualizacion.ToString("yyyyMMdd"));

                        id = (int)cmd.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return id;
        }

        /// <summary>
        /// Elimina registro de Inventario Vegetación modificando su estado a -1
        /// </summary>
        /// <param name="oInvVeg"></param>
        /// <returns></returns>
        public int EliminaInventarioVegetacion(InventarioVegetacion oInvVeg)
        {
            var conexion = new ConexionPosgreSQL();
            int id = 0;

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_elimina_inventario_vegetacion))
                    {
                        cmd.Parameters.AddWithValue("_VegetacionId", oInvVeg.nVegetacionId);
                        cmd.Parameters.AddWithValue("_FechaActualizacion", oInvVeg.dFechaActualizacion.ToString("yyyyMMdd"));

                        id = (int)cmd.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return id;
        }

        /// <summary>
        /// Devuelve los datos de un registro de la cabecera de Inventarios de Vegetación
        /// </summary>
        /// <param name="nInvId"></param>
        /// <returns></returns>
        public InventarioVegetacion CargaDatosInventarioVegetacion(int nVegId)
        {
            var conexion = new ConexionPosgreSQL();
            var oVeg = new InventarioVegetacion();

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_cargar_datos_inventario_vegetacion))
                    {
                        cmd.Parameters.AddWithValue("_vegetacionid", nVegId);
                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            oVeg.nVegetacionId = (int)reader["vegetacionid"];
                            oVeg.cNombreProyecto = (string)reader["nombreproyecto"];
                            oVeg.cAnoColecta = (string)reader["anocolecta"];
                            oVeg.nUsuarioId = (int)reader["usuarioid"];
                            oVeg.nEstado = (int)reader["estado"];
                        }
                    }

                    oVeg.ListaParcelas = new VegetacionParcelaAD().CargaListaInventarioVegetacionParcelas(nVegId);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oVeg;
        }

        public List<InventarioVegetacion> ListarInventarioVegetacion(InventarioVegetacion oInvVeg)
        {
            var conexion = new ConexionPosgreSQL();
            var oListInvVeg = new List<InventarioVegetacion>();


            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_listar_inventario_vegetacion))
                    {
                        cmd.Parameters.AddWithValue("_nombreproyecto", oInvVeg.cNombreProyecto);
                        cmd.Parameters.AddWithValue("_anocolecta", oInvVeg.cAnoColecta);
                        cmd.Parameters.AddWithValue("_estado", oInvVeg.nEstado);

                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            var oInvVegetacion = new InventarioVegetacion();

                            oInvVegetacion.nVegetacionId = (int)reader["vegetacionid"];
                            oInvVegetacion.cNombreProyecto = (string)reader["nombreproyecto"];
                            oInvVegetacion.cAnoColecta = (string)reader["anocolecta"];
                            oInvVegetacion.dFechaRegistro = Convert.ToDateTime(reader["fecharegistro"].ToString());
                            oInvVegetacion.nUsuarioId = (int)reader["usuarioid"];
                            oInvVegetacion.oUsuario = new Usuario();
                            oInvVegetacion.oUsuario.nUsuarioId = (int)reader["usuarioid"];
                            oInvVegetacion.oUsuario.cNombres = (string)reader["usuario_nombres"];
                            oInvVegetacion.oUsuario.cInstitucion = (string)reader["usuario_institucion"];
                            oInvVegetacion.nEstado = (int)reader["estado"];

                            oListInvVeg.Add(oInvVegetacion);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oListInvVeg;
        }




    }
}

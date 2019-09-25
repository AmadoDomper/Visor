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
            var oVeg = new InventarioVegetacion();
            int id = 0;

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_registrar_inventario_vegetacion))
                    {
                        cmd.Parameters.AddWithValue("_NombreProyecto", oInvVeg.cNombreProyecto);
                        cmd.Parameters.AddWithValue("_AnoColecta", oInvVeg.cAnoColecta);
                        cmd.Parameters.AddWithValue("_FechaRegistro", oInvVeg.dFechaRegistro);
                        cmd.Parameters.AddWithValue("_vegetacionid", oInvVeg.nVegetacionId);
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
        /// Método que permite obtener los datos de un registro de la cabecera de Inventarios de Vegetación
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

                    oVeg.ListaParcelas = new VegetacionParcelaAD().CargaDatosInventarioVegetacionParcelas(nVegId);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oVeg;
        }



    }
}

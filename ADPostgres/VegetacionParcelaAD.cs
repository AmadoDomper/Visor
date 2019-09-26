using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ADPostgres.Helper;
using EPostgres;
using Npgsql;

namespace ADPostgres
{
    public class VegetacionParcelaAD
    {

        /// <summary>
        /// Registra una nueva parcela perteneciente a un inventario de Vegetación
        /// </summary>
        /// <param name="oVegPar"></param>
        /// <returns></returns>
        public int RegistraInventarioVegetacionParcela(VegetacionParcela oVegPar)
        {
            var conexion = new ConexionPosgreSQL();
            int id = 0;

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_registrar_inventario_vegetacion_detalle))
                    {
                        cmd.Parameters.AddWithValue("_VegetacionId", oVegPar.nVegetacionId);
                        cmd.Parameters.AddWithValue("_Departamento", oVegPar.cDepartamento);
                        cmd.Parameters.AddWithValue("_Registrador", oVegPar.cRegistrador);
                        cmd.Parameters.AddWithValue("_Longitud", oVegPar.cLongitud);    
                        cmd.Parameters.AddWithValue("_Latitud", oVegPar.cLatitud);
                        cmd.Parameters.AddWithValue("_Wkb", oVegPar.cWkb);
                        cmd.Parameters.AddWithValue("_CodigoMuestra", oVegPar.cCodigoMuestra);
                        cmd.Parameters.AddWithValue("_Altitud", oVegPar.cAltitud);
                        cmd.Parameters.AddWithValue("_Precision", oVegPar.cPrecision);
                        cmd.Parameters.AddWithValue("_TipoVegetacion", oVegPar.cTipoVegetacion);
                        cmd.Parameters.AddWithValue("_ClaseFisonomica", oVegPar.cClaseFisonomica);
                        cmd.Parameters.AddWithValue("_Cobertura", oVegPar.cCobertura);
                        cmd.Parameters.AddWithValue("_ConfianzaClasificacion", oVegPar.cConfianzaClasificacion);
                        cmd.Parameters.AddWithValue("_ClaseHidrologica", oVegPar.cClaseHidrologica);
                        cmd.Parameters.AddWithValue("_Fisiografia", oVegPar.cFisiografia);
                        cmd.Parameters.AddWithValue("_AltitudSistemaHidrico", oVegPar.cAltitudSistemaHidrico);

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
        /// Actualiza registro de detalle (Parcela) de un inventario de Vegetación
        /// </summary>
        /// <param name="oVegPar"></param>
        /// <returns></returns>
        public int ActualizaInventarioVegetacionParcela(VegetacionParcela oVegPar)
        {
            var conexion = new ConexionPosgreSQL();
            int id = 0;

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_actualiza_inventario_vegetacion_detalle))
                    {
                        cmd.Parameters.AddWithValue("_VegetacionId", oVegPar.nVegetacionId);
                        cmd.Parameters.AddWithValue("_ParcelaId", oVegPar.nParcelaId);
                        cmd.Parameters.AddWithValue("_Departamento", oVegPar.cDepartamento);
                        cmd.Parameters.AddWithValue("_Registrador", oVegPar.cRegistrador);
                        cmd.Parameters.AddWithValue("_Longitud", oVegPar.cLongitud);
                        cmd.Parameters.AddWithValue("_Latitud", oVegPar.cLatitud);
                        cmd.Parameters.AddWithValue("_Wkb", oVegPar.cWkb);
                        cmd.Parameters.AddWithValue("_CodigoMuestra", oVegPar.cCodigoMuestra);
                        cmd.Parameters.AddWithValue("_Altitud", oVegPar.cAltitud);
                        cmd.Parameters.AddWithValue("_Precision", oVegPar.cPrecision);
                        cmd.Parameters.AddWithValue("_TipoVegetacion", oVegPar.cTipoVegetacion);
                        cmd.Parameters.AddWithValue("_ClaseFisonomica", oVegPar.cClaseFisonomica);
                        cmd.Parameters.AddWithValue("_Cobertura", oVegPar.cCobertura);
                        cmd.Parameters.AddWithValue("_ConfianzaClasificacion", oVegPar.cConfianzaClasificacion);
                        cmd.Parameters.AddWithValue("_ClaseHidrologica", oVegPar.cClaseHidrologica);
                        cmd.Parameters.AddWithValue("_Fisiografia", oVegPar.cFisiografia);
                        cmd.Parameters.AddWithValue("_AltitudSistemaHidrico", oVegPar.cAltitudSistemaHidrico);

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
        /// Elimina registro detalle (Parcela) de inventario de vegetación
        /// </summary>
        /// <param name="oVegPar"></param>
        /// <returns></returns>
        public int EliminaInventarioVegetacionParcela(VegetacionParcela oVegPar)
        {
            var conexion = new ConexionPosgreSQL();
            int id = 0;

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_elimina_inventario_vegetacion_detalle))
                    {
                        cmd.Parameters.AddWithValue("_VegetacionId", oVegPar.nVegetacionId);
                        cmd.Parameters.AddWithValue("_ParcelaId", oVegPar.nParcelaId);

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
        /// Devuelve la lista de las parcelas registradas para un determinado Inventario de Vegetación
        /// </summary>
        /// <param name="nVegId">Id Inventario Vegetación</param>
        /// <returns></returns>
        public List<VegetacionParcela> CargaListaInventarioVegetacionParcelas(int nVegId)
        {
            var conexion = new ConexionPosgreSQL();
            var oListVegetacionParcela = new List<VegetacionParcela>();
            

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_lista_inventario_vegetacion_detalle))
                    {
                        cmd.Parameters.AddWithValue("_vegetacionid", nVegId);
                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            var oVegPar = new VegetacionParcela();

                            oVegPar.nVegetacionId = (int)reader["VegetacionId"];
                            oVegPar.nParcelaId = (int)reader["ParcelaId"];
                            oVegPar.cDepartamento = (string)reader["Departamento"];
                            oVegPar.cRegistrador = (string)reader["Registrador"];
                            oVegPar.cLongitud = (string)reader["Longitud"];
                            oVegPar.cLatitud = (string)reader["Latitud"];
                            oVegPar.cWkb = (string)reader["Wkb"];
                            oVegPar.cCodigoMuestra = (string)reader["CodigoMuestra"];
                            oVegPar.cAltitud = (string)reader["Altitud"];
                            oVegPar.cPrecision = (string)reader["Precision"];
                            oVegPar.cTipoVegetacion = (string)reader["TipoVegetacion"];
                            oVegPar.cClaseFisonomica = (string)reader["ClaseFisonomica"];
                            oVegPar.cCobertura = (string)reader["Cobertura"];
                            oVegPar.cConfianzaClasificacion = (string)reader["ConfianzaClasificacion"];
                            oVegPar.cClaseHidrologica = (string)reader["ClaseHidrologica"];
                            oVegPar.cFisiografia = (string)reader["Fisiografia"];
                            oVegPar.cAltitudSistemaHidrico = (string)reader["AltitudSistemaHidrico"];
                            oVegPar.nEstado = (int)reader["Estado"];

                            oListVegetacionParcela.Add(oVegPar);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oListVegetacionParcela;
        }

        public VegetacionParcela CargaInventarioVegetacionParcela(int nParId)
        {
            var conexion = new ConexionPosgreSQL();
            var oVegPar = new VegetacionParcela();


            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_cargar_datos_inventario_vegetacion_detalle))
                    {
                        cmd.Parameters.AddWithValue("_ParcelaId", nParId);
                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            oVegPar.nVegetacionId = (int)reader["VegetacionId"];
                            oVegPar.nParcelaId = (int)reader["ParcelaId"];
                            oVegPar.cDepartamento = (string)reader["Departamento"];
                            oVegPar.cRegistrador = (string)reader["Registrador"];
                            oVegPar.cLongitud = (string)reader["Longitud"];
                            oVegPar.cLatitud = (string)reader["Latitud"];
                            oVegPar.cWkb = (string)reader["Wkb"];
                            oVegPar.cCodigoMuestra = (string)reader["CodigoMuestra"];
                            oVegPar.cAltitud = (string)reader["Altitud"];
                            oVegPar.cPrecision = (string)reader["Precision"];
                            oVegPar.cTipoVegetacion = (string)reader["TipoVegetacion"];
                            oVegPar.cClaseFisonomica = (string)reader["ClaseFisonomica"];
                            oVegPar.cCobertura = (string)reader["Cobertura"];
                            oVegPar.cConfianzaClasificacion = (string)reader["ConfianzaClasificacion"];
                            oVegPar.cClaseHidrologica = (string)reader["ClaseHidrologica"];
                            oVegPar.cFisiografia = (string)reader["Fisiografia"];
                            oVegPar.cAltitudSistemaHidrico = (string)reader["AltitudSistemaHidrico"];
                            oVegPar.nEstado = (int)reader["Estado"];

                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oVegPar;
        }

    }
}

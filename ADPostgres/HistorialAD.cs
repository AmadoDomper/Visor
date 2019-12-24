using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ADPostgres.Helper;
using EPostgres;
using EPostgres.Helper;
using Npgsql;

namespace ADPostgres
{
    public class HistorialAD
    {
        public int CrearHistorial(Historial oHistorial)
        {
            var conexion = new ConexionPosgreSQL();
            int id = 0;

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_crear_historial))
                    {
                        cmd.Parameters.AddWithValue("_refid", oHistorial.nRefId);
                        cmd.Parameters.AddWithValue("_tiporeferencia", oHistorial.nTipoReferencia);
                        cmd.Parameters.AddWithValue("_fechacreacion", oHistorial.dFechaCreacion.ToString("yyyyMMdd"));
                        cmd.Parameters.AddWithValue("_ultimaactualizacion", oHistorial.dFechaActualizacion.ToString("yyyyMMdd"));
                        cmd.Parameters.AddWithValue("_estado", oHistorial.nEstado);

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

        public string GetRecordUniqueIdByReferenciaId(int nRefId, TipoReferencia nTipoRef)
        {
            var conexion = new ConexionPosgreSQL();
            string id = "";

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_get_recordid_by_refid))
                    {
                        cmd.Parameters.AddWithValue("_nRefId", nRefId);
                        cmd.Parameters.AddWithValue("_nTipoRef", (int)nTipoRef);
                        
                        id = (string)cmd.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return id;
        }

        public Historial GetHistorialByReferenciaId(int nRefId, TipoReferencia nTipoRef)
        {
            var conexion = new ConexionPosgreSQL();
            var oHist = new Historial();

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_get_historial_by_refid))
                    {
                        cmd.Parameters.AddWithValue("_nRefId", nRefId);
                        cmd.Parameters.AddWithValue("_nTipoRef", (int)nTipoRef);

                        NpgsqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            oHist.nHistorialId = (int)reader["historialid"];
                            oHist.nRefId = (int)reader["refid"];
                            oHist.nTipoReferencia = (int)reader["tiporeferencia"];
                            oHist.dFechaCreacion = Convert.ToDateTime(reader["fechacreacion"].ToString());
                            oHist.nEstado = (int)reader["estado"];
                            oHist.cUniqueId = (string)reader["unique_id"];
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oHist;
        }
    }
}

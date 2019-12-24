using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ADPostgres.Helper;
using EPostgres;
using Npgsql;
using Newtonsoft.Json;

namespace ADPostgres
{
    public class HistorialDetalleAD
    {
        public int RegistrarHistorialDetalle(HistorialDetalle oHistDet)
        {
            var conexion = new ConexionPosgreSQL();
            int id = 0;

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_registrar_detalle_historial))
                    {
                        cmd.Parameters.AddWithValue("_historialid", oHistDet.nHistorialId);
                        cmd.Parameters.AddWithValue("_usuario_registra", oHistDet.nUsuarioRegistra);
                        cmd.Parameters.AddWithValue("_fecha_registro", oHistDet.dFechaRegistro.ToString("yyyyMMdd"));
                        cmd.Parameters.AddWithValue("_descripcion", oHistDet.cDescripcion);
                        cmd.Parameters.AddWithValue("_estado", oHistDet.Estado);

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

        public Object GetHistorialDetalleJSON(string cId)
        {
            NpgsqlConnection Conex = new NpgsqlConnection(Conexion.cadena);
            NpgsqlCommand cmd = new NpgsqlCommand(Procedimiento.usp_get_historial_det, Conex);
            cmd.CommandType = CommandType.StoredProcedure;
            Conex.Open();

            Object json;

            using (var db = Conex)
            {
                try
                {
                    using (cmd)
                    {
                        cmd.Parameters.AddWithValue("_cId", cId);

                        json = JsonConvert.DeserializeObject(cmd.ExecuteScalar().ToString());
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return json;
        }
    }
}

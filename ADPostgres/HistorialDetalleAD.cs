using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ADPostgres.Helper;
using EPostgres;
using Npgsql;

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
    }
}

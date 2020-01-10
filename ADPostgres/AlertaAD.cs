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
    public class AlertaAD
    {
        public int RegistrarAlerta(Alerta oAlerta)
        {
            var conexion = new ConexionPosgreSQL();
            int id = 0;

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_registrar_alerta))
                    {
                        cmd.Parameters.AddWithValue("_usu_nusuarioid", oAlerta.nUsuarioId);
                        cmd.Parameters.AddWithValue("_titulo", oAlerta.cTitulo);
                        cmd.Parameters.AddWithValue("_mensaje", oAlerta.cMensaje);
                        cmd.Parameters.AddWithValue("_url", oAlerta.cUrl);
                        cmd.Parameters.AddWithValue("_alert_icon", oAlerta.cAlertaIcono);
                        cmd.Parameters.AddWithValue("_alert_color", oAlerta.cAlertaColor);
                        cmd.Parameters.AddWithValue("_estado", oAlerta.nEstado);

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


        public int ActualizaAlertaVisto(int nUsuId)
        {
            var conexion = new ConexionPosgreSQL();
            int id = 0;

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_actualiza_alerta_visto))
                    {
                        cmd.Parameters.AddWithValue("_usu_nusuarioid", nUsuId);

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

        public int ActualizaAlertaRevisado(int nUsuId, int nAlertaId)
        {
            var conexion = new ConexionPosgreSQL();
            int id = 0;

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_actualiza_alerta_revisado))
                    {
                        cmd.Parameters.AddWithValue("_usu_nusuarioid", nUsuId);
                        cmd.Parameters.AddWithValue("_alertaid", nAlertaId);

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

        public Object GetAlertasJSON(int nUsuarioId, int nPage, int nSize)
        {
            NpgsqlConnection Conex = new NpgsqlConnection(Conexion.cadena);
            NpgsqlCommand cmd = new NpgsqlCommand(Procedimiento.usp_get_alertas, Conex);
            cmd.CommandType = CommandType.StoredProcedure;
            Conex.Open();

            Object json;

            using (var db = Conex)
            {
                try
                {
                    using (cmd)
                    {
                        cmd.Parameters.AddWithValue("_nusuarioid", nUsuarioId);
                        cmd.Parameters.AddWithValue("_npage", nPage);
                        cmd.Parameters.AddWithValue("_nsize", nSize);

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

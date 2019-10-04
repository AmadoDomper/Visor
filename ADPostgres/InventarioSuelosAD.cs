using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ADPostgres.Helper;
using EPostgres;
using Npgsql;


namespace ADPostgres
{
    public class InventarioSuelosAD
    {

        /// <summary>
        /// Registra cabecera para un nuevo inventario de suelos
        /// </summary>
        /// <param name="oInvSue"></param>
        /// <returns></returns>
        public int RegistraInventarioSuelos(InventarioSuelos oInvSue)
        {
            var conexion = new ConexionPosgreSQL();
            int id = 0;

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_registrar_inventario_suelos))
                    {
                        cmd.Parameters.AddWithValue("_NombreProyecto", oInvSue.cNombreProyecto);
                        cmd.Parameters.AddWithValue("_AnoColecta", oInvSue.cAnoColecta);
                        cmd.Parameters.AddWithValue("_NombreColector", oInvSue.cNombreColector);
                        cmd.Parameters.AddWithValue("_FechaRegistro", oInvSue.dFechaRegistro.ToString("yyyyMMdd"));
                        cmd.Parameters.AddWithValue("_UsuarioId", oInvSue.nUsuarioId);
                        cmd.Parameters.AddWithValue("_Estado", oInvSue.nEstado);

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
        /// Actualiza registro de la cabecera del Inventario de Suelos
        /// </summary>
        /// <param name="oInvSue"></param>
        /// <returns></returns>
        public int ActualizaInventarioSuelos(InventarioSuelos oInvSue)
        {
            var conexion = new ConexionPosgreSQL();
            int id = 0;

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_actualiza_inventario_suelos))
                    {
                        cmd.Parameters.AddWithValue("_SuelosId", oInvSue.nSuelosId);
                        cmd.Parameters.AddWithValue("_NombreProyecto", oInvSue.cNombreProyecto);
                        cmd.Parameters.AddWithValue("_AnoColecta", oInvSue.cAnoColecta);
                        cmd.Parameters.AddWithValue("_NombreColector", oInvSue.cNombreColector);
                        cmd.Parameters.AddWithValue("_FechaActualizacion", oInvSue.dFechaActualizacion.ToString("yyyyMMdd"));

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
        /// Elimina registro de Inventario Suelos modificando su estado a -1
        /// </summary>
        /// <param name="oInvSue"></param>
        /// <returns></returns>
        public int EliminaInventarioSuelos(InventarioSuelos oInvSue)
        {
            var conexion = new ConexionPosgreSQL();
            int id = 0;

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_elimina_inventario_suelos))
                    {
                        cmd.Parameters.AddWithValue("_SuelosId", oInvSue.nSuelosId);
                        cmd.Parameters.AddWithValue("_FechaActualizacion", oInvSue.dFechaActualizacion.ToString("yyyyMMdd"));

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
        /// Devuelve los datos de un registro de la cabecera de Inventarios de Suelos
        /// </summary>
        /// <param name="nSueId"></param>
        /// <returns></returns>
        public InventarioSuelos CargaDatosInventarioSuelos(int nSueId)
        {
            var conexion = new ConexionPosgreSQL();
            var oSue= new InventarioSuelos();

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_cargar_datos_inventario_suelos))
                    {
                        cmd.Parameters.AddWithValue("_suelosid", nSueId);
                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            oSue.nSuelosId = (int)reader["suelosid"];
                            oSue.cNombreProyecto = (string)reader["nombreproyecto"];
                            oSue.cAnoColecta = (string)reader["anocolecta"];
                            oSue.cNombreColector = (string)reader["nombrecolector"];
                            oSue.nUsuarioId = (int)reader["usuarioid"];
                            oSue.nEstado = (int)reader["estado"];
                        }
                    }

                    oSue.ListaPerfilModal = new SuelosPerfilModalAD().CargaListaInventarioSuelosPerfilModal(nSueId);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oSue;
        }

        public ListaPaginada ListarMisInventariosSuelosPag(int nInvEst, int nPage = 1, int nSize = 10, string cNombreProy = "", string cAno = "", string cNombreColector = "", int nUsuarioId = 0)
        {
            var conexion = new ConexionPosgreSQL();
            ListaPaginada ListaInvPag = new ListaPaginada();


            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_listar_mis_inventarios_suelos_paginado))
                    {
                        cmd.Parameters.AddWithValue("_nombreproyecto", cNombreProy);
                        cmd.Parameters.AddWithValue("_anocolecta", cAno);
                        cmd.Parameters.AddWithValue("_nombrecolector", cNombreColector);
                        cmd.Parameters.AddWithValue("_usuarioId", nUsuarioId);
                        cmd.Parameters.AddWithValue("_estado", nInvEst);
                        cmd.Parameters.AddWithValue("_npage", nPage);
                        cmd.Parameters.AddWithValue("_nsize", nSize);

                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            var oInvSuelos = new InventarioSuelos();

                            oInvSuelos.nSuelosId = (int)reader["suelosid"];
                            oInvSuelos.cNombreProyecto = (string)reader["nombreproyecto"];
                            oInvSuelos.cAnoColecta = (string)reader["anocolecta"];
                            oInvSuelos.cNombreColector = (string)reader["nombrecolector"];
                            oInvSuelos.dFechaRegistro = Convert.ToDateTime(reader["fecharegistro"].ToString());
                            oInvSuelos.cFechaRegistro = Convert.ToDateTime(reader["fecharegistro"].ToString()).ToString("dd/MM/yyyy");
                            oInvSuelos.nUsuarioId = (int)reader["usuarioid"];
                            oInvSuelos.oUsuario = new Usuario();
                            oInvSuelos.oUsuario.nUsuarioId = (int)reader["usuarioid"];
                            oInvSuelos.oUsuario.cNombres = (string)reader["usuario_nombres"];
                            oInvSuelos.oUsuario.cInstitucion = (string)reader["usuario_institucion"];
                            oInvSuelos.nEstado = (int)reader["estado"];

                            ListaInvPag.oLista.Add(oInvSuelos);
                        }
                    }

                    ListaInvPag.nPage = nPage;
                    ListaInvPag.nPageSize = nSize;
                    ObtenerPaginadoMisInventariosSuelos(nInvEst, ref ListaInvPag, nSize, cNombreProy, cAno, cNombreColector, nUsuarioId);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return ListaInvPag;
        }

        public void ObtenerPaginadoMisInventariosSuelos(int nInvEst, ref ListaPaginada oLista, int nSize = 10, string cNombreProy = "", string cAno = "", string cNombreColector = "", int nUsuarioId = 0)
        {
            var conexion = new ConexionPosgreSQL();

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_obtener_mis_inventarios_suelos_cantpag))
                    {
                        cmd.Parameters.AddWithValue("_nombreproyecto", cNombreProy);
                        cmd.Parameters.AddWithValue("_anocolecta", cAno);
                        cmd.Parameters.AddWithValue("_nombrecolector", cNombreColector);
                        cmd.Parameters.AddWithValue("_usuarioId", nUsuarioId);
                        cmd.Parameters.AddWithValue("_estado", nInvEst);
                        cmd.Parameters.AddWithValue("_nsize", nSize);

                        NpgsqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            oLista.nRows = Int32.Parse(reader[0].ToString());
                            oLista.nPageTotal = Int32.Parse(reader[1].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


    }
}

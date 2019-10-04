using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ADPostgres.Helper;
using EPostgres;
using Npgsql;

namespace ADPostgres
{
    public class SuelosPerfilModalAD
    {

        /// <summary>
        /// Registra un nuevo Perfil Modal perteneciente a un inventario de Suelos
        /// </summary>
        /// <param name="oSuePerMod"></param>
        /// <returns></returns>
        public int RegistraInventarioSuelosPerfilModal(SuelosPerfilModal oSuePerMod)
        {
            var conexion = new ConexionPosgreSQL();
            int id = 0;

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_registrar_inventario_suelos_detalle))
                    {
                        cmd.Parameters.AddWithValue("_SuelosId", oSuePerMod.nSuelosId);
                        cmd.Parameters.AddWithValue("_Departamento", oSuePerMod.cDepartamento);
                        cmd.Parameters.AddWithValue("_Longitud", oSuePerMod.cLongitud);
                        cmd.Parameters.AddWithValue("_Latitud", oSuePerMod.cLatitud);
                        cmd.Parameters.AddWithValue("_Wkb", oSuePerMod.cLongitud + " " + oSuePerMod.cLatitud);
                        cmd.Parameters.AddWithValue("_PerfilModal", oSuePerMod.cPerfilModal);
                        cmd.Parameters.AddWithValue("_NroCalicata", oSuePerMod.cNroCalicata);
                        cmd.Parameters.AddWithValue("_Msnm", oSuePerMod.cMsnm);
                        cmd.Parameters.AddWithValue("_ZonaMuestreo", oSuePerMod.cZonaMuestreo);
                        cmd.Parameters.AddWithValue("_ClasificacionNatural", oSuePerMod.cClasificacionNatural);
                        cmd.Parameters.AddWithValue("_Fisiografia", oSuePerMod.cFisiografia);
                        cmd.Parameters.AddWithValue("_Pendiente", oSuePerMod.cPendiente);
                        cmd.Parameters.AddWithValue("_Relieve", oSuePerMod.cRelieve);
                        cmd.Parameters.AddWithValue("_Clima", oSuePerMod.cClima);
                        cmd.Parameters.AddWithValue("_ZonaVida", oSuePerMod.cZonaVida);
                        cmd.Parameters.AddWithValue("_MaterialParental", oSuePerMod.cMaterialParental);
                        cmd.Parameters.AddWithValue("_Vegetacion", oSuePerMod.cVegetacion);
                        cmd.Parameters.AddWithValue("_ModoColecta", oSuePerMod.cModoColecta);
                        cmd.Parameters.AddWithValue("_Drenaje", oSuePerMod.cDrenaje);
                        cmd.Parameters.AddWithValue("_ProfundidadEfectiva", oSuePerMod.cProfundidadEfectiva);
                        cmd.Parameters.AddWithValue("_FactorLimitante", oSuePerMod.cFactorLimitante);

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
        /// Actualiza registro de detalle (Perfil Modal) de un inventario de Suelos
        /// </summary>
        /// <param name="oSuePerMod"></param>
        /// <returns></returns>
        public int ActualizaInventarioSuelosPerfilModal(SuelosPerfilModal oSuePerMod)
        {
            var conexion = new ConexionPosgreSQL();
            int id = 0;

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_actualiza_inventario_suelos_detalle))
                    {
                        cmd.Parameters.AddWithValue("_SuelosId", oSuePerMod.nSuelosId);
                        cmd.Parameters.AddWithValue("_PerfilId", oSuePerMod.nPerfilId);
                        cmd.Parameters.AddWithValue("_Departamento", oSuePerMod.cDepartamento);
                        cmd.Parameters.AddWithValue("_Longitud", oSuePerMod.cLongitud);
                        cmd.Parameters.AddWithValue("_Latitud", oSuePerMod.cLatitud);
                        cmd.Parameters.AddWithValue("_Wkb", oSuePerMod.cLongitud + " " + oSuePerMod.cLatitud);
                        cmd.Parameters.AddWithValue("_PerfilModal", oSuePerMod.cPerfilModal);
                        cmd.Parameters.AddWithValue("_NroCalicata", oSuePerMod.cNroCalicata);
                        cmd.Parameters.AddWithValue("_Msnm", oSuePerMod.cMsnm);
                        cmd.Parameters.AddWithValue("_ZonaMuestreo", oSuePerMod.cZonaMuestreo);
                        cmd.Parameters.AddWithValue("_ClasificacionNatural", oSuePerMod.cClasificacionNatural);
                        cmd.Parameters.AddWithValue("_Fisiografia", oSuePerMod.cFisiografia);
                        cmd.Parameters.AddWithValue("_Pendiente", oSuePerMod.cPendiente);
                        cmd.Parameters.AddWithValue("_Relieve", oSuePerMod.cRelieve);
                        cmd.Parameters.AddWithValue("_Clima", oSuePerMod.cClima);
                        cmd.Parameters.AddWithValue("_ZonaVida", oSuePerMod.cZonaVida);
                        cmd.Parameters.AddWithValue("_MaterialParental", oSuePerMod.cMaterialParental);
                        cmd.Parameters.AddWithValue("_Vegetacion", oSuePerMod.cVegetacion);
                        cmd.Parameters.AddWithValue("_ModoColecta", oSuePerMod.cModoColecta);
                        cmd.Parameters.AddWithValue("_Drenaje", oSuePerMod.cDrenaje);
                        cmd.Parameters.AddWithValue("_ProfundidadEfectiva", oSuePerMod.cProfundidadEfectiva);
                        cmd.Parameters.AddWithValue("_FactorLimitante", oSuePerMod.cFactorLimitante);

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
        /// Elimina registro detalle (Perfil Modal) de inventario de suelos
        /// </summary>
        /// <param name="oSuePerMod"></param>
        /// <returns></returns>
        public int EliminaInventarioSuelosPerfilModal(SuelosPerfilModal oSuePerMod)
        {
            var conexion = new ConexionPosgreSQL();
            int id = 0;

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_elimina_inventario_suelos_detalle))
                    {
                        cmd.Parameters.AddWithValue("_SuelosId", oSuePerMod.nSuelosId);
                        cmd.Parameters.AddWithValue("_PerfilId", oSuePerMod.nPerfilId);

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
        /// <param name="nSueId">Id Inventario Vegetación</param>
        /// <returns></returns>
        public List<SuelosPerfilModal> CargaListaInventarioSuelosPerfilModal(int nSueId)
        {
            var conexion = new ConexionPosgreSQL();
            var oListSuelosPerfilModal = new List<SuelosPerfilModal>();


            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_lista_inventario_suelos_detalle))
                    {
                        cmd.Parameters.AddWithValue("_SuelosId", nSueId);
                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            var oSuePerMod = new SuelosPerfilModal();

                            oSuePerMod.nSuelosId = (int)reader["SuelosId"];
                            oSuePerMod.nPerfilId = (int)reader["PerfilId"];
                            oSuePerMod.cDepartamento = (string)reader["Departamento"];
                            oSuePerMod.cLongitud = (string)reader["Longitud"];
                            oSuePerMod.cLatitud = (string)reader["Latitud"];
                            oSuePerMod.cWkb = (string)reader["Wkb"];
                            oSuePerMod.cPerfilModal = (string)reader["PerfilModal"];
                            oSuePerMod.cNroCalicata = (string)reader["NroCalicata"];
                            oSuePerMod.cMsnm = (string)reader["Msnm"];
                            oSuePerMod.cZonaMuestreo = (string)reader["ZonaMuestreo"];
                            oSuePerMod.cClasificacionNatural = (string)reader["ClasificacionNatural"];
                            oSuePerMod.cFisiografia = (string)reader["Fisiografia"];
                            oSuePerMod.cPendiente = (string)reader["Pendiente"];
                            oSuePerMod.cRelieve = (string)reader["Relieve"];
                            oSuePerMod.cClima = (string)reader["Clima"];
                            oSuePerMod.cZonaVida = (string)reader["ZonaVida"];
                            oSuePerMod.cMaterialParental = (string)reader["MaterialParental"];
                            oSuePerMod.cVegetacion = (string)reader["Vegetacion"];
                            oSuePerMod.cModoColecta = (string)reader["ModoColecta"];
                            oSuePerMod.cDrenaje = (string)reader["Drenaje"];
                            oSuePerMod.cProfundidadEfectiva = (string)reader["ProfundidadEfectiva"];
                            oSuePerMod.cFactorLimitante = (string)reader["FactorLimitante"];
                            oSuePerMod.nEstado = (int)reader["Estado"];

                            oListSuelosPerfilModal.Add(oSuePerMod);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oListSuelosPerfilModal;
        }

        public SuelosPerfilModal CargaInventarioSuelosPerfilModal(int nPerModId)
        {
            var conexion = new ConexionPosgreSQL();
            var oSuePerMod = new SuelosPerfilModal();


            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_cargar_datos_inventario_suelos_detalle))
                    {
                        cmd.Parameters.AddWithValue("_PerfilId", nPerModId);
                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            oSuePerMod.nSuelosId = (int)reader["SuelosId"];
                            oSuePerMod.nPerfilId = (int)reader["PerfilId"];
                            oSuePerMod.cDepartamento = (string)reader["Departamento"];
                            oSuePerMod.cLongitud = (string)reader["Longitud"];
                            oSuePerMod.cLatitud = (string)reader["Latitud"];
                            oSuePerMod.cWkb = (string)reader["Wkb"];
                            oSuePerMod.cPerfilModal = (string)reader["PerfilModal"];
                            oSuePerMod.cNroCalicata = (string)reader["NroCalicata"];
                            oSuePerMod.cMsnm = (string)reader["Msnm"];
                            oSuePerMod.cZonaMuestreo = (string)reader["ZonaMuestreo"];
                            oSuePerMod.cClasificacionNatural = (string)reader["ClasificacionNatural"];
                            oSuePerMod.cFisiografia = (string)reader["Fisiografia"];
                            oSuePerMod.cPendiente = (string)reader["Pendiente"];
                            oSuePerMod.cRelieve = (string)reader["Relieve"];
                            oSuePerMod.cClima = (string)reader["Clima"];
                            oSuePerMod.cZonaVida = (string)reader["ZonaVida"];
                            oSuePerMod.cMaterialParental = (string)reader["MaterialParental"];
                            oSuePerMod.cVegetacion = (string)reader["Vegetacion"];
                            oSuePerMod.cModoColecta = (string)reader["ModoColecta"];
                            oSuePerMod.cDrenaje = (string)reader["Drenaje"];
                            oSuePerMod.cProfundidadEfectiva = (string)reader["ProfundidadEfectiva"];
                            oSuePerMod.cFactorLimitante = (string)reader["FactorLimitante"];
                            oSuePerMod.nEstado = (int)reader["Estado"];
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oSuePerMod;
        }

    }
}

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

        public List<VegetacionParcela> CargaDatosInventarioVegetacionParcelas(int nVegId)
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

    }
}

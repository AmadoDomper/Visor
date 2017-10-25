using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ADPostgres.Helper;
using EPostgres;
using Npgsql;

namespace ADPostgres
{
    public class ConfiguracionAD
    {
        public List<Menu> ListarMenuRol(int nRolId)
        {
            var conexion = new ConexionPosgreSQL();
            var lista = new List<Menu>();

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_ListarMenuRol);
                    cmd.Parameters.AddWithValue("nRolId", nRolId);

                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                    DataTable datos = new DataTable();
                    da.Fill(datos);

                    lista = datos.AsEnumerable().Select(row =>
                        new Menu
                        {
                            nMenuId = row.Field<int>("nMenuId"),
                            cMenuDesc = row.Field<string>("cMenuDesc"),
                            bEstado = row.Field<bool>("nValor")
                        }).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return lista;
        }

        public List<Modulo> ListarModuloRol(int nRolId)
        {
            var conexion = new ConexionPosgreSQL();
            var lista = new List<Modulo>();

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_ListarModuloRol);
                    cmd.Parameters.AddWithValue("_nRolId", nRolId);

                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                    DataTable datos = new DataTable();
                    da.Fill(datos);

                    lista = datos.AsEnumerable().Select(row =>
                        new Modulo
                        {
                            nMenuId = row.Field<int>("nMenuId"),
                            nModId = row.Field<int>("nModId"),
                            cModDesc = row.Field<string>("cModDesc"),
                            bEstado = row.Field<bool>("nValor")
                        }).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return lista;
        }

        public List<Permiso> ListarPermisoRol(int nRolId)
        {
            var conexion = new ConexionPosgreSQL();
            var lista = new List<Permiso>();

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_ListarPermisoRol);
                    cmd.Parameters.AddWithValue("_nRolId", nRolId);

                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                    DataTable datos = new DataTable();
                    da.Fill(datos);

                    lista = datos.AsEnumerable().Select(row =>
                        new Permiso
                        {
                            nModId = row.Field<int>("nModId"),
                            nPermId = row.Field<int>("nPermId"),
                            cPermDesc = row.Field<string>("cPermDesc"),
                            bEstado = row.Field<bool>("nValor")
                        }).ToList();                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return lista;
        }

        public int RegistrarActualizarRolPermisos(int nRolId, string cRolDesc, List<Menu> ListMenu, List<Modulo> ListModulo, List<Permiso> ListaPermiso)
        {
            int resultado = -1;
            try
            {
                if (nRolId == 0) //Nuevo
                {
                    resultado = RegistrarNuevoRolPermisos(cRolDesc, ListMenu, ListModulo, ListaPermiso);
                }
                else
                {
                    resultado = ActualizarRolPermisos(nRolId, ListMenu, ListModulo, ListaPermiso);
                }
            }
            catch (Exception ex)
            {
                resultado = -1;
            }
            return resultado;
        }


        public int RegistrarNuevoRolPermisos(string cRolDesc, List<Menu> ListMenu, List<Modulo> ListModulo, List<Permiso> ListaPermiso)
        {
            var conexion = new ConexionPosgreSQL();
            int resultado = -1;

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    int nNuevoRolId = 0;
                    string sql = "";

                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_CrearRol))
                    {
                        cmd.Parameters.AddWithValue("cRolDesc", cRolDesc);

                        NpgsqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            nNuevoRolId = Int32.Parse(reader[0].ToString());
                        }

                        foreach (Menu oMenu in ListMenu)
                        {
                            if (oMenu.bEstado == false)
                            {
                                sql += "INSERT INTO MenuDenegadoPorRol(nRolId,nMenuId)";
                                sql += String.Format(" VALUES ({0},{1});", nNuevoRolId, oMenu.nMenuId);
                            }
                        }

                        foreach (Modulo oModulo in ListModulo)
                        {
                            if (oModulo.bEstado == false)
                            {
                                sql += "INSERT INTO ModuloDenegadoPorRol(nRolId,nModId)";
                                sql += String.Format(" VALUES ({0},{1});", nNuevoRolId, oModulo.nModId);
                            }
                        }

                        foreach (Permiso oPermiso in ListaPermiso)
                        {
                            if (oPermiso.bEstado == false)
                            {
                                sql += "INSERT INTO PermisoDenegadoPorRol(nRolId,nPermId)";
                                sql += String.Format(" VALUES ({0},{1});", nNuevoRolId, oPermiso.nPermId);
                            }
                        }

                        ConexionPosgreSQL.ejecutar(sql);
                    }

                    resultado = nNuevoRolId;
                }
                catch (Exception ex)
                {
                    resultado = -1;
                }
            }
            return resultado;
        }

        public int ActualizarRolPermisos(int nRolId, List<Menu> ListMenu, List<Modulo> ListModulo, List<Permiso> ListaPermiso)
        {
            var conexion = new ConexionPosgreSQL();
            int resultado = -1;

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    string sql = "";

                    sql += "DELETE FROM MenuDenegadoPorRol";
                    sql += String.Format(" WHERE nRolId = {0};", nRolId);
                    sql += "DELETE FROM ModuloDenegadoPorRol";
                    sql += String.Format(" WHERE nRolId = {0};", nRolId);
                    sql += "DELETE FROM PermisoDenegadoPorRol";
                    sql += String.Format(" WHERE nRolId = {0};", nRolId);

                    foreach (Menu oMenu in ListMenu)
                    {
                        if (oMenu.bEstado == false)
                        {
                            sql += "INSERT INTO MenuDenegadoPorRol(nRolId,nMenuId)";
                            sql += String.Format(" VALUES ({0},{1});", nRolId, oMenu.nMenuId);
                        }
                    }

                    foreach (Modulo oModulo in ListModulo)
                    {
                        if (oModulo.bEstado == false)
                        {
                            sql += "INSERT INTO ModuloDenegadoPorRol(nRolId,nModId)";
                            sql += String.Format(" VALUES ({0},{1});", nRolId, oModulo.nModId);
                        }
                    }

                    foreach (Permiso oPermiso in ListaPermiso)
                    {
                        if (oPermiso.bEstado == false)
                        {
                            sql += "INSERT INTO PermisoDenegadoPorRol(nRolId,nPermId)";
                            sql += String.Format(" VALUES ({0},{1});", nRolId, oPermiso.nPermId);
                        }
                    }

                    ConexionPosgreSQL.ejecutar(sql);

                    resultado = nRolId;
                }
                catch (Exception ex)
                {
                    resultado = -1;
                }
            }
            return resultado;
        }
        
        public int EliminarRol(int nRolId)
        {
            var conexion = new ConexionPosgreSQL();
            int resultado = -1;

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_EliminarRol))
                    {
                        cmd.Parameters.AddWithValue("_nRolId", nRolId);

                        NpgsqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            resultado = Int32.Parse(reader[0].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    resultado = -1;
                }
            }
            return resultado;
        }
    }
}

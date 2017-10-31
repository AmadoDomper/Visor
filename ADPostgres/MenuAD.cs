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
    public class MenuAD
    {

        public List<Menu> ObtenerMenuFull(int nRolId)
        {
            var conexion = new ConexionPosgreSQL();
            var oListaMenu = new List<Menu>();

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_SeleccionarMenuFull))
                    {
                        cmd.Parameters.AddWithValue("_nRolId", nRolId);
                        NpgsqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            var oMenu = new Menu();

                            oMenu.nMenuId = (int)reader[0];
                            oMenu.nMenuPadre =(int)reader[1];
                            oMenu.cMenuDesc = (string)reader[2];
                            oMenu.cMenuIcono = (string)reader[3];
                            oMenu.nMenuposicion = (int)reader[4];
                            oMenu.cMenuUrl = (string)reader[5];
                            oMenu.cMenuEstado = (string)reader[6];

                            oListaMenu.Add(oMenu);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oListaMenu;
        }



    }
}

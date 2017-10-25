using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ADPostgres.Helper;
using EPostgres;

namespace ADPostgres
{
    public class RolAD
    {
        public List<Rol> ListarRoles()
        {
            var conexion = new ConexionPosgreSQL();
            List<Rol> lista = new List<Rol>();

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    DataTable datos = ConexionPosgreSQL.ejecutarDT(Procedimiento.usp_ListarRoles);

                    lista = datos.AsEnumerable().Select(row =>
                        new Rol
                        {
                            nRolId = row.Field<int>("nrolid"),
                            cRolDesc = row.Field<string>("croldesc"),
                        }).ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return lista;
        }
    }
}

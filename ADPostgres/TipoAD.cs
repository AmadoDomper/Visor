using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EPostgres;
namespace ADPostgres
{
    public class TipoAD
    {
        public List<Tipo> obtener()
        {
            var conexion = new ConexionPosgreSQL();
            List<Tipo> lsItems = new List<Tipo>();

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    DataSet datos = ConexionPosgreSQL.Seleccionar("tip_idtipo,tip_descripcion,tip_uri", "tipo", "tip_descripcion");
                    
                    foreach (DataRow row in datos.Tables[0].Rows)
                    {
                        Tipo item = new Tipo();
                        item.nTipoId = Int32.Parse(row[0].ToString());
                        item.cDesc = row[1].ToString();
                        item.cUri = row[2].ToString();
                        lsItems.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return lsItems;
        }
    }
}

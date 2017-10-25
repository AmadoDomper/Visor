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
    public class TemaAD
    {
        public List<Tema> ObtenerTema()
        {
            var conexion = new ConexionPosgreSQL();
            List<Tema> lista = new List<Tema>();

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    DataTable datos = ConexionPosgreSQL.ejecutarDT(Procedimiento.usp_ObtenerTema);

                    lista = datos.AsEnumerable().Select(row =>
                        new Tema
                        {
                            nTemaId = row.Field<int>("tem_idtema"),
                            cDesc = row.Field<string>("tem_descripcion")
                        }).ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return lista;
        }

        //public List<Tema> ObtenerTemasPorPublicacionId(int pub_idpublicacion)
        //{
        //    ConexionPosgreSQL.Conectar();
        //    string sql = String.Format
        //                    (
        //                    "select" +
        //                    " t.tem_idtema,t.tem_descripcion" +
        //                    " from publicacion_tema pt" +
        //                    " inner join tema t on t.tem_idtema=pt.tem_idtema" +
        //                    " inner join publicacion p on p.pub_idpublicacion=pt.pub_idpublicacion" +
        //                    " where pt.pub_idpublicacion={0}",
        //                    pub_idpublicacion
        //                    );

        //    DataSet datos = ConexionPosgreSQL.ejecutar(sql);
        //    List<Tema> lsItems = new List<Tema>();
        //    foreach (DataRow row in datos.Tables[0].Rows)
        //    {
        //        Tema item = new Tema();
        //        item.nTemaId = Int32.Parse(row[0].ToString());
        //        item.cDesc = row[1].ToString();
        //        lsItems.Add(item);
        //    }
        //    ConexionPosgreSQL.Desconectar();
        //    return lsItems;
        //}

        public List<Tema> ObtenerTemasPorPublicacionId(int pub_idpublicacion)
        {
            var conexion = new ConexionPosgreSQL();
            List<Tema> lista = new List<Tema>();

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_ObtenerTemasPorPublicacionId);
                    cmd.Parameters.AddWithValue("nPublicacionId", pub_idpublicacion);

                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                    DataTable datos = new DataTable();
                    da.Fill(datos);

                    lista = datos.AsEnumerable().Select(row =>
                        new Tema
                        {
                            nTemaId = row.Field<int>("tem_idtema"),
                            cDesc = row.Field<string>("tem_descripcion")
                        }).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return lista;
        }
    }
}

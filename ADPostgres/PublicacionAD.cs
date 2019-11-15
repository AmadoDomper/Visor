using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ADPostgres.Helper;
using EPostgres;
using Npgsql;
using Newtonsoft.Json;

namespace ADPostgres
{
    public class PublicacionAD
    {
        //public Publicacion obtener(int pub_idpublicacion) //Eliminar (No se usará)
        //{
        //    Publicacion item = null;
        //    ConexionPosgreSQL.Conectar();

        //    string sql = String.Format(
        //                    "select" +
        //                    " p.pub_idpublicacion," +
        //                    " p.pub_anopublicacion," +
        //                    " p.pub_referenciabibliografica," +
        //                    " p.pub_enlace," +
        //                    " p.tip_idtipo," +
        //                    " p.pub_titulo" +
        //                    " from publicacion p" +
        //                    " where p.pub_idpublicacion={0}",
        //                    pub_idpublicacion
        //                    );
        //    DataSet datos = ConexionPosgreSQL.ejecutar(sql);
        //    foreach (DataRow row in datos.Tables[0].Rows)
        //    {
        //        item = new Publicacion();
        //        item.nPubliId = Int32.Parse(row[0].ToString());
        //        item.nPubliAno = Int32.Parse(row[1].ToString());
        //        item.cRefBiblio = row[2].ToString();
        //        item.cEnlace = row[3].ToString();
        //        item.oTipo = new Tipo();
        //        item.oTipo.nTipoId = Int32.Parse(row[4].ToString());
        //        item.cTitulo = row[5].ToString();

        //    }
        //    ConexionPosgreSQL.Desconectar();
        //    item.ListaTemas = new TemaAD().ObtenerTemasPorPublicacionId(pub_idpublicacion);
        //    item.ListaFeatures = obtenerPuntos(pub_idpublicacion);
        //    return item;
        //}


        public Publicacion CargaDatosPublicacion(int nPubId)
        {
            var conexion = new ConexionPosgreSQL();
            var oPub = new Publicacion();

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_CargarDatosPublicacion))
                    {
                        cmd.Parameters.AddWithValue("_nPubId", nPubId);
                        NpgsqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            oPub.nPubliId = Int32.Parse(reader[0].ToString());
                            oPub.cTitulo = reader[1].ToString();
                            oPub.cEnlace = reader[2].ToString();
                            oPub.oTipo = new Tipo();
                            oPub.oTipo.nTipoId = Int32.Parse(reader[3].ToString());
                            oPub.nPubliAno = Int32.Parse(reader[4].ToString());
                            oPub.cRefBiblio = reader[5].ToString();
                            oPub.nEstado = Int32.Parse(reader[6].ToString());
                        }
                    }

                    oPub.ListaTemas = new TemaAD().ObtenerTemasPorPublicacionId(nPubId);
                    oPub.ListaFeatures = obtenerPuntos(nPubId);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oPub;
        }




        public List<Feature> obtenerPuntos(int pub_idpublicacion)
        {
            var conexion = new ConexionPosgreSQL();
            List<Feature> lsItems = new List<Feature>();

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    string sql = String.Format("SELECT pun_idpunto,ST_AsTEXT(wkb) from punto where pub_idpublicacion={0} ORDER BY pun_idpunto", pub_idpublicacion);
                    DataSet datos = ConexionPosgreSQL.ejecutar(sql);
                    
                    foreach (DataRow row in datos.Tables[0].Rows)
                    {
                        Feature item = new Feature();
                        int tamano = row[1].ToString().Length;
                        string info = row[1].ToString();
                        item.Id = Convert.ToInt32(row[0]);
                        item.Info = info.Substring(6, (tamano - 1) - 6);
                        item.Tipo = 1;
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

        public List<Publicacion> listar()
        {
            var conexion = new ConexionPosgreSQL();
            List<Publicacion> lsItems = new List<Publicacion>();

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    DataSet datos = ConexionPosgreSQL.Seleccionar("pub_idpublicacion,pub_anopublicacion,pub_referenciabibliografica,pub_enlace,tip_idtipo,pub_titulo", "publicacion", "pub_idpublicacion");

                    foreach (DataRow row in datos.Tables[0].Rows)
                    {
                        Publicacion item = new Publicacion();
                        item.nPubliId = Int32.Parse(row[0].ToString());
                        item.nPubliAno = Int32.Parse(row[1].ToString());
                        item.cRefBiblio = row[2].ToString();
                        item.cEnlace = row[3].ToString();
                        item.oTipo = new Tipo();
                        item.oTipo.nTipoId = Int32.Parse(row[4].ToString());
                        item.cTitulo = row[5].ToString();
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

        public Publicacion registrar(Publicacion item)
        {
            var conexion = new ConexionPosgreSQL();
            item.dFechaRegistro = DateTime.Now;

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    /* REGISTRA PUBLICACIÓN */
                    string sql = "INSERT INTO publicacion(pub_anopublicacion, usu_nusuarioid, pub_fecharegistro, pub_referenciabibliografica, pub_enlace, tip_idtipo, pub_estado,pub_titulo)";
                    sql += String.Format
                        (
                        "VALUES ({0},{1},'{2}','{3}','{4}',{5}, {6},'{7}')",
                        item.nPubliAno,
                        item.nUsuId,
                        item.dFechaRegistro.ToString("yyyyMMdd"),
                        item.cRefBiblio,
                        item.cEnlace,
                        item.oTipo.nTipoId,
                        1,
                        item.cTitulo
                        );
                    sql += ";";
                    sql += "SELECT currval(pg_get_serial_sequence('publicacion','pub_idpublicacion')) AS id";

                    DataSet datos = ConexionPosgreSQL.ejecutar(sql);

                    foreach (DataRow row in datos.Tables[0].Rows)
                    {
                        item.nPubliId = Int32.Parse(row[0].ToString());
                    }

                    /* END REGISTRA PUBLICACIÓN */

                    /* REGISTRA LOS TEMAS */
                    sql = "";
                    foreach (Tema itemTema in item.ListaTemas)
                    {
                        sql += "INSERT INTO publicacion_tema(pub_idpublicacion,tem_idtema)";
                        sql += String.Format
                        (
                        "VALUES ({0},{1});",
                        item.nPubliId,
                        itemTema.nTemaId
                        );
                    }
                    datos = ConexionPosgreSQL.ejecutar(sql);

                    /* END REGISTRA LOS TEMAS */

                    /* REGISTRA LOS FEATURES */
                    sql = "";
                    foreach (Feature itemF in item.ListaFeatures)
                    {

                        if (itemF.Tipo == 1)// es un punto
                        {
                            sql += "INSERT INTO punto(pub_idpublicacion,wkb)";
                            sql += String.Format
                            (
                                "VALUES ({0},{1});",
                                item.nPubliId,
                                "ST_GeomFromText('POINT(" + itemF.Info + ")', 4326)"
                            );
                        }
                        else if (itemF.Tipo == 3)// es un poligono
                        {
                            sql += "INSERT INTO poligono(pub_idpublicacion,wkb)";
                            sql += String.Format
                            (
                                "VALUES ({0},{1});",
                                item.nPubliId,
                                "ST_GeomFromText('POLYGON((" + itemF.Info + "))', 4326)"
                            );
                        }
                    }
                    datos = ConexionPosgreSQL.ejecutar(sql);

                    /* END REGISTRA LOS FEATURES */
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            return item;

        }



        public Publicacion editar(Publicacion item)
        {
            var conexion = new ConexionPosgreSQL();
            item.dFechaRegistro = DateTime.Now;

            using (var db = conexion.AbreConexion())
            {
                try
                {

                    string sql = String.Format("UPDATE publicacion SET pub_anopublicacion={0}, pub_referenciabibliografica='{1}', pub_enlace='{2}', tip_idtipo={3},pub_titulo='{4}' WHERE pub_idpublicacion={5};"
                                                , item.nPubliAno,
                                                item.cRefBiblio,
                                                item.cEnlace,
                                                item.oTipo.nTipoId,
                                                item.cTitulo,
                                                item.nPubliId
                                              );
                    DataSet datos = ConexionPosgreSQL.ejecutar(sql);
                    //eliminar los temas
                    sql = "";
                    sql = String.Format("DELETE from publicacion_tema where pub_idpublicacion={0}", item.nPubliId);
                    datos = ConexionPosgreSQL.ejecutar(sql);
                    //regitrar los temas
                    sql = "";
                    foreach (Tema itemTema in item.ListaTemas)
                    {
                        sql += "INSERT INTO publicacion_tema(pub_idpublicacion,tem_idtema)";
                        sql += String.Format
                        (
                        "VALUES ({0},{1});",
                        item.nPubliId,
                        itemTema.nTemaId
                        );
                    }
                    datos = ConexionPosgreSQL.ejecutar(sql);

                    //eliminamos los features
                    sql = "";
                    sql = String.Format("DELETE from punto where pub_idpublicacion={0}", item.nPubliId);
                    datos = ConexionPosgreSQL.ejecutar(sql);
                    //insertar los features
                    sql = "";
                    foreach (Feature itemF in item.ListaFeatures)
                    {

                        if (itemF.Tipo == 1)// es un punto
                        {
                            sql += "INSERT INTO punto(pub_idpublicacion,wkb)";
                            sql += String.Format
                            (
                                "VALUES ({0},{1});",
                                item.nPubliId,
                                "ST_GeomFromText('POINT(" + itemF.Info + ")', 4326)"
                            );
                        }
                        else if (itemF.Tipo == 3)// es un poligono
                        {
                            sql += "INSERT INTO poligono(pub_idpublicacion,wkb)";
                            sql += String.Format
                            (
                                "VALUES ({0},{1});",
                                item.nPubliId,
                                "ST_GeomFromText('POLYGON((" + itemF.Info + "))', 4326)"
                            );
                        }
                    }
                    datos = ConexionPosgreSQL.ejecutar(sql);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return item;
        }

        //public List<Publicacion> ListarPublicaciones()
        //{
        //    try
        //    {
        //        ConexionPosgreSQL.Conectar();
        //        DataTable datos = ConexionPosgreSQL.ejecutarDT(Procedimiento.usp_ListarPublicaciones);

        //        List<Publicacion> lista = datos.AsEnumerable().Select(row =>
        //            new Publicacion
        //            {
        //                nPubliId = row.Field<int>("pub_idpublicacion"),
        //                cTitulo = row.Field<string>("pub_titulo"),
        //                cFechaRegistro = row.Field<string>("pub_fecharegistro"),
        //                cEstado = row.Field<string>("pEst_descripcion"),

        //            }).ToList();

        //        ConexionPosgreSQL.Desconectar();
        //        return lista;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}



        #region MisPublicaciones
        public ListaPaginada ListarMisPublicacionesPag(int nPubEst, int nPage = 1, int nSize = 10, int nPubId = -1, string cPubTitulo = "", string cDni = "", string cInst = "")
        {
            var conexion = new ConexionPosgreSQL();
            ListaPaginada ListaPubPag = new ListaPaginada();

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_ListarMisPublicacionesPaginado))
                    {
                        cmd.Parameters.AddWithValue("_pub_estado", nPubEst);
                        cmd.Parameters.AddWithValue("_pub_titulo", cPubTitulo);
                        cmd.Parameters.AddWithValue("_usu_cDni", cDni);
                        cmd.Parameters.AddWithValue("_pub_nPudId", nPubId);
                        cmd.Parameters.AddWithValue("_usu_cInst", cInst);
                        cmd.Parameters.AddWithValue("_nPage", nPage);
                        cmd.Parameters.AddWithValue("_nSize", nSize);

                        NpgsqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Publicacion oPublicacion = new Publicacion();

                            oPublicacion.nPubliId = Int32.Parse(reader[0].ToString());
                            oPublicacion.cTitulo = reader[1].ToString();
                            oPublicacion.oUsuario = new Usuario();
                            oPublicacion.oUsuario.cNombres = reader[2].ToString();
                            oPublicacion.oUsuario.cInstitucion = reader[3].ToString();
                            oPublicacion.cFechaRegistro = reader[4].ToString();
                            oPublicacion.cEstado = reader[5].ToString();

                            ListaPubPag.oLista.Add(oPublicacion);
                        }
                    }

                    ListaPubPag.nPage = nPage;
                    ListaPubPag.nPageSize = nSize;
                    ObtenerPaginadoMisPublicaciones(nPubEst, ref ListaPubPag, nSize, nPubId, cPubTitulo, cDni, cInst);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return ListaPubPag;
        }

        public void ObtenerPaginadoMisPublicaciones(int nPubEst, ref ListaPaginada oLista, int nSize = 10, int nPubId = -1, string cPubTitulo = "", string cDni = "", string cInst = "")
        {
            var conexion = new ConexionPosgreSQL();

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_ObtenerMisPublicacionesCantPag))
                    {
                        cmd.Parameters.AddWithValue("_pub_estado", nPubEst);
                        cmd.Parameters.AddWithValue("_pub_titulo", cPubTitulo);
                        cmd.Parameters.AddWithValue("_usu_cDni", cDni);
                        cmd.Parameters.AddWithValue("_pub_nPudId", nPubId);
                        cmd.Parameters.AddWithValue("_usu_cInst", cInst);
                        cmd.Parameters.AddWithValue("_nSize", nSize);

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

        #endregion

        #region Revisión de Publicaciones
        public ListaPaginada ListarRevPublicacionesPag(int nPubEst, int nPage = 1, int nSize = 10, int nPubId = -1, string cPubTitulo = "", string cDni = "", string cInst = "", string cNom = "")
        {
            var conexion = new ConexionPosgreSQL();
            ListaPaginada ListaPubPag = new ListaPaginada();

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_ListarRevPublicacionesPaginado))
                    {
                        cmd.Parameters.AddWithValue("_pub_estado", nPubEst);
                        cmd.Parameters.AddWithValue("_pub_titulo", cPubTitulo);
                        cmd.Parameters.AddWithValue("_usu_cDni", cDni);
                        cmd.Parameters.AddWithValue("_pub_nPudId", nPubId);
                        cmd.Parameters.AddWithValue("_usu_cInst", cInst);
                        cmd.Parameters.AddWithValue("_usu_cNombre", cNom);
                        cmd.Parameters.AddWithValue("_nPage", nPage);
                        cmd.Parameters.AddWithValue("_nSize", nSize);

                        NpgsqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Publicacion oPublicacion = new Publicacion();

                            oPublicacion.nPubliId = Int32.Parse(reader[0].ToString());
                            oPublicacion.cTitulo = reader[1].ToString();
                            oPublicacion.oUsuario = new Usuario();
                            oPublicacion.oUsuario.cNombres = reader[2].ToString();
                            oPublicacion.oUsuario.cInstitucion = reader[3].ToString();
                            oPublicacion.cFechaRegistro = reader[4].ToString();
                            oPublicacion.cEstado = reader[5].ToString();

                            ListaPubPag.oLista.Add(oPublicacion);
                        }
                    }

                    ListaPubPag.nPage = nPage;
                    ListaPubPag.nPageSize = nSize;
                    ObtenerPaginadoRevPublicaciones(nPubEst, ref ListaPubPag, nSize, nPubId, cPubTitulo, cDni, cInst, cNom);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return ListaPubPag;

        }

        public void ObtenerPaginadoRevPublicaciones(int nPubEst, ref ListaPaginada oLista, int nSize = 10, int nPubId = -1, string cPubTitulo = "", string cDni = "", string cInst = "", string cNom = "")
        {
            var conexion = new ConexionPosgreSQL();

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_ObtenerRevPublicacionesCantPag))
                    {
                        cmd.Parameters.AddWithValue("_pub_estado", nPubEst);
                        cmd.Parameters.AddWithValue("_pub_titulo", cPubTitulo);
                        cmd.Parameters.AddWithValue("_usu_cDni", cDni);
                        cmd.Parameters.AddWithValue("_pub_nPudId", nPubId);
                        cmd.Parameters.AddWithValue("_usu_cInst", cInst);
                        cmd.Parameters.AddWithValue("_usu_cNombre", cNom);
                        cmd.Parameters.AddWithValue("_nSize", nSize);

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
        #endregion

        /*Buscador API*/

        #region Revisión de Publicaciones
        public ListaPaginada BuscarPublicacionesPag(int nPage = 1, int nSize = 10, string cPubTexto = "", int nTipo = -1, string cAno = "", int nAreaTem = -1)
        {
            var conexion = new ConexionPosgreSQL();
            ListaPaginada ListaPubPag = new ListaPaginada();

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_BuscarPublicacionesPaginado))
                    {
                        cmd.Parameters.AddWithValue("_pub_texto", cPubTexto);
                        cmd.Parameters.AddWithValue("_pub_nTipo", nTipo);
                        cmd.Parameters.AddWithValue("_pub_cAno", cAno);
                        cmd.Parameters.AddWithValue("_pub_nAreaTem", nAreaTem);
                        cmd.Parameters.AddWithValue("_nPage", nPage);
                        cmd.Parameters.AddWithValue("_nSize", nSize);

                        NpgsqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Publicacion oPublicacion = new Publicacion();

                            oPublicacion.nPubliId = Int32.Parse(reader[0].ToString());
                            oPublicacion.cTitulo = reader[1].ToString();
                            oPublicacion.cRefBiblio = reader[2].ToString();

                            ListaPubPag.oLista.Add(oPublicacion);
                        }
                    }

                    ListaPubPag.nPage = nPage;
                    ListaPubPag.nPageSize = nSize;
                    ObtenerPaginadoBuscarPublicaciones(ref ListaPubPag, nSize, cPubTexto, nTipo, cAno, nAreaTem);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return ListaPubPag;

        }

        public void ObtenerPaginadoBuscarPublicaciones(ref ListaPaginada oLista, int nSize = 10, string cPubTexto = "", int nTipo = -1, string cAno = "", int nAreaTem = -1)
        {
            var conexion = new ConexionPosgreSQL();

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_BuscarPublicacionesCantPag))
                    {
                        cmd.Parameters.AddWithValue("_pub_texto", cPubTexto);
                        cmd.Parameters.AddWithValue("_pub_nTipo", nTipo);
                        cmd.Parameters.AddWithValue("_pub_nAno", cAno);
                        cmd.Parameters.AddWithValue("_pub_nAreaTem", nAreaTem);
                        cmd.Parameters.AddWithValue("_nSize", nSize);

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
        #endregion

        /*END buscador API*/




        public List<Tema> ListarTemas()
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

        public List<Tipo> ListarTipos()
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

        /// <summary>
        /// Get all publication points in a JSON format
        /// </summary>
        /// <returns></returns>
        public Object GetAllPublicationPoints()
        {
            var conexion = new ConexionPosgreSQL();
            Object json;

            using (var db = conexion.AbreConexion())
            {
                try
                {
                    using (NpgsqlCommand cmd = ConexionPosgreSQL.Procedimiento(Procedimiento.usp_getAllPublicationPoints))
                    {
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


        /// <summary>
        /// Get all publication points ids in a JSON array format
        /// </summary>
        /// <returns></returns>
        public Object GetSearchPublicactionPoints(string cPubTexto = "", int nTipo = -1, string cAno = "", int nAreaTem = -1)
        {
            NpgsqlConnection Conex = new NpgsqlConnection(Conexion.cadena);
            NpgsqlCommand cmd = new NpgsqlCommand(Procedimiento.usp_getPublicationsPointsId, Conex);
            cmd.CommandType = CommandType.StoredProcedure;
            Conex.Open();

            Object json;

            using (var db = Conex)
            {
                try
                {
                    using (cmd)
                    {
                        cmd.Parameters.AddWithValue("_pub_texto", cPubTexto ?? "");
                        cmd.Parameters.AddWithValue("_pub_nTipo", nTipo);
                        cmd.Parameters.AddWithValue("_pub_cAno", cAno ?? "");
                        cmd.Parameters.AddWithValue("_pub_nAreaTem", nAreaTem);

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

        /// <summary>
        /// Get all publication ids found in a JSON array format
        /// </summary>
        /// <returns></returns>
        public Object GetSearchPublicactionIds(string cPubTexto = "", int nTipo = -1, string cAno = "", int nAreaTem = -1)
        {
            NpgsqlConnection Conex = new NpgsqlConnection(Conexion.cadena);
            NpgsqlCommand cmd = new NpgsqlCommand(Procedimiento.usp_getpublicationids, Conex);
            cmd.CommandType = CommandType.StoredProcedure;
            Conex.Open();

            Object json;

            using (var db = Conex)
            {
                try
                {
                    using (cmd)
                    {
                        cmd.Parameters.AddWithValue("_pub_texto", cPubTexto ?? "");
                        cmd.Parameters.AddWithValue("_pub_nTipo", nTipo);
                        cmd.Parameters.AddWithValue("_pub_cAno", cAno ?? "");
                        cmd.Parameters.AddWithValue("_pub_nAreaTem", nAreaTem);

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

        /// <summary>
        /// Get all publication information in a JSON format
        /// </summary>
        /// <returns></returns>
        public Object GetAllPublicactionsJSON()
        {
            NpgsqlConnection Conex = new NpgsqlConnection(Conexion.cadena);
            NpgsqlCommand cmd = new NpgsqlCommand(Procedimiento.usp_getallpublicationsJSON, Conex);
            cmd.CommandType = CommandType.StoredProcedure;
            Conex.Open();

            Object json;

            using (var db = Conex)
            {
                try
                {
                    using (cmd)
                    {
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

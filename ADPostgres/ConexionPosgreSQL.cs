using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;
using NpgsqlTypes;
using ADPostgres.Helper;
using System.Data;
namespace ADPostgres
{
    public class ConexionPosgreSQL
    {
        //Declaramos un objeto conexión, adaptador, comando, tabla, cadena de conexión y un bindingsource.
        //static NpgsqlConnection Conex = new NpgsqlConnection();
        static NpgsqlConnection Conex;
        static NpgsqlDataAdapter Adaptador = new NpgsqlDataAdapter();
        static NpgsqlCommand Comando = new NpgsqlCommand();

        public NpgsqlConnection AbreConexion()
        {            
            var cadenaDeConexion = Conexion.cadena;

            if (!string.IsNullOrWhiteSpace(cadenaDeConexion))
            {
                try
                {
                    Conex = new NpgsqlConnection(cadenaDeConexion);
                    Conex.Open();
                }
                catch (Exception ex)
                {
                    Conex.Close();
                }
            }
            return Conex;
        }

        public static void Desconectar()
        {
            //Cerramos la conexión.
            Conex.Close();
        }

        public static DataSet Seleccionar(string campos, string tabla, string orden)
        {
            //Declaramos una variable para almacenar la consulta.
            string Consulta = "select " + campos + " from " + tabla + " order by " + orden + ";";
            //Creamos nuestro adaptador y le pasamos la consulta y la conexión.
            Adaptador = new NpgsqlDataAdapter(Consulta, Conex);
            //Creamos un comando constructor y le pasamos el adaptador.
            NpgsqlCommandBuilder ComandoConstructor = new NpgsqlCommandBuilder(Adaptador);
            //Llenamos nuestra tabla con los datos de nuestro adaptador.
            DataSet Tabla = new DataSet();
            Adaptador.Fill(Tabla);
            return Tabla;
        }
        public static DataSet ejecutar(string sql)
        {
            Adaptador = new NpgsqlDataAdapter(sql, Conex);
            NpgsqlCommandBuilder ComandoConstructor = new NpgsqlCommandBuilder(Adaptador);
            DataSet Tabla = new DataSet();
            Adaptador.Fill(Tabla);
            return Tabla;
        }


        public static DataTable ejecutarDT(string sql)
        {
            NpgsqlCommand cmd = new NpgsqlCommand(sql, Conex);
            cmd.CommandType = CommandType.StoredProcedure;
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;


            //Adaptador = new NpgsqlDataAdapter(sql, Conex);
            //NpgsqlCommandBuilder ComandoConstructor = new NpgsqlCommandBuilder(Adaptador);
            //DataTable Tabla = new DataTable();
            //Adaptador.Fill(Tabla);
            //return Tabla;
        }


        public static NpgsqlCommand Procedimiento(string sql)
        {
            NpgsqlCommand cmd = new NpgsqlCommand(sql, Conex);
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }


    }
}

﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADSql.Helper
{
    public class Conexion
    {
        public static string nombre = "cadenaSeguridad";
        public static string cadena = ConfigurationManager.ConnectionStrings[nombre].ToString();
    }
}

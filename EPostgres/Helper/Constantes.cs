using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPostgres.Helper
{
        public enum TipoReferencia
        {
            Publicaciones = 1,
            InventarioSuelos = 2,
            InventarioVegetaciones = 3
        }

        public enum EstadoSolicitud
        {
            Solicitado = 1,
            Aprobado = 2,
            Observado = 3,
            Rechazado = 4
        }

        /// <summary>
        /// Devuelve el Id único de acuerdo a su Rol
        /// </summary>
        public enum RolId
        {
   	        Administrador    =  1,
   	        Investigador     =  2,
   	        Supervisor       =  3
        }

        public static class AlertIcon
        {
            public const string
                Solicitado = "fa-gift",
                Aprobado = "fa-check",
                Observado = "fa-search",
                Rechazado = "fa-times-circle";
        }

        public static class AlertColor
        {
            public const string
                Solicitado = "bg-success",
                Aprobado = "bg-warning",
                Observado = "bg-primary",
                Rechazado = "btn-danger";
        }


}

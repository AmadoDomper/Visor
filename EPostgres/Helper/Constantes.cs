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
}

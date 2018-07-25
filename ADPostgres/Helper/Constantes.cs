using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADPostgres.Helper
{
    public static class Procedimiento
    {

        public const string usp_ObtenerTema = "usp_ObtenerTema";
        public const string usp_ObtenerTemasPorPublicacionId = "usp_ObtenerTemasPorPublicacionId";


        /*MIS PUBLICACIONES*/
        public const string usp_ListarMisPublicacionesPaginado = "usp_ListarMisPublicacionesPaginado";
        public const string usp_ObtenerMisPublicacionesCantPag = "usp_ObtenerMisPublicacionesCantPag";
        public const string usp_CargarDatosPublicacion = "usp_CargarDatosPublicacion";
        /*END MIS PUBLICACIONES*/


        /*REVISIÓN PUBLICACIONES*/
        public const string usp_ListarRevPublicacionesPaginado = "usp_ListarRevPublicacionesPaginado";
        public const string usp_ObtenerRevPublicacionesCantPag = "usp_ObtenerRevPublicacionesCantPag";
        /*END REVISIÓN PUBLICACIONES*/

        /*CONFIGURACIÓN*/
        public const string usp_ListarRoles = "usp_ListarRoles";
        public const string usp_ListarMenuRol = "usp_ListarMenuRol";
        public const string usp_ListarModuloRol = "usp_ListarModuloRol";
        public const string usp_ListarPermisoRol = "usp_ListarPermisoRol";
        public const string usp_CrearRol = "usp_CrearRol";
        public const string usp_EliminarRol = "usp_EliminarRol";


        /*END CONFIGURACIÓN*/

        /*USUARIOS*/
        public const string usp_ListarUsuariosPaginado = "usp_ListarUsuariosPaginado";
        public const string usp_ObtenerPaginadoUsuarios = "usp_ObtenerPaginadoUsuarios";
        public const string usp_CargarDatosUsuario = "usp_CargarDatosUsuario";
        public const string usp_Crear_o_ModificarUsuario = "usp_Crear_o_ModificarUsuario";
        /*END USUARIOS*/


        /*SEGURIDAD*/

        public const string usp_ValidaAccesoUsuario = "usp_ValidaAccesoUsuario";

        /*END SEGURIDAD*/


        /*MENU*/
        public const string usp_SeleccionarMenuFull = "usp_SeleccionarMenuFull";
        /*END MENU*/

        /*API*/

        public const string usp_BuscarPublicacionesPaginado = "usp_BuscarPublicacionesPaginado";
        public const string usp_BuscarPublicacionesCantPag = "usp_BuscarPublicacionesCantPag";

    }
}

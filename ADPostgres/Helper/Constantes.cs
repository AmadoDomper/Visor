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

        /*INVENTARIO DE VEGETACIONES*/

        public const string usp_registrar_inventario_vegetacion = "usp_registrar_inventario_vegetacion";
        public const string usp_actualiza_inventario_vegetacion = "usp_actualiza_inventario_vegetacion";
        public const string usp_elimina_inventario_vegetacion = "usp_elimina_inventario_vegetacion";
        public const string usp_listar_mis_inventarios_vegetacion_paginado = "usp_listar_mis_inventarios_vegetacion_paginado";
        public const string usp_obtener_mis_inventarios_vegetacion_cantpag = "usp_obtener_mis_inventarios_vegetacion_cantpag";
        public const string usp_cargar_datos_inventario_vegetacion = "usp_cargar_datos_inventario_vegetacion";


        /*INVENTARIO DE VEGETACIONES - PARCELA*/

        public const string usp_registrar_inventario_vegetacion_detalle = "usp_registrar_inventario_vegetacion_detalle";
        public const string usp_actualiza_inventario_vegetacion_detalle = "usp_actualiza_inventario_vegetacion_detalle";
        public const string usp_elimina_inventario_vegetacion_detalle = "usp_elimina_inventario_vegetacion_detalle";
        public const string usp_lista_inventario_vegetacion_detalle = "usp_lista_inventario_vegetacion_detalle";
        public const string usp_cargar_datos_inventario_vegetacion_detalle = "usp_cargar_datos_inventario_vegetacion_detalle";

        /*INVENTARIO DE SUELOS*/

        public const string usp_registrar_inventario_suelos = "usp_registrar_inventario_suelos";
        public const string usp_actualiza_inventario_suelos = "usp_actualiza_inventario_suelos";
        public const string usp_elimina_inventario_suelos = "usp_elimina_inventario_suelos";
        public const string usp_listar_mis_inventarios_suelos_paginado = "usp_listar_mis_inventarios_suelos_paginado";
        public const string usp_obtener_mis_inventarios_suelos_cantpag = "usp_obtener_mis_inventarios_suelos_cantpag";
        public const string usp_cargar_datos_inventario_suelos = "usp_cargar_datos_inventario_suelos";


        /*INVENTARIO DE SUELOS - PERFIL MODAL*/

        public const string usp_registrar_inventario_suelos_detalle = "usp_registrar_inventario_suelos_detalle";
        public const string usp_actualiza_inventario_suelos_detalle = "usp_actualiza_inventario_suelos_detalle";
        public const string usp_elimina_inventario_suelos_detalle = "usp_elimina_inventario_suelos_detalle";
        public const string usp_lista_inventario_suelos_detalle = "usp_lista_inventario_suelos_detalle";
        public const string usp_cargar_datos_inventario_suelos_detalle = "usp_cargar_datos_inventario_suelos_detalle";

    }
}

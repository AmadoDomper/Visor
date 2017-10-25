using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADPostgres.Common
{
    public enum RolesPermisos
    {
        #region Mis Publicaciones
        Mis_Publicaciones_Registra_Publicacion = 1,
        Mis_Publicaciones_Editar_Publicacion = 2,
        #endregion

        #region Revision de Solicitudes
        Clie_Crear_Nuevo_Cliente = 7,
        Clie_Editar_Cliente = 8,
        Clie_Eliminar_Cliente = 9,
        #endregion

        #region Usuarios
        Usu_Nuevo_Usuario = 10,
        Usu_Editar_Usuario = 11,
        Usu_Eliminar_Usuario = 12,
        #endregion

        #region Configuración
        Con_Agregar_Rol = 16,
        Con_Editar_Rol = 17,
        Con_Eliminar_Rol = 18,
        #endregion

    }
}

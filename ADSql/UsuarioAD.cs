using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using ADSql.Helper;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ESql;

namespace ADSql
{
    public class UsuarioAD
    {
        private Database oDatabase = DatabaseFactory.CreateDatabase(Conexion.nombre);// EnterpriseLibraryContainer.Current.GetInstance<Database>(Conexion.cadena);

        public Usuario validarUsuario(string UserName, string Pass)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_user);
            oDatabase.AddInParameter(oDbCommand, "@pUserName", DbType.String, UserName);
            oDatabase.AddInParameter(oDbCommand, "@pPassword", DbType.String, Pass);
            Usuario user = null;
            using (IDataReader datos = oDatabase.ExecuteReader(oDbCommand))
            {
                if (datos.Read())
                {
                    user = new Usuario();
                    user.UserName = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("UserName")]);
                    user.UserId = DataUtil.DbValueToDefault<Guid>(datos[datos.GetOrdinal("UserId")]).ToString();
                }
            }
            return user;
        }
        
        public List<Role> obtenerRolesXuser(string UserName)
        {
            List<Role> lista = null;
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.aspnet_UsersInRoles_GetRolesForUser);
            oDatabase.AddInParameter(oDbCommand, "@ApplicationName", DbType.String, "/");
            oDatabase.AddInParameter(oDbCommand, "@UserName", DbType.String, UserName);
            using (IDataReader datos = oDatabase.ExecuteReader(oDbCommand))
            {

                lista = new List<Role>();
                while (datos.Read())
                {
                    Role item = new Role();
                    item.RoleName = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("RoleName")]);
                    lista.Add(item);
                }
            }
            return lista;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.ComponentModel;

namespace ADSql.Helper
{
    public static class DataUtil
    {
        public static Nullable<T> DbValueToNullable<T>(object dbValue) where T : struct
        {
            Nullable<T> returnValue = null;
            if ((dbValue != null) && (dbValue != DBNull.Value))
            {
                returnValue = (T)dbValue;
            }
            return returnValue;
        }
        public static T DbValueToDefault<T>(object obj)
        {
            if (obj == null || obj == DBNull.Value) return default(T);
            else { return (T)obj; }
        }

        public static double toDoubleDefault(object obj)
        {
            double ret = 0;
            Double.TryParse(obj.ToString(), out ret);
            return ret;
        }

        public static int toIntDefault(object obj)
        {
            int ret = 0;
            Int32.TryParse(obj.ToString(), out ret);
            return ret;
        }


        public static object ToSafeDbDateDBnull(this object objectstring)
        {
            try
            {
                if ((DateTime)objectstring >= System.Data.SqlTypes.SqlDateTime.MinValue)
                {
                    return objectstring;
                }
                else
                {
                    return DBNull.Value;
                }
            }
            catch (Exception)
            {

                return DBNull.Value;
            }

        }

        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    object o= prop.GetValue(item);

                    if (o.GetType() == typeof(DateTime))
                    {
                        DateTime od = (DateTime)prop.GetValue(item);
                        DateTime? odn = ToNullIfTooEarlyForDb(od);
                        if (odn == null)
                        {
                            row[prop.Name] = DBNull.Value;
                        }
                        else
                        {
                            row[prop.Name] = prop.GetValue(item);
                        }
                    }
                    else
                    {
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    }

                    
                }
                table.Rows.Add(row);
            }
            return table;
        }

        public static DateTime? ToNullIfTooEarlyForDb(this DateTime date)
        {
            return (date >= (DateTime)SqlDateTime.MinValue) ? date : (DateTime?)null;
        }

        public static DateTime? ToNullIfTooEarlyForDb(this DateTime? date)
        {
            return (date >= (DateTime)SqlDateTime.MinValue) ? date : (DateTime?)null;
        }

    }
}

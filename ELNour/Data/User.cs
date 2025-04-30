using DataBaseOperations;
using DevExpress.XtraEditors;
using ELNour.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ELNour.Data
{
    internal class User
    {
        static DatabaseConnection con = new DatabaseConnection(Connections.Constr);
        public static T GetUserPermissions<T>(string query) where T : new()
        {
            // إنشاء كائن من الكلاس
            T userPermissions = new T();

            // فتح الاتصال بقاعدة البيانات

            if (con.Connection.State == ConnectionState.Closed)
                con.Connection.Open();
            using (SqlCommand command = new SqlCommand(query, con.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                // الحصول على جميع الخصائص في الكلاس
                                PropertyInfo[] properties = typeof(T).GetProperties();

                                foreach (PropertyInfo property in properties)
                                {
                                    string columnName = property.Name;

                                    // التحقق إذا كان العمود موجودًا في النتيجة
                                    if (!reader.IsDBNull(reader.GetOrdinal(columnName)))
                                    {
                                        object value = reader[columnName];
                                        property.SetValue(userPermissions, Convert.ChangeType(value, property.PropertyType));
                                    }
                                }
                            }
                        }
                    }
                }
            con.CloseConnection();
            return userPermissions;
        }
        public static int UserID { get; set; } = 1;
        public static string UserName { get; set; } = "Admin";
        public static string FullName { get; set; } = "Aministator";
        public static void ResetStaticPropertiesToDefault<T>() where T : class
        {
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Static);

            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType == typeof(bool))
                {
                    property.SetValue(null, false); // تعيين القيمة إلى false
                }
                
            }
        }
    }
}

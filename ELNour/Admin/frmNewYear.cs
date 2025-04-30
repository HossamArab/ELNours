using DevExpress.XtraEditors;
using DevExpress.XtraReports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using ELNour;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.SqlServer.Management.Smo;
using MessageBoxes;

namespace ELNour.Admin
{
    public partial class frmNewYear : DevExpress.XtraEditors.XtraForm
    {
        public frmNewYear()
        {
            InitializeComponent();
        }
        string AddYearToFileName(string filePath, int year)
        {
            // استخراج اسم الملف بدون الامتداد
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);

            // استخراج الامتداد
            string extension = Path.GetExtension(filePath);

            // استخراج المسار بدون اسم الملف
            string directoryPath = Path.GetDirectoryName(filePath);

            // إنشاء اسم جديد بإضافة السنة
            string newFileName = $"{fileNameWithoutExtension}_{year}{extension}";

            // إعادة بناء المسار الكامل
            return Path.Combine(directoryPath, newFileName);
        }
        private void GenerateScript(string ScriptPath)
        {
            string serverName = ".\\EAGLESERVER";
            string databaseName = "ElNour";
            string username = "sa"; // استخدم حساب Windows Authentication إذا لزم الأمر
            string password = "123456";
            string Year = (DateTime.Now.Year - 1).ToString();

            // إنشاء اتصال بـ SQL Server
            var connectionString = new SqlConnectionStringBuilder
            {
                DataSource = serverName,
                InitialCatalog = databaseName,
                IntegratedSecurity = string.IsNullOrEmpty(username), // إذا كان فارغاً استخدم Windows Authentication
                UserID = username,
                Password = password
            }.ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                ServerConnection serverConnection = new ServerConnection(connection);
                Server server = new Server(serverConnection);
                Database database = server.Databases[databaseName];

                // تحديد مكان حفظ السكربت
                string scriptFilePath = ScriptPath;
                string query = $"SELECT physical_name FROM sys.master_files WHERE database_id = DB_ID('{databaseName}')";
                SqlCommand command = new SqlCommand(query, connection);
                string databasePath = (string)command.ExecuteScalar();
                query = $"SELECT physical_name FROM sys.master_files WHERE database_id = DB_ID('{databaseName}') AND type_desc = 'LOG'";
                command = new SqlCommand(query, connection);
                string logFilePath = (string)command.ExecuteScalar();
                string newdatabasepath = AddYearToFileName(databasePath, Convert.ToInt32(Year));
                string newlogfilepath = AddYearToFileName(logFilePath, Convert.ToInt32(Year));
                // فتح ملف السكربت للكتابة
                using (StreamWriter writer = new StreamWriter(scriptFilePath))
                {
                    writer.WriteLine($"-- ****** إنشاء قاعدة البيانات {databaseName}_{Year} ******");
                    writer.WriteLine($"USE [Master];");
                    writer.WriteLine($"GO");
                    writer.WriteLine($"CREATE DATABASE {databaseName}_{Year} ON PRIMARY");
                    writer.WriteLine($"(NAME = {databaseName}_{Year}, FILENAME = '{newdatabasepath}',  SIZE = 5MB, MAXSIZE = Unlimited, FILEGROWTH = 1MB) ");
                    writer.WriteLine($"LOG ON ");
                    writer.WriteLine($"(NAME = {databaseName}_{Year}_log, FILENAME = '{newlogfilepath}', SIZE = 1MB, MAXSIZE = Unlimited, FILEGROWTH = 1%) ");
                    writer.WriteLine($"COLLATE Arabic_CI_AS; -- Arabic collation ");
                    writer.WriteLine($"GO");
                    writer.WriteLine($"USE [{databaseName}_{Year}];");
                    writer.WriteLine($"GO");
                    writer.WriteLine();
                    // تفعيل الخيارات لتوليد سكربت متكامل بالبيانات
                    ScriptingOptions options = new ScriptingOptions
                    {
                        ScriptDrops = false,
                        IncludeIfNotExists = true,
                        SchemaQualify = false,
                        ScriptData = true,
                        ScriptSchema = true,
                        Indexes = true,
                        Triggers = true,
                        FullTextIndexes = true,
                        DriAll = true,
                        IncludeHeaders = true,
                        NoIdentities = false,
                        ExtendedProperties = true,
                        ContinueScriptingOnError = true,
                        NoCollation = false,
                        TargetServerVersion = SqlServerVersion.Version80,
                    };
                    ScriptingOptions optionsScema = new ScriptingOptions
                    {
                        ScriptDrops = false,
                        IncludeIfNotExists = true,
                        SchemaQualify = true,
                        ScriptData = false, // بدون البيانات
                        ScriptSchema = true,
                        Indexes = true,
                        Triggers = true,
                        FullTextIndexes = true,
                        DriAll = true,
                        IncludeHeaders = true,
                        NoIdentities = false,
                        ExtendedProperties = true,
                        ContinueScriptingOnError = true,
                        NoCollation = false,
                        TargetServerVersion = SqlServerVersion.Version110,
                    };
                    ScriptingOptions schemaOptions = new ScriptingOptions
                    {
                        ScriptDrops = false,
                        ScriptData = false, // لا تصدير البيانات
                        ScriptSchema = true, // تصدير الهيكل
                        Indexes = true,
                        Triggers = true,
                        DriAll = true,
                        NoCollation = false,
                        TargetServerVersion = SqlServerVersion.Version110,
                    };

                    // ثانياً: تصدير البيانات فقط (اختياري)
                    ScriptingOptions dataOptions = new ScriptingOptions
                    {
                        ScriptData = true, // تصدير البيانات فقط
                        ScriptSchema = false // لا تصدير الهيكل
                    };
                    // توليد سكربت الجداول
                    foreach (Table table in database.Tables)
                    {
                        if (!table.IsSystemObject || table.Schema == "dbo")
                        {
                            foreach (string script in table.Script(schemaOptions))
                            {
                                writer.WriteLine(script);
                            }

                            writer.WriteLine("GO");

                            // تصدير البيانات (اختياري)
                            foreach (string script in table.EnumScript(dataOptions))
                            {
                                writer.WriteLine(script);
                            }
                        }
                    }
                    List<Microsoft.SqlServer.Management.Smo.View> orderedViews = OrderViewsByDependencies(database);
                    // توليد سكربت الـ Views
                    foreach (Microsoft.SqlServer.Management.Smo.View view in orderedViews)
                    {
                        if (!view.IsSystemObject)
                        {

                            var viewScripts = view.Script(optionsScema);
                            foreach (var script in viewScripts)
                            {
                                writer.WriteLine(script);
                            }
                            writer.WriteLine("GO");
                            // سكربت العرض نفسه


                            // إغلاق الشرط لـ `IF EXISTS`


                            writer.WriteLine("GO");
                        }
                    }
                    // توليد سكربت الـ Stored Procedures
                    foreach (StoredProcedure sp in database.StoredProcedures)
                    {
                        if (!sp.IsSystemObject)
                        {
                            var spScripts = sp.Script(options);
                            foreach (string script in spScripts)
                            {
                                writer.WriteLine(script);
                            }
                        }
                    }

                    // توليد سكربت الدوال (Functions)
                    foreach (UserDefinedFunction function in database.UserDefinedFunctions)
                    {
                        if (!function.IsSystemObject)
                        {
                            var functionScripts = function.Script(options);
                            foreach (string script in functionScripts)
                            {
                                writer.WriteLine(script);
                            }
                        }
                    }
                    //XtraMessageBox.Show($"تم إنشاء سكربت قاعدة البيانات بنجاح في: {Path.GetFullPath(scriptFilePath)}");
                }
            }
        }
        private List<Microsoft.SqlServer.Management.Smo.View> OrderViewsByDependencies(Database database)
        {
            // خريطة لتخزين التبعيات لكل عرض
            Dictionary<string, List<string>> dependenciesMap = new Dictionary<string, List<string>>();
            HashSet<string> visited = new HashSet<string>();
            Stack<Microsoft.SqlServer.Management.Smo.View> stack = new Stack<Microsoft.SqlServer.Management.Smo.View>();

            foreach (Microsoft.SqlServer.Management.Smo.View view in database.Views)
            {
                if (!view.IsSystemObject)
                {
                    // استخراج التبعيات من نص العرض
                    List<string> dependencies = ExtractDependencies(view);
                    dependenciesMap[view.Name] = dependencies;
                }
            }

            // ترتيب العروض باستخدام الفرز الطوبولوجي
            List<Microsoft.SqlServer.Management.Smo.View> orderedViews = new List<Microsoft.SqlServer.Management.Smo.View>();
            foreach (Microsoft.SqlServer.Management.Smo.View view in database.Views)
            {
                if (!visited.Contains(view.Name) && !view.IsSystemObject)
                {
                    TopologicalSort(view, database, visited, stack, dependenciesMap);
                }
            }

            while (stack.Count > 0)
            {
                orderedViews.Add(stack.Pop());
            }

            return orderedViews;
        }

        // دالة لاستخراج أسماء الجداول والعروض من نص العرض
        private List<string> ExtractDependencies(Microsoft.SqlServer.Management.Smo.View view)
        {
            string viewText = view.TextHeader + view.TextBody;
            List<string> dependencies = new List<string>();

            // استخراج أسماء الجداول والعروض من نص العرض
            string[] words = viewText.Split(new[] { ' ', '\r', '\n', '\t', '[', ']', ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < words.Length - 1; i++)
            {
                if (string.Equals(words[i], "FROM", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(words[i], "JOIN", StringComparison.OrdinalIgnoreCase))
                {
                    dependencies.Add(words[i + 1]);
                }
            }

            return dependencies;
        }

        // دالة للفرز الطوبولوجي
        private void TopologicalSort(Microsoft.SqlServer.Management.Smo.View view, Database database, HashSet<string> visited, Stack<Microsoft.SqlServer.Management.Smo.View> stack, Dictionary<string, List<string>> dependenciesMap)
        {
            visited.Add(view.Name);

            if (dependenciesMap.ContainsKey(view.Name))
            {
                foreach (string dependencyName in dependenciesMap[view.Name])
                {
                    Microsoft.SqlServer.Management.Smo.View dependentView = database.Views.Cast<Microsoft.SqlServer.Management.Smo.View>().FirstOrDefault(v => v.Name == dependencyName);
                    if (dependentView != null && !visited.Contains(dependentView.Name))
                    {
                        TopologicalSort(dependentView, database, visited, stack, dependenciesMap);
                    }
                }
            }

            stack.Push(view);
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            btnGenerateScript.Enabled = false;
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "SQL Files |*.sql";
                if (sfd.ShowDialog() != DialogResult.OK) return;
                string scriptPath = sfd.FileName;
                await Task.Run(() => GenerateScript(scriptPath));
                MyBox.Show("تم إنشاء قاعدة البيانات الجديدة ومسح العمليات المتعلقة بالسنة الماضية بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            catch (Exception ex)
            {
                MyBox.Show($"حدث خطأ: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // إعادة تمكين الزر بعد انتهاء العملية
                btnGenerateScript.Enabled = true;
            }

        }
        private void DeleteOldYearData()
        {
            string serverName = ELNour.Data.Server.ServerName;
            string databaseName = "ElNour";
            string username = ELNour.Data.Server.UserName; // استخدم حساب Windows Authentication إذا لزم الأمر
            string password = ELNour.Data.Server.Password;
            // إنشاء اتصال بـ SQL Server
            var connectionString = new SqlConnectionStringBuilder
            {
                DataSource = serverName,
                InitialCatalog = databaseName,
                IntegratedSecurity = string.IsNullOrEmpty(username), // إذا كان فارغاً استخدم Windows Authentication
                UserID = username,
                Password = password
            }.ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string Operationquery = $"DELETE FROM Operation_tbl WHERE Year(OperationDate) <= {DateTime.Now.Year - 1}";
                SqlCommand command = new SqlCommand(Operationquery, connection);
                command.ExecuteNonQuery();
                string Recievequery = $"DELETE FROM Recieve_tbl WHERE Year(RecieveDate) <= {DateTime.Now.Year - 1}";
                command = new SqlCommand(Recievequery, connection);
                command.ExecuteNonQuery();
                string RecieveDetailsquery = $"DELETE FROM RecieveDetails_tbl WHERE Year(RecieveDate) <= {DateTime.Now.Year - 1}";
                command = new SqlCommand(RecieveDetailsquery, connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataBaseOperations;
using ELNour.Classes;
using MessageBoxes;

namespace ELNour.Data
{
    internal class Server
    {
        static DatabaseConnection con = new DatabaseConnection(Connections.Constr);
        public static string ServerName { get; set; } = "";
        public static string UserName { get; set; } = "sa";
        public static string Password { get; set; } = "123456";
        public static void Backup()
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(Connections.Constr);
                string fileName;
                fileName = @"ELNourBackUp" + System.DateTime.Now.ToString("dd_MM_yyyy_hh-mm-ss") + ".bak";
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Backup Files |*.bak";
                sfd.FileName = fileName;
                sfd.ShowDialog();
                fileName = sfd.FileName;
                if (sqlConnection.State == ConnectionState.Open) { sqlConnection.Close(); }
                sqlConnection.Open();
                string sql = "backup database ELNour to disk='" + fileName + "' with init,stats=10";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Connection = sqlConnection;
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
                MyBox.Show("تم حفظ النسخة الاحتياطية بنجاح", "حفظ", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            catch (SqlException ex)
            {
                MyBox.Show($"خطأ في الاتصال بقاعد البيانات :{Environment.NewLine} تفاصيل الخطأ : {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exe)
            {
                MyBox.Show($"خطأ غير متوقع :{Environment.NewLine} تفاصيل الخطأ : {exe.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public static void RestoreBackup()
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(Connections.Constr);
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "DB Backup Files|*.bak";
                ofd.FilterIndex = 4;
                ofd.FileName = "";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    SqlConnection.ClearAllPools();
                    if (sqlConnection.State == ConnectionState.Closed) { sqlConnection.Open(); }
                    string sql = "USE Master ALTER DATABASE ELNour SET Single_User WITH Rollback Immediate Restore database ELNour FROM disk='" + ofd.FileName + "' WITH REPLACE ALTER DATABASE ELNour SET Multi_User ";
                    SqlCommand cmd = new SqlCommand(sql);
                    cmd.Connection = sqlConnection;
                    cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    MyBox.Show("تم استرجاع النسخة الاحتياطية بنجاح", "حفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException ex)
            {
                MyBox.Show($"خطأ في الاتصال بقاعد البيانات :{Environment.NewLine} تفاصيل الخطأ : {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exe)
            {
                MyBox.Show($"خطأ غير متوقع :{Environment.NewLine} تفاصيل الخطأ : {exe.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}

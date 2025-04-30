using DataBaseOperations;
using DevExpress.XtraEditors;
using ELNour.Classes;
using ELNour.Data;
using MessageBoxes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ELNour.Frm
{
    public partial class frmUser : DevExpress.XtraEditors.XtraForm
    {
        DatabaseConnection con;
        DatabaseOperation oper;
        SqlCommand cmd;
        public frmUser()
        {
            InitializeComponent();
            con = new DatabaseConnection(Connections.Constr);
            oper = new DatabaseOperation(con);
            SetPerMission();
        }
        private void SetPerMission()
        {
            btnNewUser.Enabled = UserPermission.AddUsers;
            btnEditUser.Enabled = UserPermission.EditUsers;
            btnDeleteUser.Enabled = UserPermission.DeleteUsers;
            btnShowAll.Enabled = UserPermission.ShowUsers;
            btnExportToExcel.Enabled = UserPermission.ExportUsers;
            btnPrint.Enabled = UserPermission.printUsers;
        }
        private void GetData()
        {
            try
            {
                dgvUser.Rows.Clear();
                if (con.Connection.State == ConnectionState.Closed)
                    con.Connection.Open();

                using (cmd = new SqlCommand("SELECT Id, FullName, UserName FROM User_tbl", con.Connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dgvUser.Rows.Add(reader["Id"], reader["FullName"], reader["UserName"]);
                        }
                    }
                }
            }
            catch (SqlException ex) { MyBox.Show($"خطأ في الاتصال بقاعد البيانات :{Environment.NewLine} تفاصيل الخطأ : {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception exe) { MyBox.Show($"خطأ غير متوقع :{Environment.NewLine} تفاصيل الخطأ : {exe.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void btnNewUser_Click(object sender, EventArgs e)
        {
            new frmPerMissions().ShowDialog();
            dgvUser.Rows.Clear();
        }

        private void btnEditUser_Click(object sender, EventArgs e)
        {
            if (dgvUser.SelectedRows.Count == 0)
            {
                MyBox.Show("يجب أختيار مستخدم للتعديل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int UserId = Convert.ToInt32(dgvUser.SelectedRows[0].Cells[0].Value);
            new frmPerMissions(UserId).ShowDialog();
            dgvUser.Rows.Clear();
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (dgvUser.SelectedRows.Count == 0)
            {
                MyBox.Show("يجب أختيار مستخدم للحذف", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MyBox.Show("سيتم حذف مستخدم ؟ هل تود الاستمرار", "تنبيه", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                int UserId = Convert.ToInt32(dgvUser.SelectedRows[0].Cells[0].Value);
                oper.Delete("User_tbl", $"Id = {UserId}");
                oper.Delete("UserPermission_tbl", $"UserId = {UserId}");
                MyBox.Show("تم حذف مستخدم بنجاح", "إجراء سليم", MessageBoxButtons.OK, MessageBoxIcon.None);
                dgvUser.Rows.Clear();
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel.Excel(dgvUser);
        }
    }
}
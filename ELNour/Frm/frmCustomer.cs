using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DataBaseOperations;
using ELNour.Addes;
using ELNour.Classes;
using MessageBoxes;
using System.Data.SqlClient;
using ELNour.Data;

namespace ELNour.Frm
{
	public partial class frmCustomer: DevExpress.XtraEditors.XtraForm
	{
        DatabaseConnection con;
        DatabaseOperation oper;
        SqlCommand cmd;
        public frmCustomer()
        {
            InitializeComponent();
            con = new DatabaseConnection(Connections.Constr);
            oper = new DatabaseOperation(con);
            SetPerMission();
        }
        private void SetPerMission()
        {
            btnNewCustomer.Enabled = UserPermission.AddCustomer;
            btnEditCustomer.Enabled = UserPermission.EditCustomer;
            btnDeleteCustomer.Enabled = UserPermission.DeleteCustomer;
            btnShowAll.Enabled = UserPermission.ShowCustomer;
            btnExportToExcel.Enabled = UserPermission.ExportCustomer;
            btnPrint.Enabled = UserPermission.PrintCustomer;
        }
        private void GetData()
        {
            try
            {
                dgvImporter.Rows.Clear();
                if (con.Connection.State == ConnectionState.Closed)
                    con.Connection.Open();

                using (cmd = new SqlCommand("SELECT Id, Name FROM Vendor_tbl where Id > 0 and Type =1 ", con.Connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dgvImporter.Rows.Add(reader["Id"], reader["Name"]);
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
        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (dgvImporter.Rows.Count == 0)
            {
                MyBox.Show("يجب أختيار البيانات المراد استخراجها", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ExportToExcel.Excel(dgvImporter);
        }

        private void btnNewCustomer_Click(object sender, EventArgs e)
        {
            new frmAddEditCustomer().ShowDialog();
            dgvImporter.Rows.Clear();
        }

        private void btnEditCustomer_Click(object sender, EventArgs e)
        {
            if (dgvImporter.SelectedRows.Count == 0)
            {
                MyBox.Show("يجب أختيار عميل للتعديل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int customerId = (int)dgvImporter.CurrentRow.Cells[0].Value;
            new frmAddEditCustomer(customerId).ShowDialog();
            dgvImporter.Rows.Clear();
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            if (dgvImporter.SelectedRows.Count == 0)
            {
                MyBox.Show("يجب أختيار عميل للحذف", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MyBox.Show("سيتم حذف عميل ؟ هل تود الاستمرار", "تنبيه", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                int customerId = (int)dgvImporter.CurrentRow.Cells[0].Value;
                oper.Delete("Vendor_tbl", $"Id = {customerId}");
                MyBox.Show("تم حذف عميل بنجاح", "إجراء سليم", MessageBoxButtons.OK, MessageBoxIcon.None);
                dgvImporter.Rows.Clear();
            }
        }
    }
}
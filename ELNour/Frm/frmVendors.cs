using DataBaseOperations;
using DevExpress.XtraEditors;
using ELNour.Addes;
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
    public partial class frmVendors : DevExpress.XtraEditors.XtraForm
    {
        DatabaseConnection con ;
        DatabaseOperation oper ;
        SqlCommand cmd;
        public frmVendors()
        {
            InitializeComponent();
            con = new DatabaseConnection(Connections.Constr);
            oper = new DatabaseOperation(con);
            SetPerMission();
        }
        private void SetPerMission()
        {
            btnNewImporter.Enabled = UserPermission.AddVendor;
            btnEditImporter.Enabled = UserPermission.EditVendor;
            btnDeleteImporter.Enabled = UserPermission.DeleteVendor;
            btnShowAll.Enabled = UserPermission.ShowVendor;
            btnExportToExcel.Enabled = UserPermission.ExportVendor;
            btnPrint.Enabled = UserPermission.PrintVendor;
        }
        private void GetData()
        {
            try
            {
                dgvImporter.Rows.Clear();
                if (con.Connection.State == ConnectionState.Closed)
                    con.Connection.Open();

                using (cmd = new SqlCommand("SELECT Id, Name FROM Vendor_tbl where Id > 0 and Type = 0", con.Connection))
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
        private void btnNewImporter_Click(object sender, EventArgs e)
        {
            new frmAddEditVendor().ShowDialog();
            dgvImporter.Rows.Clear();
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void btnEditImporter_Click(object sender, EventArgs e)
        {
            if (dgvImporter.SelectedRows.Count == 0)
            {
                MyBox.Show("يجب أختيار مورد للتعديل","تنبيه",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int vendorId = (int)dgvImporter.CurrentRow.Cells[0].Value;
            new frmAddEditVendor(vendorId).ShowDialog();
            dgvImporter.Rows.Clear();
        }

        private void btnDeleteImporter_Click(object sender, EventArgs e)
        {
            if (dgvImporter.SelectedRows.Count == 0)
            {
                MyBox.Show("يجب أختيار مورد للحذف", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(MyBox.Show("سيتم حذف مورد ؟ هل تود الاستمرار", "تنبيه", MessageBoxButtons.YesNo, MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                int vendorId = (int)dgvImporter.CurrentRow.Cells[0].Value;
                oper.Delete("Vendor_tbl", $"Id = {vendorId}");
                MyBox.Show("تم حذف مورد بنجاح", "إجراء سليم", MessageBoxButtons.OK, MessageBoxIcon.None);
                dgvImporter.Rows.Clear();
            }
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
    }
}
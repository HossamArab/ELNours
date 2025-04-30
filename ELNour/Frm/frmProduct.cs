using DataBaseOperations;
using DevExpress.Data.Svg;
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
    public partial class frmProduct : DevExpress.XtraEditors.XtraForm
    {
        DatabaseConnection con;
        DatabaseOperation oper;
        SqlCommand cmd;
        public frmProduct()
        {
            InitializeComponent();
            con = new DatabaseConnection(Connections.Constr);
            oper = new DatabaseOperation(con);
            SetPerMission();
        }
        private void SetPerMission()
        {
            btnNewProduct.Enabled = UserPermission.AddProduct;
            btnEditProduct.Enabled = UserPermission.EditProduct;
            btnDeleteProduct.Enabled = UserPermission.DeleteProduct;
            btnShowAll.Enabled = UserPermission.ShowProduct;
            btnExportToExcel.Enabled = UserPermission.ExportProduct;
            btnPrint.Enabled = UserPermission.PrintProduct;
        }
        private void GetData()
        {
            try
            {
                dgvProduct.Rows.Clear();
                if (con.Connection.State == ConnectionState.Closed)
                    con.Connection.Open();

                using (cmd = new SqlCommand("SELECT Id, Name FROM Product_tbl Where Id > 0", con.Connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dgvProduct.Rows.Add(reader["Id"], reader["Name"]);
                        }
                    }
                }
            }
            catch (SqlException ex) { MyBox.Show($"خطأ في الاتصال بقاعد البيانات :{Environment.NewLine} تفاصيل الخطأ : {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception exe) { MyBox.Show($"خطأ غير متوقع :{Environment.NewLine} تفاصيل الخطأ : {exe.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnNewProduct_Click(object sender, EventArgs e)
        {
            new frmAddEditProduct().ShowDialog();
            dgvProduct.Rows.Clear();
        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            if (dgvProduct.SelectedRows.Count == 0)
            {
                MyBox.Show("يجب أختيار منتج للتعديل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int productId = (int)dgvProduct.CurrentRow.Cells[0].Value;
            new frmAddEditProduct(productId).ShowDialog();
            dgvProduct.Rows.Clear();
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            if (dgvProduct.SelectedRows.Count == 0)
            {
                MyBox.Show("يجب أختيار منتج للحذف", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MyBox.Show("سيتم حذف منتج ؟ هل تود الاستمرار", "تنبيه", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                int productId = (int)dgvProduct.CurrentRow.Cells[0].Value;
                oper.Delete("Product_tbl", $"Id = {productId}");
                MyBox.Show("تم حذف منتج بنجاح", "إجراء سليم", MessageBoxButtons.OK, MessageBoxIcon.None);
                dgvProduct.Rows.Clear();
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (dgvProduct.Rows.Count == 0)
            {
                MyBox.Show("يجب أختيار البيانات المراد استخراجها", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ExportToExcel.Excel(dgvProduct);
        }
    }
}
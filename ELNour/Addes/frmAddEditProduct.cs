using DataBaseOperations;
using DevExpress.XtraEditors;
using ELNour.Classes;
using ELNour.Services;
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

namespace ELNour.Addes
{
    public partial class frmAddEditProduct : DevExpress.XtraEditors.XtraForm
    {
        DatabaseConnection con;
        DatabaseOperation oper;
        SqlCommand cmd;
        IMaxID max = new MaxID();
        bool IsEdit = false;
        public frmAddEditProduct(int productId = -1)
        {
            InitializeComponent();
            con = new DatabaseConnection(Connections.Constr);
            oper = new DatabaseOperation(con);
            loadData(productId);
        }
        private void ClearFields()
        {
            txtID.Text = string.Empty;
            txtName.Text = string.Empty;
            IsEdit = false;
        }
        private void loadData(int productId = -1)
        {
            if (productId == -1)
            {
                ClearFields();
                txtName.Focus();
                txtName.Select();
                return;
            }
            try
            {

                if (con.Connection.State == ConnectionState.Closed)
                    con.Connection.Open();

                using (cmd = new SqlCommand("SELECT Id, Name FROM Product_tbl WHERE Id = @ProductId", con.Connection))
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtID.Text = reader["Id"].ToString();
                            txtName.Text = reader["Name"].ToString();
                            IsEdit = true;
                        }
                        else
                        {
                            ClearFields();
                        }
                    }
                }
                txtName.Focus();
                txtName.Select();
            }
            catch (SqlException ex) { MyBox.Show($"خطأ في الاتصال بقاعد البيانات :{Environment.NewLine} تفاصيل الخطأ : {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception exe) { MyBox.Show($"خطأ غير متوقع :{Environment.NewLine} تفاصيل الخطأ : {exe.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void Save()
        {
            try
            {
                txtID.Text = (max.MaxIDs("Id", "Product_tbl") + 1).ToString();
                Dictionary<string, object> ProductData = new Dictionary<string, object>
                {
                    {"Id",Convert.ToInt32(txtID.Text) },
                    {"Name",txtName.Text },
                };
                if (con.Connection.State == ConnectionState.Closed)
                    con.Connection.Open();
                oper.Insert("Product_tbl", ProductData);
                ClearFields();
                MyBox.Show("تم الحفظ بنجاح", "إجراء سليم", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            catch (SqlException ex) { MyBox.Show($"خطأ في الاتصال بقاعد البيانات :{Environment.NewLine} تفاصيل الخطأ : {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception exe) { MyBox.Show($"خطأ غير متوقع :{Environment.NewLine} تفاصيل الخطأ : {exe.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void Edit()
        {
            try
            {


                Dictionary<string, object> ProductData = new Dictionary<string, object>
                {
                    {"Id",Convert.ToInt32(txtID.Text) },
                    {"Name",txtName.Text },
                };
                if (con.Connection.State == ConnectionState.Closed)
                    con.Connection.Open();
                oper.Update("Product_tbl", ProductData, $"Id = {Convert.ToInt32(txtID.Text)}");
                ClearFields();
                MyBox.Show("تم الحفظ بنجاح", "إجراء سليم", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            catch (SqlException ex) { MyBox.Show($"خطأ في الاتصال بقاعد البيانات :{Environment.NewLine} تفاصيل الخطأ : {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception exe) { MyBox.Show($"خطأ غير متوقع :{Environment.NewLine} تفاصيل الخطأ : {exe.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void SaveOrEdit()
        {
            if (IsEdit)
            {
                Edit();
                return;
            }
            Save();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MyBox.Show("يجب إدخال الأسم", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtName.Focus();
                return;
            }
            SaveOrEdit();
        }
        private void frmAddEditProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) { ClearFields(); }
            if (e.KeyCode == Keys.F3) { btnSave_Click(null, null); }
        }
    }
}
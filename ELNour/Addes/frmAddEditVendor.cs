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
    public partial class frmAddEditVendor : DevExpress.XtraEditors.XtraForm
    {
        DatabaseConnection con;
        DatabaseOperation oper;
        SqlCommand cmd;
        IMaxID max = new MaxID();
        bool IsEdit = false;
        public frmAddEditVendor(int vendorId = -1)
        {
            InitializeComponent();
            con = new DatabaseConnection(Connections.Constr);
            oper = new DatabaseOperation(con);
            loadData(vendorId);
        }
        private void ClearFields()
        {
            txtID.Text = string.Empty;
            txtName.Text = string.Empty;
            IsEdit = false ;
        }
        private void loadData(int vendorId = -1)
        {
            if (vendorId == -1)
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

                using (cmd = new SqlCommand("SELECT Id, Name FROM Vendor_tbl WHERE Id = @VendorId", con.Connection))
                {
                    cmd.Parameters.AddWithValue("@VendorId", vendorId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtID.Text = reader["Id"].ToString();
                            txtName.Text = reader["Name"].ToString();
                            IsEdit = true ;
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
            catch (SqlException ex) { MyBox.Show($"خطأ في الاتصال بقاعد البيانات :{Environment.NewLine} تفاصيل الخطأ : {ex.Message}","خطأ",MessageBoxButtons.OK,MessageBoxIcon.Error); }
            catch(Exception exe) { MyBox.Show($"خطأ غير متوقع :{Environment.NewLine} تفاصيل الخطأ : {exe.Message}","خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void Save()
        {
            try
            {
                txtID.Text = (max.MaxIDs("Id", "Vendor_tbl") + 1).ToString();
                Dictionary<string, object> VendorData = new Dictionary<string, object>
                {
                    {"Id",Convert.ToInt32(txtID.Text) },
                    {"Name",txtName.Text },
                };
                if (con.Connection.State == ConnectionState.Closed)
                    con.Connection.Open();
                oper.Insert("Vendor_tbl", VendorData);
                MyBox.Show("تم الحفظ بنجاح", "إجراء سليم", MessageBoxButtons.OK, MessageBoxIcon.None);
                ClearFields();
            }
            catch (SqlException ex) { MyBox.Show($"خطأ في الاتصال بقاعد البيانات :{Environment.NewLine} تفاصيل الخطأ : {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception exe) { MyBox.Show($"خطأ غير متوقع :{Environment.NewLine} تفاصيل الخطأ : {exe.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void Edit()
        {
            try
            {
                
                    
                Dictionary<string, object> VendorData = new Dictionary<string, object>
                {
                    {"Id",Convert.ToInt32(txtID.Text) },
                    {"Name",txtName.Text },
                };
                if (con.Connection.State == ConnectionState.Closed)
                    con.Connection.Open();
                oper.Update("Vendor_tbl", VendorData, $"Id = {Convert.ToInt32(txtID.Text)}");
                MyBox.Show("تم الحفظ بنجاح", "إجراء سليم", MessageBoxButtons.OK, MessageBoxIcon.None);
                ClearFields();
            }
            catch (SqlException ex) { MyBox.Show($"خطأ في الاتصال بقاعد البيانات :{Environment.NewLine} تفاصيل الخطأ : {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception exe) { MyBox.Show($"خطأ غير متوقع :{Environment.NewLine} تفاصيل الخطأ : {exe.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void SaveOrEdit()
        {
            if(IsEdit)
            {
                Edit();
                return;
            }
            Save();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtName.Text))
            {
                MyBox.Show("يجب إدخال الأسم", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtName.Focus();
                return;
            }
            SaveOrEdit();
        }
        private void frmAddEditVendor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) { ClearFields(); }
            if (e.KeyCode == Keys.F3) { btnSave_Click(null,null); }
        }
    }
}
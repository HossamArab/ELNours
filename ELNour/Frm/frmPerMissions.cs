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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ELNour.Frm
{
    public partial class frmPerMissions : DevExpress.XtraEditors.XtraForm
    {
        DatabaseConnection con;
        DatabaseOperation oper;
        SqlCommand cmd;
        IMaxID max = new MaxID();
        bool IsEdit = false;
        public frmPerMissions(int UserId = -1)
        {
            InitializeComponent();
            con = new DatabaseConnection(Connections.Constr);
            oper = new DatabaseOperation(con);
            LoadUser(UserId);
        }
        private void ClearFields()
        {
            txtID.Text = string.Empty;
            txtName.Text = string.Empty;
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfimPassword.Text = string.Empty;
            chkPassword.Checked = false;
            chkConfimPassword.Checked = false;
            IsEdit = false;
            ResetCheckBoxesRecursive(pnlPermission);
        }
        public void ResetCheckBoxesRecursive(Control container)
        {
            // المرور على جميع العناصر داخل الحاوية
            foreach (Control control in container.Controls)
            {
                // التحقق إذا كان العنصر هو CheckBox
                if (control is CheckEdit checkBox)
                {
                    // إعادة تعيين القيمة إلى false
                    checkBox.Checked = false;
                }
                else if (control.HasChildren)
                {
                    // إذا كانت الحاوية تحتوي على عناصر فرعية، استدعِ الدالة بشكل تكراري
                    ResetCheckBoxesRecursive(control);
                }
            }
        }
        private void LoadCheckBoxValuesRecursive(Control container, SqlDataReader reader)
        {
            foreach (Control control in container.Controls)
            {
                // إذا كان العنصر هو CheckEdit
                if (control is DevExpress.XtraEditors.CheckEdit checkEdit)
                {
                    if (checkEdit.Tag != null && !string.IsNullOrEmpty(checkEdit.Tag.ToString()))
                    {
                        string columnName = checkEdit.Tag.ToString();

                        // التحقق إذا كان العمود موجودًا في النتيجة
                        if (!reader.IsDBNull(reader.GetOrdinal(columnName)))
                        {
                            bool isChecked = reader.GetBoolean(reader.GetOrdinal(columnName));
                            checkEdit.Checked = isChecked; // تعيين حالة التحديد
                        }
                        else
                        {
                            checkEdit.Checked = false; // قيمة افتراضية إذا كانت القيمة NULL
                        }
                    }
                }
                // إذا كانت الحاوية تحتوي على عناصر فرعية، استدعِ الدالة بشكل تكراري
                else if (control.HasChildren)
                {
                    LoadCheckBoxValuesRecursive(control, reader);
                }
            }
        }
        public void LoadCheckBoxValuesFromDatabase(Control container, string query)
        {
            if(con.Connection.State == ConnectionState.Open)
                con.CloseConnection();
            // قراءة البيانات من قاعدة البيانات
                con.OpenConnection();
                using (cmd = new SqlCommand(query, con.Connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                            // المرور على جميع العناصر داخل الحاوية
                                LoadCheckBoxValuesRecursive(container, reader);
                            }
                        }
                    }
                }
                IsEdit = true;
        }
        private void LoadUser(int UserId = -1)
        {
            try
            {
                if (con.Connection.State == ConnectionState.Closed)
                    con.Connection.Open();
                using (cmd = new SqlCommand("SELECT * FROM User_tbl WHERE Id = @UserId", con.Connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtID.Text = reader["Id"].ToString();
                            txtName.Text = reader["FullName"].ToString();
                            txtUserName.Text = reader["UserName"].ToString();
                            txtPassword.Text = reader["Password"].ToString();
                            txtConfimPassword.Text = reader["Password"].ToString();
                            string query = "SELECT * FROM UserPermission_tbl WHERE UserId = " + UserId + " ";
                            LoadCheckBoxValuesFromDatabase(pnlPermission, query);
                            IsEdit = true;
                        }
                        else
                        {
                            ClearFields();
                            txtName.Focus();
                            txtName.Select();
                            return;
                        }
                    }
                }
                con.CloseConnection();
            }
            catch (SqlException ex) { MyBox.Show($"خطأ في الاتصال بقاعد البيانات :{Environment.NewLine} تفاصيل الخطأ : {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception exe) { MyBox.Show($"خطأ غير متوقع :{Environment.NewLine} تفاصيل الخطأ : {exe.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void Save()
        {
            txtID.Text = (max.MaxIDs("Id", "User_tbl") + 1).ToString();
            if (con.Connection.State == ConnectionState.Closed)
                con.Connection.Open();
            con.BeginTransaction();

            try
            {
                Dictionary<string, object> UserData = new Dictionary<string, object> // بيانات العملية الأساسية
                {
                    {"Id",Convert.ToInt32(txtID.Text) },
                    {"FullName",txtName.Text},
                    {"UserName",txtUserName.Text },
                    {"Password",txtPassword.Text },
                };
                oper.InsertWithTransaction("User_tbl", UserData);
                oper.InsertWithTransaction("UserPermission_tbl", GetCheckBoxStatesRecursive(pnlPermission, Convert.ToInt32(txtID.Text)));
                con.CommitTransaction();
                IsEdit = true;
                MyBox.Show("تم الحفظ بنجاح", "تم الحفظ", MessageBoxButtons.OK, MessageBoxIcon.None);

            }
            catch (Exception ex)
            {
                con.RollbackTransaction();
                MyBox.Show($"خطأ غير متوقع : {Environment.NewLine} {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                con.CloseConnection();
            }
        }
        private void Edit()
        {
            try
            {
                if (con.Connection.State == ConnectionState.Closed)
                    con.Connection.Open();
                con.BeginTransaction();

                Dictionary<string, object> UserData = new Dictionary<string, object>
                {
                    {"FullName",txtName.Text},
                    {"UserName",txtUserName.Text },
                    {"Password",txtPassword.Text },
                };
                if (con.Connection.State == ConnectionState.Closed)
                    con.Connection.Open();
                oper.UpdateWithTransaction("User_tbl", UserData, $"Id = {Convert.ToInt32(txtID.Text)}");
                oper.UpdateWithTransaction("UserPermission_tbl", GetCheckBoxStatesRecursive(pnlPermission, Convert.ToInt32(txtID.Text)), $"UserId = {Convert.ToInt32(txtID.Text)}");
                con.CommitTransaction();
                
                MyBox.Show("تم الحفظ بنجاح", "إجراء سليم", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            catch (Exception exe) { con.RollbackTransaction(); MyBox.Show($"خطأ غير متوقع :{Environment.NewLine} تفاصيل الخطأ : {exe.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            finally
            {
                con.CloseConnection();
            }
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
        public Dictionary<string, object> GetCheckBoxStatesRecursive(Control container,int UserID)
        {
            Dictionary<string, object> checkBoxStates = new Dictionary<string, object>();

            foreach (Control control in container.Controls)
            {
                if (control is CheckEdit checkBox)
                {
                    if (checkBox.Tag != null && !string.IsNullOrEmpty(checkBox.Tag.ToString()))
                    {
                        checkBoxStates[checkBox.Tag.ToString()] = checkBox.Checked;
                    }
                }
                else if (control.HasChildren)
                {
                    // إذا كانت الحاوية تحتوي على عناصر فرعية، استدعِ الدالة بشكل تكراري
                    var nestedStates = GetCheckBoxStatesRecursive(control, UserID);
                    foreach (var state in nestedStates)
                    {
                        checkBoxStates[state.Key] = state.Value;
                    }
                }
            }
            checkBoxStates["UserId"] = UserID;
            return checkBoxStates;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtName.Text))
            {
                MyBox.Show("الرجاء إدخال اسم الموظف", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                MyBox.Show("الرجاء إدخال اسم المستخدم", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MyBox.Show("الرجاء إدخال كلمة المرور", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtConfimPassword.Text))
            {
                MyBox.Show("الرجاء إدخال تأكيد كلمة المرور", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtConfimPassword.Focus();
                return;
            }
            if (txtPassword.Text != txtConfimPassword.Text)
            {
                MyBox.Show("كلمة المرور وتأكيد كلمة المرور غير متطابقتين", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtConfimPassword.Focus();
                return;
            }
            SaveOrEdit();
        }

        private void chkPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.Properties.UseSystemPasswordChar = !chkPassword.Checked;
        }

        private void chkConfimPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtConfimPassword.Properties.UseSystemPasswordChar = !chkConfimPassword.Checked;
        }
    }
}
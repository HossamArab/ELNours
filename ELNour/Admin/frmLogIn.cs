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

namespace ELNour.Admin
{
    public partial class frmLogIn : DevExpress.XtraEditors.XtraForm
    {
        DatabaseConnection con;
        SqlCommand cmd;

        public frmLogIn()
        {
            InitializeComponent();
            con = new DatabaseConnection(Connections.Constr);
        }
        private void Login()
        {
            if (txtUserName.Text == "" || txtPassword.Text == "")
            {
                MyBox.Show("الرجاء ادخال اسم المستخدم وكلمة المرور", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (con.Connection.State == ConnectionState.Closed)
                con.Connection.Open();

            using (cmd = new SqlCommand("SELECT * FROM User_tbl where UserName = '" + txtUserName.Text + "' and Password = '" + txtPassword.Text + "'", con.Connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            User.UserID = Convert.ToInt32(reader["Id"]);
                            User.UserName = reader["UserName"].ToString();
                            User.FullName = reader["FullName"].ToString();
                            string query = "SELECT * FROM UserPermission_tbl WHERE UserId = " + User.UserID + "";
                            // استدعاء الدالة للحصول على الصلاحيات
                            UserPermission permissions = User.GetUserPermissions<UserPermission>(query);
                        }
                        con.CloseConnection();
                        MyBox.Show("تم تسجيل الدخول بنجاح", "تسجيل دخول", MessageBoxButtons.OK, MessageBoxIcon.None);
                        this.Close();
                    }
                    else
                    {
                        MyBox.Show("اسم المستخدم أو كلمة المرور غير صحيحة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUserName.Focus();
                        txtUserName.Select();
                        txtPassword.Text = "";
                    }
                }
            }
           
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }
    }
}
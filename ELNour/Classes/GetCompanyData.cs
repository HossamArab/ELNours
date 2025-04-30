using MessageBoxes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using DataBaseOperations;
using System.Drawing;

namespace ELNour.Classes
{
    internal class GetCompanyData
    {
        DatabaseConnection con;
        DatabaseOperation oper;
        SqlCommand cmd;
        public string CompanyName = "";
        public string CompanyNameEn = "";
        public string CompanyDescription = "";
        public string CompanyPhone = "";
        public string CompanyMobile = "";
        public string CompanyEmail = "";
        public string CompanyAddress = "";
        public Image CompanyLogo = null;
        public GetCompanyData()
        {
            con = new DatabaseConnection(Connections.Constr);
            oper = new DatabaseOperation(con);
            GetMainData();
        }
        private void GetMainData()
        {
            try
            {

                if (con.Connection.State == ConnectionState.Closed)
                    con.Connection.Open();

                using (cmd = new SqlCommand("SELECT * FROM Organization_tbl WHERE Id = 1", con.Connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            CompanyName = reader["OrgName"].ToString();
                            CompanyNameEn = reader["OrgNameEn"].ToString();
                            CompanyDescription = reader["OrgDescription"].ToString();
                            CompanyPhone = reader["OrgPhone"].ToString();
                            CompanyMobile = reader["OrgMobile"].ToString();
                            CompanyEmail = reader["OrgEmail"].ToString();
                            CompanyAddress = reader["OrgAddress"].ToString();
                            try
                            {
                                Image image = null;
                                CompanyLogo = image.ArraytoPicture((byte[])reader["LogoName"]);
                            }
                            catch { }
                        }
                    }
                }
            }
            catch (SqlException ex) { MyBox.Show($"خطأ في الاتصال بقاعد البيانات :{Environment.NewLine} تفاصيل الخطأ : {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception exe) { MyBox.Show($"خطأ غير متوقع :{Environment.NewLine} تفاصيل الخطأ : {exe.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}

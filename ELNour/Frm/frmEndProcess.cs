using DataBaseOperations;
using DevExpress.Charts.Native;
using DevExpress.XtraEditors;
using ELNour.Classes;
using ELNour.Data;
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

namespace ELNour.Frm
{
    public partial class frmEndProcess : DevExpress.XtraEditors.XtraForm
    {
        DatabaseConnection con;
        DatabaseOperation oper;
        GetCompanyData Company = new GetCompanyData();
        GetPrinterData Printer = new GetPrinterData();
        IMaxID max = new MaxID();
        private readonly Fills fills = new Fills();
        public frmEndProcess(int RecieveId)
        {
            InitializeComponent();
            con = new DatabaseConnection(Connections.Constr);
            oper = new DatabaseOperation(con);
            FillData(RecieveId);
        }
        private DataTable Data (int RecieveId)
        {
            DataTable dt = new DataTable();
            try
            {
                if (con.Connection.State == ConnectionState.Closed)
                    con.Connection.Open();
                string query = $@"
                                SELECT
                                    InProcess_tbl.Id AS ProcessId,
                                    InProcess_tbl.RecieveId AS RecieveId,
                                    InProcess_tbl.VendorId,
                                    InProcess_tbl.RecieveDate,
                                    InProcess_tbl.BoxesCount,
                                    InProcess_tbl.PalleteWeight,
                                    InProcess_tbl.BoxesWeight,
                                    InProcess_tbl.QA,
                                    InProcess_tbl.Notes,
                                    InProcess_tbl.UserId,
                                    InProcess_tbl.BoxWeight,
                                    InProcess_tbl.Weight,
                                    InProcess_tbl.DiscountWeight,
                                    InProcess_tbl.TotalDiscount,
                                    InProcess_tbl.NetWeight,
                                    InProcess_tbl.ProductId,
                                    Product_tbl.Name AS ProductName,
                                    Vendor_tbl.Name AS VendorName
                                FROM
                                    InProcess_tbl
                                INNER JOIN Product_tbl ON InProcess_tbl.ProductId = Product_tbl.Id
                                INNER JOIN Vendor_tbl ON Vendor_tbl.Id = InProcess_tbl.VendorId
                                AND 
                                    IsNull(InProcess_tbl.IsProcessed,0) = 0 And InProcess_tbl.RecieveId ={RecieveId}
                                ORDER BY 
                                    InProcess_tbl.Id ";

                using (SqlDataAdapter da = new SqlDataAdapter(query, con.Connection))
                {
                    da.Fill(dt);
                }
                    return dt;
            }
            catch(Exception ex) 
            {
                MyBox.Show($"خطأ غير متوقع : {Environment.NewLine} {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dt = null;
                return dt;
            }
            finally
            {
                if (con.Connection.State == ConnectionState.Open) { con.CloseConnection(); }
            }
        }
        public void CulcInv()
        {
            decimal TotalCount = 0;
            decimal TotalNetWeight = 0;
            decimal DiscountWeight = 0;
            decimal Weight = 0;
            decimal RecieveWeight = Convert.ToDecimal(lblRecieveWeight.Text);
            decimal BadWeight = Convert.ToDecimal(lblBadWeight.Text);
            decimal newWeight = 0;
            DataGridViewRowCollection rows = dgvSale.Rows;
            foreach (DataGridViewRow row in rows)
            {
                TotalCount += Convert.ToDecimal(row.Cells[1].Value);
                Weight += Convert.ToDecimal(row.Cells[3].Value);
                DiscountWeight += Convert.ToDecimal(row.Cells[8].Value);
                TotalNetWeight += Convert.ToDecimal(row.Cells[9].Value);
            }
            lblCount.Text = dgvSale.Rows.Count.ToString();
            lblBoxCount.Text = TotalCount.ToString();
            lblTotalWeight.Text = Weight.ToString();
            lblDiscountWeight.Text = DiscountWeight.ToString();
            lblTotalNetWeight.Text = TotalNetWeight.ToString();
            newWeight = TotalNetWeight + BadWeight;
            lblDifferentWeight.Text = Math.Round(RecieveWeight - newWeight,3).ToString();
        }
        private void FillData(int RecieveId)
        {
            fills.fillComboBox(cmbVendor, "Vendor_tbl", "Id", "Name");
            cmbVendor.SelectedIndex = -1;
            Salesman.Text = User.FullName;
            txtDate.Value = DateTime.Now;
            DataTable dt = new DataTable();
            dt = Data(RecieveId);
            // الحصول على الصفوف المحددة من DataGridView
            DataRowCollection rows = dt.Rows;
            foreach (DataRow row in rows)
            {                
                int VendorId = Convert.ToInt32(row["VendorId"]);
                if (cmbVendor.SelectedIndex == -1)
                {
                    cmbVendor.SelectedValue = VendorId;
                    txtID.Text = VendorId.ToString();
                }
                else if (cmbVendor.SelectedValue != null && Convert.ToInt32(cmbVendor.SelectedValue) != VendorId)
                {
                    MyBox.Show("يجب ألا يكون هناك موردان لفاتورة واحدة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrEmpty(txtRecieveId.Text))
                {
                    
                    txtRecieveId.Text = row["RecieveId"].ToString();
                }
                // إضافة البيانات إلى جدول المبيعات
                dgvSale.Rows.Add(
                    row["ProductId"],
                    row["BoxesCount"],
                    row["ProductName"],
                    row["Weight"],
                    row["PalleteWeight"],
                    row["BoxWeight"],
                    row["BoxesWeight"],
                    row["DiscountWeight"],
                    row["TotalDiscount"],
                    row["NetWeight"],
                    row["QA"],
                    row["UserId"],
                    row["Notes"],
                    row["RecieveDate"],
                    row["ProcessId"]
                );
                
            }
            CulcInv();
        }

        private void lblRecieveWeight_TextChanged(object sender, EventArgs e)
        {
            CulcInv();
        }
    }
}
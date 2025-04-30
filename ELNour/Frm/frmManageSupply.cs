using DataBaseOperations;
using DevExpress.XtraEditors;
using ELNour.Classes;
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
    public partial class frmManageSupply : DevExpress.XtraEditors.XtraForm
    {
        DatabaseConnection con;
        private readonly Fills fills = new Fills();
        public frmManageSupply()
        {
            InitializeComponent();
            con = new DatabaseConnection(Connections.Constr);
            LoadData();
        }
        private void LoadData()
        {
            txtFromDate.Value = DateTime.Today;
            txtToDate.Value = DateTime.Today.AddHours(24);
            Search();
            fills.fillComboBox(cmbVendor, "Vendor_tbl", "Id", "Name", "Type", 1);
            fills.fillComboBox(cmbProduct, "Product_tbl", "Id", "Name");
            cmbVendor.SelectedIndex = -1;
            cmbProduct.SelectedIndex = -1;
        }
        public void Calc_Inv()
        {
            decimal totalPrice = 0;
            decimal totalCount = 0;
            decimal totalNetWeight = 0;

            foreach (DataGridViewRow row in dgvOperation.Rows)
            {
                if (row.Cells[7].Value != null && row.Cells[11].Value != null && row.Cells[12].Value != null)
                {
                    totalPrice += Convert.ToDecimal(row.Cells[5].Value);
                    totalCount += Convert.ToDecimal(row.Cells[11].Value);
                    totalNetWeight += Convert.ToDecimal(row.Cells[12].Value);
                }
            }

            lblTotalPrice.Text = totalPrice.ToString();
            lblTotalCount.Text = totalCount.ToString();
            lblTotalWeight.Text = totalNetWeight.ToString();
        }

        // جلب البيانات من قاعدة البيانات
        private void Search()
        {
            string condition = "(Recieve_tbl.RecieveDate >= @date1 AND Recieve_tbl.RecieveDate <= @date2) AND ";

            if (cmbVendor.SelectedIndex != -1 && Convert.ToInt32(cmbVendor.SelectedValue) != 0)
            {
                condition += "Recieve_tbl.VendorId = " + Convert.ToInt32(cmbVendor.SelectedValue) + " AND ";
            }

            if (cmbProduct.SelectedIndex != -1 && Convert.ToInt32(cmbProduct.SelectedValue) != 0)
            {
                condition += "RecieveDetails_tbl.ProductId = " + Convert.ToInt32(cmbProduct.SelectedValue) + " AND ";
            }
            // إزالة الكلمة الأخيرة "AND"
            if (!string.IsNullOrEmpty(condition))
            {
                condition = condition.TrimEnd(" AND".ToCharArray());
            }

            GetData(condition);
            Calc_Inv();
        }

        // استعلام قاعدة البيانات وإضافة الصفوف إلى DataGridView
        private void GetData(string condition)
        {
            try
            {
                dgvOperation.SuspendLayout();
                dgvOperation.Rows.Clear();
                if (con.Connection.State == ConnectionState.Closed) { con.OpenConnection(); }

                string query = @"
                SELECT 
                    Recieve_tbl.Id AS RecieveId,
                    Recieve_tbl.RecieveDate AS RecieveDate,
                    Recieve_tbl.VendorId AS VendorID,
                    RecieveDetails_tbl.ProductId AS ProductID,
                    RecieveDetails_tbl.Weight AS Weight,
                    RecieveDetails_tbl.BoxesCount AS Count,
                    RecieveDetails_tbl.BoxWeight AS BoxWeight,
                    RecieveDetails_tbl.PalleteWeight AS PalleteWeight,
                    RecieveDetails_tbl.BoxesWeight AS BoxesWeight,
                    RecieveDetails_tbl.DiscountWeight AS DiscountWeight,
                    RecieveDetails_tbl.TotalDiscount AS TotalDiscount,
                    RecieveDetails_tbl.NetWeight AS NetWeight,
                    Product_tbl.Name AS ProductName,
                    Vendor_tbl.Name AS VendorName,
                    User_tbl.FullName AS UserName
                FROM 
                    Recieve_tbl
                INNER JOIN 
                    RecieveDetails_tbl ON RecieveDetails_tbl.RecieveId = Recieve_tbl.Id
                INNER JOIN 
                    Product_tbl ON Product_tbl.Id = RecieveDetails_tbl.ProductId
                INNER JOIN 
                    Vendor_tbl ON Vendor_tbl.Id = Recieve_tbl.VendorId
                INNER JOIN 
                    User_tbl ON User_tbl.Id = Recieve_tbl.UserId
                WHERE 
                    Recieve_tbl.Type = 1 AND " + condition + @"
                ORDER BY 
                    Recieve_tbl.Id"
                ;

                using (SqlDataAdapter da = new SqlDataAdapter(query, con.Connection))
                {
                    if (!string.IsNullOrEmpty(condition))
                    {
                        da.SelectCommand.Parameters.Add("@date1", SqlDbType.DateTime).Value = txtFromDate.Value;
                        da.SelectCommand.Parameters.Add("@date2", SqlDbType.DateTime).Value = txtToDate.Value;
                    }

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        AddRowToDataGridView(row);
                    }
                }

                dgvOperation.ResumeLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con.Connection.State == ConnectionState.Open) { con.CloseConnection(); }
            }
        }

        // إضافة صف إلى DataGridView
        private void AddRowToDataGridView(DataRow row)
        {
            int rowIndex = dgvOperation.Rows.Add();
            DataGridViewRow currentRow = dgvOperation.Rows[rowIndex];

            currentRow.Cells[0].Value = row["RecieveId"];
            currentRow.Cells[1].Value = row["VendorID"];
            currentRow.Cells[2].Value = row["VendorName"];
            currentRow.Cells[3].Value = row["ProductId"];
            currentRow.Cells[4].Value = row["ProductName"];
            currentRow.Cells[5].Value = row["Weight"];
            currentRow.Cells[6].Value = row["Count"];
            currentRow.Cells[7].Value = row["BoxWeight"];
            currentRow.Cells[8].Value = row["BoxesWeight"];
            currentRow.Cells[9].Value = row["PalleteWeight"];
            currentRow.Cells[10].Value = row["DiscountWeight"];
            currentRow.Cells[11].Value = row["TotalDiscount"];
            currentRow.Cells[12].Value = row["NetWeight"];
            currentRow.Cells[13].Value = row["UserName"];
            currentRow.Cells[14].Value = Convert.ToDateTime(row["RecieveDate"]).ToString("dd MMMM");
            currentRow.Cells[15].Value = Convert.ToDateTime(row["RecieveDate"]).ToString("hh:mm:ss tt");
        }
        private void Changed(object sender, EventArgs e)
        {
            try
            {
                if (cmbVendor.Text == "" || cmbVendor.Text == string.Empty)
                {
                    cmbVendor.SelectedIndex = -1;
                    Search();
                    return;
                }
                if (cmbProduct.Text == "" || cmbProduct.Text == string.Empty)
                {
                    cmbProduct.SelectedIndex = -1;
                    Search();
                    return;
                }
                Search();
            }
            catch { return; }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (dgvOperation.Rows.Count == 0)
            {
                MyBox.Show("يجب أختيار البيانات المراد استخراجها", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ExportToExcel.Excel(dgvOperation);
        }
    }
}
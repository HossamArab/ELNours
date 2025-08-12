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
    public partial class frmAllProcess : DevExpress.XtraEditors.XtraForm
    {
        DatabaseConnection con;
        private readonly Fills fills = new Fills();
        public frmAllProcess()
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
            fills.fillComboBox(cmbProduct, "Product_tbl", "Id", "Name");
            cmbProduct.SelectedIndex = -1;
            fills.fillComboBox(cmbVendor, "Vendor_tbl", "Id", "Name");
            cmbVendor.SelectedIndex = -1;
        }
        private void Search()
        {
            string condition = "AND ";
            if (cmbProduct.SelectedIndex != -1 && Convert.ToInt32(cmbProduct.SelectedValue) != 0)
            {
                condition += "ProcessDetails_tbl.ProductId = " + Convert.ToInt32(cmbProduct.SelectedValue) + " AND ";
            }
            if (cmbVendor.SelectedIndex != -1 && Convert.ToInt32(cmbVendor.SelectedValue) != 0)
            {
                condition += "ProcessDetails_tbl.VendorId = " + Convert.ToInt32(cmbVendor.SelectedValue) + " AND ";
            }
            // إزالة الكلمة الأخيرة "AND"
            if (!string.IsNullOrEmpty(condition))
            {
                condition = condition.TrimEnd(" AND".ToCharArray());
            }

            GetData(condition);

        }

        // استعلام قاعدة البيانات وإضافة الصفوف إلى DataGridView
        private void GetData(string Condition = "")
        {
            try
            {
                //dgvOperation.SuspendLayout();
                dgvOperation.Rows.Clear();
                if (con.Connection.State == ConnectionState.Closed) { con.OpenConnection(); }
                string query = $@"
                                SELECT
                                    Process_tbl.Id AS ProcessId,
                                    ProcessDetails_tbl.RecieveId AS RecieveId,
                                    ProcessDetails_tbl.VendorId AS VendorId,
                                    ProcessDetails_tbl.ProcessDate AS ProcessDate,
                                    ProcessDetails_tbl.Weight AS Weight,
                                    ProcessDetails_tbl.Good AS Good,
                                    ProcessDetails_tbl.Bad AS Bad,
                                    ProcessDetails_tbl.WeightDifferent AS WeightDifferent,
                                    Product_tbl.Id AS ProductID,
                                    Product_tbl.Name AS ProductName,
                                    Vendor_tbl.Name AS VendorName,
                                    User_tbl.FullName AS UserName
                                FROM
                                    Process_tbl
                                INNER JOIN 
                                     ProcessDetails_tbl 
                                ON ProcessDetails_tbl.ProcessId = Process_tbl.Id
                                INNER JOIN Product_tbl ON ProcessDetails_tbl.ProductId = Product_tbl.Id
                                INNER JOIN Vendor_tbl ON Vendor_tbl.Id = ProcessDetails_tbl.VendorId
                                INNER JOIN User_tbl ON User_tbl.Id = ProcessDetails_tbl.UserId
                                WHERE 
                                    Process_tbl.ProcessDate BETWEEN @date1 AND @date2
                                    {Condition}
                                ORDER BY 
                                    Process_tbl.Id";

                using (SqlDataAdapter da = new SqlDataAdapter(query, con.Connection))
                {
                    da.SelectCommand.Parameters.Add("@date1", SqlDbType.DateTime).Value = txtFromDate.Value;
                    da.SelectCommand.Parameters.Add("@date2", SqlDbType.DateTime).Value = txtToDate.Value;
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
            currentRow.Cells[0].Value = row["ProcessId"];
            currentRow.Cells[1].Value = row["RecieveId"];
            currentRow.Cells[2].Value = Convert.ToDateTime(row["ProcessDate"]).ToString("dd MMMM");
            currentRow.Cells[3].Value = Convert.ToDateTime(row["ProcessDate"]).ToString("hh:mm:ss tt");
            currentRow.Cells[4].Value = row["VendorId"];
            currentRow.Cells[5].Value = row["VendorName"];
            currentRow.Cells[6].Value = row["ProductId"];
            currentRow.Cells[7].Value = row["ProductName"];
            currentRow.Cells[8].Value = row["Weight"];
            currentRow.Cells[9].Value = row["Good"];
            currentRow.Cells[10].Value = row["Bad"];
            currentRow.Cells[11].Value = row["WeightDifferent"];
            currentRow.Cells[12].Value = row["UserName"];
        }
        private void Changed(object sender, EventArgs e)
        {
            try
            {
                if (cmbProduct.Text == "" || cmbProduct.Text == string.Empty)
                {
                    cmbProduct.SelectedIndex = -1;
                    Search();
                    return;
                }
                if (cmbVendor.Text == "" || cmbVendor.Text == string.Empty)
                {
                    cmbVendor.SelectedIndex = -1;
                    Search();
                    return;
                }
                Search();
            }
            catch { return; }
        }
        private void btnMakeReceive_Click(object sender, EventArgs e)
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
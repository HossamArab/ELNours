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
        }
        private void Search()
        {
            string condition = "(ProcessDetails_tbl.ProcessDate >= @date1 AND ProcessDetails_tbl.ProcessDate <= @date2) AND ";
            if (cmbProduct.SelectedIndex != -1 && Convert.ToInt32(cmbProduct.SelectedValue) != 0)
            {
                condition += "ProcessDetails_tbl.ProductId = " + Convert.ToInt32(cmbProduct.SelectedValue) + " AND ";
            }
            // إزالة الكلمة الأخيرة "AND"
            if (!string.IsNullOrEmpty(condition))
            {
                condition = condition.TrimEnd(" AND".ToCharArray());
            }

            GetData(condition);

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
                    ProcessDetails_tbl.ProcessId AS ProcessId,
                    ProcessDetails_tbl.ProcessDate AS ProcessDate,
                    ProcessDetails_tbl.ProductId AS ProductId,
                    ProcessDetails_tbl.Weight AS Weight,
                    ProcessDetails_tbl.Good AS Good,
                    ProcessDetails_tbl.Bad AS Bad,
                    ProcessDetails_tbl.WeightDifferent AS WeightDifferent,
                    ProcessDetails_tbl.UserId AS UserId,
                    Product_tbl.Name AS ProductName,
                    User_tbl.FullName AS UserName
                FROM 
                    ProcessDetails_tbl
                INNER JOIN 
                    Product_tbl ON Product_tbl.Id = ProcessDetails_tbl.ProductId
                INNER JOIN 
                    User_tbl ON User_tbl.Id = ProcessDetails_tbl.UserId
                WHERE 
                    " + condition + @"
                ORDER BY 
                    ProcessDetails_tbl.Id"
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
                    lblTotalCount.Text = dgvOperation.Rows.Count.ToString();
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
            currentRow.Cells[1].Value = Convert.ToDateTime(row["ProcessDate"]).ToString("dd MMMM");
            currentRow.Cells[2].Value = Convert.ToDateTime(row["ProcessDate"]).ToString("hh:mm:ss tt");
            currentRow.Cells[3].Value = row["ProductId"];
            currentRow.Cells[4].Value = row["ProductName"];
            currentRow.Cells[5].Value = row["Weight"];
            currentRow.Cells[6].Value = row["Good"];
            currentRow.Cells[7].Value = row["Bad"];
            currentRow.Cells[8].Value = row["WeightDifferent"];
            currentRow.Cells[9].Value = row["UserName"];
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
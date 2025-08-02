using DataBaseOperations;
using DevExpress.Charts.Native;
using DevExpress.Printing.Core.PdfExport.Metafile;
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

namespace ELNour.Frm
{
    public partial class frmProcess : DevExpress.XtraEditors.XtraForm
    {
        DatabaseConnection con;
        private readonly Fills fills = new Fills();
        public frmProcess()
        {
            InitializeComponent();
            con = new DatabaseConnection(Connections.Constr);
            txtFromDate.Value = DateTime.Today;
            txtToDate.Value = DateTime.Today.AddHours(24);
            InitializeData();


        }
        private void InitializeData()
        {
            GetData();
            fills.fillComboBox(cmbProduct, "Product_tbl", "Id", "Name");
            cmbProduct.SelectedIndex = -1;
        }
        private void GetData(string Condition = "")
        {
            try
            {
                dgvOperation.SuspendLayout();
                dgvOperation.Rows.Clear();
                if (con.Connection.State == ConnectionState.Closed) { con.OpenConnection(); }

                string query = $@"
                                SELECT 
                                Product_tbl.Id AS ProductID,
                                Product_tbl.Name AS ProductName,
                                ISNULL(SUM(RecieveDetails_tbl.NetWeight), 0) AS NetWeight
                            FROM 
                                Product_tbl
                            INNER JOIN 
                                (RecieveDetails_tbl 
                                 INNER JOIN Recieve_tbl 
                                     ON RecieveDetails_tbl.RecieveId = Recieve_tbl.Id 
                                     AND Recieve_tbl.Type = 0 
                                     AND Recieve_tbl.RecieveDate BETWEEN @date1 AND @date2)
                                ON Product_tbl.Id = RecieveDetails_tbl.ProductId
                                AND RecieveDetails_tbl.IsProccess = 0
                                {Condition}
                            GROUP BY
                                Product_tbl.Id, Product_tbl.Name
                            ORDER BY 
                                Product_tbl.Id";

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
            catch
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con.Connection.State == ConnectionState.Open) { con.CloseConnection(); }
            }
        }
        private void AddRowToDataGridView(DataRow row)
        {
            int rowIndex = dgvOperation.Rows.Add();
            DataGridViewRow currentRow = dgvOperation.Rows[rowIndex];

            currentRow.Cells[0].Value = row["ProductId"];
            currentRow.Cells[1].Value = row["ProductName"];
            currentRow.Cells[2].Value = row["NetWeight"];
        }
        private void txtFromDate_ValueChanged(object sender, EventArgs e)
        {
            if(txtFromDate.Value > txtToDate.Value)
            {
               MyBox.Show("تاريخ البداية يجب أن يكون أقل من تاريخ النهاية","تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dgvOperation.Rows.Clear();
                return;
            }
            GetData();
        }
        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }
        private void dgvOperation_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
                if (dgvOperation.CurrentCell.ColumnIndex == 3 || dgvOperation.CurrentCell.ColumnIndex == 4)
                {
                    TextBox tb = e.Control as TextBox;
                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                    }
                }
            }
            catch { }
        }
        private void dgvOperation_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
        private void dgvOperation_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dgvOperation.CurrentCell.ColumnIndex == 3 || dgvOperation.CurrentCell.ColumnIndex == 4)
            {
                if (!UserPermission.EditMakeReceive) // allowEdit هي متغير يحدد إذا كان مسموحاً بالتعديل أم لا
                {
                    e.Cancel = true; // إلغاء عملية التعديل
                    MyBox.Show("غير مسموح بالتعديل حالياً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            
        }
        private void dgvOperation_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var currentRow = dgvOperation.CurrentRow;
            if (dgvOperation.CurrentCell.ColumnIndex == 3 || dgvOperation.CurrentCell.ColumnIndex == 4)
            {
                if(Convert.ToDecimal(currentRow.Cells[2].Value) != 0 && Convert.ToDecimal(currentRow.Cells[3].Value) != 0)
                {
                    currentRow.Cells[5].Value = ((Convert.ToDecimal(currentRow.Cells[3].Value) + Convert.ToDecimal(currentRow.Cells[4].Value)) - Convert.ToDecimal(currentRow.Cells[2].Value));
                    currentRow.Cells[6].Value = 1;
                }
                
            }
        }
        private void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbProduct.SelectedIndex == -1)
                {
                    GetData();
                    return;
                }
                
                string Condition = $"And Product_tbl.Id = {cmbProduct.SelectedValue}";
                GetData(Condition);
            }
            catch {
                GetData();
                return;
            }
        }
        private void cmbProduct_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbProduct.Text))
            {
                cmbProduct.SelectedIndex = -1;
                GetData();
                return;
            }
        }
    }
}
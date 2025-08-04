using DataBaseOperations;
using DevExpress.Charts.Native;
using DevExpress.Data.Linq.Helpers;
using DevExpress.DataProcessing.InMemoryDataProcessor;
using DevExpress.PivotGrid.OLAP.SchemaEntities;
using DevExpress.Printing.Core.PdfExport.Metafile;
using DevExpress.XtraEditors;
using ELNour.Classes;
using ELNour.Data;
using ELNour.Services;
using MessageBoxes;
using Microsoft.SqlServer.Management.Dmf;
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
        DatabaseOperation oper;
        IMaxID max = new MaxID();
        private readonly Fills fills = new Fills();
        string condition = "";
        public frmProcess()
        {
            InitializeComponent();
            con = new DatabaseConnection(Connections.Constr);
            oper = new DatabaseOperation(con);
            txtFromDate.Value = DateTime.Today;
            txtToDate.Value = DateTime.Today.AddHours(24);
            InitializeData();
        }
        private void InitializeData()
        {
            
            fills.fillComboBox(cmbProduct, "Product_tbl", "Id", "Name");
            cmbProduct.SelectedIndex = -1;
        }
        private void GetData(string Condition = "")
        {
            try
            {
                //dgvOperation.SuspendLayout();
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
            GetData(condition);
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
                if (!UserPermission.EditProcess) // allowEdit هي متغير يحدد إذا كان مسموحاً بالتعديل أم لا
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
                    condition = "";
                    GetData(condition);
                    return;
                }
                
                condition = $"And Product_tbl.Id = {cmbProduct.SelectedValue}";
                GetData(condition);
            }
            catch {
                condition = "";
                GetData(condition);
                return;
            }
        }
        private void cmbProduct_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbProduct.Text))
            {
                cmbProduct.SelectedIndex = -1;
                condition = "";
                GetData(condition);
                return;
            }
        }
        bool isAllFalse()
        {
            bool allFalse = true;
            foreach (DataGridViewRow row in dgvOperation.Rows)
            {
                if (row.IsNewRow) continue;

                if (Convert.ToBoolean(row.Cells[6].Value))
                {
                    allFalse = false;
                    break;
                }
            }
            return allFalse;
        }
        private void SaveInDataBase()
        {
            if (isAllFalse())
            {
                MyBox.Show($"لا يمكن الحفظ ما لم يحدث تغير", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int processId = (max.MaxIDs("Id", "Process_tbl") + 1);
            con.OpenConnection();
            con.BeginTransaction();

            try
            {
                Dictionary<string, object> ProcessData = new Dictionary<string, object> // بيانات العملية الأساسية
                {
                    {"Id",processId},
                    {"ProcessDate",DateTime.Now },
                    {"UserId",User.UserID },
                };
                oper.InsertWithTransaction("Process_tbl", ProcessData);
                foreach (DataGridViewRow row in dgvOperation.Rows)
                {
                    if(Convert.ToBoolean(row.Cells[6].Value))
                    {
                        Dictionary<string, object> ProcessDetails = new Dictionary<string, object>//تفاصيل  العملية 
                        {
                            {"ProcessId",processId },
                            {"ProductId",Convert.ToInt32(row.Cells[0].Value) },
                            {"Weight",Convert.ToDecimal(row.Cells[2].Value) },
                            {"Good",Convert.ToDecimal(row.Cells[3].Value) },
                            {"Bad",Convert.ToDecimal(row.Cells[4].Value) },
                            {"WeightDifferent",Convert.ToDecimal(row.Cells[5].Value) },
                            {"UserId",User.UserID },
                            {"ProcessDate",DateTime.Now},
                        };
                        oper.InsertWithTransaction("ProcessDetails_tbl", ProcessDetails);
                        Dictionary<string, object> UpdateOperation = new Dictionary<string, object>
                        {
                            {"IsProccess",true },
                        };
                        int productId = Convert.ToInt32(row.Cells[0].Value);
                        DateTime fromDate = txtFromDate.Value;
                        DateTime toDate = txtToDate.Value;
                        string condition = $"RecieveDetails_tbl.ProductId = {productId}" +
                                           $"AND RecieveDetails_tbl.RecieveId IN( " +
                                           $"SELECT r.Id "+
                                           $"FROM Recieve_tbl r " +
                                           $"WHERE r.RecieveDate BETWEEN '{fromDate:yyyy-MM-dd HH:mm:ss}' AND '{toDate:yyyy-MM-dd HH:mm:ss}')";
                        oper.UpdateWithTransaction("RecieveDetails_tbl", UpdateOperation,condition);
                    }
                    
                }
                con.CommitTransaction();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dgvOperation.Rows.Count <= 0)
            {
                MyBox.Show($"لا يمكن الحفظ و الجدول فارغ", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            SaveInDataBase();
            GetData(condition);
        }
    }
}
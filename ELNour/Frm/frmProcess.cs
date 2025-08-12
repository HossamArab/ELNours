using DataBaseOperations;
using ELNour.Classes;
using ELNour.Data;
using ELNour.Services;
using MessageBoxes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
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
            fills.fillComboBox(cmbVendor, "Vendor_tbl", "Id", "Name");
            cmbVendor.SelectedIndex = -1;
            fills.fillComboBox(cmbProduct, "Product_tbl", "Id", "Name");
            cmbProduct.SelectedIndex = -1;
        }
        private decimal GoodWeight(int RecieveId, int ProductId)
        {
            decimal total = 0;
            try
            {
                if (con.Connection.State == ConnectionState.Open)
                    con.CloseConnection();
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = con.Connection;
                Cmd.CommandType = CommandType.Text;
                Cmd.CommandText = ("Select NetWeight From InProcess_tbl Where RecieveId =@RecieveId and ProductId=@ProductId");
                Cmd.Parameters.Clear();
                Cmd.Parameters.Add("@RecieveId", SqlDbType.Int).Value = RecieveId;
                Cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = ProductId;
                con.OpenConnection();
                total = Convert.ToDecimal(Cmd.ExecuteScalar());
                return total;

            }
            catch
            {
                total = 0;
                con.CloseConnection();
                return total;
            }
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
                                    Recieve_tbl.Id AS RecieveId,
                                    Recieve_tbl.VendorId,
                                    Recieve_tbl.RecieveDate,
                                    Recieve_tbl.BoxesCount,
                                    Recieve_tbl.TotalWeight,
                                    Recieve_tbl.TotalDiscount,
                                    Recieve_tbl.TotalNetWeight,
                                    Product_tbl.Id AS ProductID,
                                    Product_tbl.Name AS ProductName,
                                    Vendor_tbl.Name AS VendorName
                                FROM
                                    Recieve_tbl
                                INNER JOIN (
                                    SELECT 
                                        RecieveId,
                                        ProductId,
                                        IsProccess,
                                        ROW_NUMBER() OVER (
                                            PARTITION BY RecieveId, ProductId 
                                            ORDER BY Id ASC 
                                        ) AS rn
                                    FROM RecieveDetails_tbl
                                ) AS RecieveDetails_filtered ON RecieveDetails_filtered.RecieveId = Recieve_tbl.Id
                                                           AND RecieveDetails_filtered.rn = 1 
                                INNER JOIN Product_tbl ON RecieveDetails_filtered.ProductId = Product_tbl.Id
                                INNER JOIN Vendor_tbl ON Vendor_tbl.Id = Recieve_tbl.VendorId
                                WHERE 
                                    Recieve_tbl.Type = 0 
                                    AND Recieve_tbl.RecieveDate BETWEEN @date1 AND @date2
                                    AND RecieveDetails_filtered.IsProccess = 0
                                    {Condition}
                                ORDER BY 
                                    Recieve_tbl.Id";

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
        private void AddRowToDataGridView(DataRow row)
        {
            int rowIndex = dgvOperation.Rows.Add();
            int state = 0;
            DataGridViewRow currentRow = dgvOperation.Rows[rowIndex];
            currentRow.Cells[0].Value = row["RecieveId"];
            currentRow.Cells[1].Value = Convert.ToDateTime(row["RecieveDate"]).ToShortDateString();
            currentRow.Cells[2].Value = Convert.ToDateTime(row["RecieveDate"]).ToShortTimeString();
            currentRow.Cells[3].Value = row["VendorId"];
            currentRow.Cells[4].Value = row["VendorName"];
            currentRow.Cells[5].Value = row["ProductId"];
            currentRow.Cells[6].Value = row["ProductName"];
            currentRow.Cells[7].Value = row["TotalNetWeight"];
            currentRow.Cells[8].Value = GoodWeight(Convert.ToInt32(currentRow.Cells[0].Value), Convert.ToInt32(currentRow.Cells[5].Value));
            currentRow.Cells[10].Value = ((Convert.ToDecimal(currentRow.Cells[8].Value) + Convert.ToDecimal(currentRow.Cells[9].Value)) - Convert.ToDecimal(currentRow.Cells[7].Value));
            if (Convert.ToDecimal(currentRow.Cells[8].Value) > 0)
            {
                state = 1;
            }
            switch (state)
            {
                case 0:
                    currentRow.Cells[11].Value = "لم يتم الانتاج ...";
                    currentRow.Cells[11].Style.BackColor = Color.Maroon;
                    currentRow.Cells[11].Style.ForeColor = Color.White;
                    currentRow.Cells[12].Value = state;
                    break;
                case 1:
                    currentRow.Cells[11].Value = "تحت الانتاج ...";
                    currentRow.Cells[11].Style.BackColor = Color.Gold;
                    currentRow.Cells[11].Style.ForeColor = Color.Black;
                    currentRow.Cells[12].Value = state;
                    break;
                default:
                    break;
            }

        }
        public void Search()
        {
            condition = "AND ";
            if (cmbVendor.SelectedIndex != -1 && Convert.ToInt32(cmbVendor.SelectedValue) != 0)
            {
                condition += "Vendor_tbl.Id = " + Convert.ToInt32(cmbVendor.SelectedValue) + " AND ";
            }

            if (cmbProduct.SelectedIndex != -1 && Convert.ToInt32(cmbProduct.SelectedValue) != 0)
            {
                condition += "Product_tbl.Id = " + Convert.ToInt32(cmbProduct.SelectedValue) + " AND ";
            }
            // إزالة الكلمة الأخيرة "AND"
            if (!string.IsNullOrEmpty(condition))
            {
                condition = condition.TrimEnd(" AND".ToCharArray());
            }

            GetData(condition);
        }
        private void txtFromDate_ValueChanged(object sender, EventArgs e)
        {
            if (txtFromDate.Value > txtToDate.Value)
            {
                MyBox.Show("تاريخ البداية يجب أن يكون أقل من تاريخ النهاية", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (dgvOperation.CurrentCell.ColumnIndex == 8 || dgvOperation.CurrentCell.ColumnIndex == 9)
            {
                if (Convert.ToDecimal(currentRow.Cells[8].Value) != 0 && Convert.ToDecimal(currentRow.Cells[7].Value) != 0)
                {
                    currentRow.Cells[10].Value = ((Convert.ToDecimal(currentRow.Cells[8].Value) + Convert.ToDecimal(currentRow.Cells[9].Value)) - Convert.ToDecimal(currentRow.Cells[7].Value));
                    currentRow.Cells[12].Value = 1;
                }

            }
        }
        private void ChangeValue(object sender, EventArgs e)
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
        bool isAllFalse()
        {
            bool allFalse = true;
            foreach (DataGridViewRow row in dgvOperation.Rows)
            {
                if (row.IsNewRow) continue;

                if (Convert.ToBoolean(row.Cells[12].Value))
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
                    if (Convert.ToBoolean(row.Cells[12].Value))
                    {
                        Dictionary<string, object> ProcessDetails = new Dictionary<string, object>//تفاصيل  العملية 
                        {
                            {"ProcessId",processId },
                            {"RecieveId",Convert.ToInt32(row.Cells[0].Value) },
                            {"VendorId",Convert.ToInt32(row.Cells[3].Value) },
                            {"ProductId",Convert.ToInt32(row.Cells[5].Value) },
                            {"Weight",Convert.ToDecimal(row.Cells[7].Value) },
                            {"Good",Convert.ToDecimal(row.Cells[8].Value) },
                            {"Bad",Convert.ToDecimal(row.Cells[9].Value) },
                            {"WeightDifferent",Convert.ToDecimal(row.Cells[10].Value) },
                            {"UserId",User.UserID },
                            {"ProcessDate",DateTime.Now},
                        };
                        oper.InsertWithTransaction("ProcessDetails_tbl", ProcessDetails);
                        Dictionary<string, object> UpdateOperation = new Dictionary<string, object>
                        {
                            {"IsProccess",true },
                        };
                        string condition = $"RecieveId = {Convert.ToInt32(row.Cells[0].Value)} AND ProductId = {Convert.ToInt32(row.Cells[5].Value)}";
                        oper.UpdateWithTransaction("RecieveDetails_tbl", UpdateOperation, condition);
                        oper.DeleteWithTransaction("InProcess_tbl", $"RecieveId = {Convert.ToInt32(row.Cells[0].Value)} AND ProductId = {Convert.ToInt32(row.Cells[5].Value)} ");
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
        private void SendDataToUpgrade()
        {
            bool isSameFormOpen = Application.OpenForms.OfType<frmMakeProcess>().Any();
            bool isWeightFormOpen = Application.OpenForms.OfType<frmOperation>().Any();
            if (isSameFormOpen)
            {
                MyBox.Show("هذه الصفحة مفتوحة بالفعل يرجي التأكد منها لعدم حدوث خلل خاص بالميزان", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (isWeightFormOpen)
            {
                MyBox.Show("هناك صفحة وزن مفتوحة بالفعل يرجي التأكد منها لعدم حدوث خلل خاص بالميزان", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            frmMakeProcess frm = new frmMakeProcess(this);
            fills.fillComboBox(frm.cmbVendor, "Vendor_tbl", "Id", "Name");
            frm.cmbVendor.SelectedIndex = -1;
            fills.fillComboBox(frm.cmbItems, "Product_tbl", "Id", "Name");
            frm.cmbItems.SelectedIndex = -1;
            DataGridViewRow row = dgvOperation.CurrentRow;
            frm.txtRecieveId.Text = row.Cells[0].Value.ToString();
            frm.cmbVendor.SelectedValue = Convert.ToInt32(row.Cells[3].Value);
            frm.cmbItems.SelectedValue = Convert.ToInt32(row.Cells[5].Value);
            frm.txtUser.Text = User.FullName;
            frm.Show();
        }
        private void dgvOperation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 13)
            {
                SendDataToUpgrade();
            }
        }
    }
}
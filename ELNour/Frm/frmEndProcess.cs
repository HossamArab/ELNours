using DataBaseOperations;
using DevExpress.Charts.Native;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;
using ELNour.Classes;
using ELNour.Data;
using ELNour.Report;
using ELNour.Services;
using MessageBoxes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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
            dgvSale.Rows.Clear();
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
        private void SaveProcess()
        {
            txtProcessId.Text = (max.MaxIDs("Id", "Process_tbl") + 1).ToString();
            if (con.Connection.State == ConnectionState.Closed)
            {
                con.OpenConnection();
            }
            
            con.BeginTransaction();

            try
            {
                Dictionary<string, object> ProcessData = new Dictionary<string, object> // بيانات العملية الأساسية
                {
                    {"Id",Convert.ToInt32(txtProcessId.Text) },
                    {"ProcessDate",DateTime.Now },
                    {"UserId",User.UserID },
                    {"VendorId",Convert.ToInt32(cmbVendor.SelectedValue) },
                    {"Count",int.Parse(lblCount.Text) },
                    {"RecieveId",Convert.ToInt32(txtRecieveId.Text) },
                    {"BoxesCount",int.Parse(lblBoxCount.Text) },
                    {"TotalWeight",decimal.Parse(lblTotalWeight.Text) },
                    {"TotalDiscount",decimal.Parse(lblDiscountWeight.Text) },
                    {"TotalNetWeight", decimal.Parse(lblTotalNetWeight.Text) },
                    {"TotalRecieveWeight",decimal.Parse(lblRecieveWeight.Text) },
                    {"TotalBadWeight",decimal.Parse(lblBadWeight.Text) },
                    {"TotalDifferWeight", decimal.Parse(lblDifferentWeight.Text) },
                };
                oper.InsertWithTransaction("Process_tbl", ProcessData);
                foreach (DataGridViewRow row in dgvSale.Rows)
                {
                    Dictionary<string, object> ProcessesDetails = new Dictionary<string, object>//تفاصيل  العملية 
                    {
                        {"ProcessId",Convert.ToInt32(txtProcessId.Text) },
                        {"ProductId",Convert.ToInt32(row.Cells[0].Value) },
                        {"BoxesCount",Convert.ToInt32(row.Cells[1].Value) },
                        {"Weight",Convert.ToDecimal(row.Cells[3].Value) },
                        {"PalleteWeight",Convert.ToDecimal(row.Cells[4].Value) },
                        {"BoxWeight",Convert.ToDecimal(row.Cells[5].Value) },
                        {"BoxesWeight",Convert.ToDecimal(row.Cells[6].Value) },
                        {"DiscountWeight",Convert.ToDecimal(row.Cells[7].Value) },
                        {"TotalDiscount",Convert.ToDecimal(row.Cells[8].Value) },
                        {"NetWeight",Convert.ToDecimal(row.Cells[9].Value) },
                        {"QA",Convert.ToString(row.Cells[10].Value) },
                        {"UserId",Convert.ToInt32(row.Cells[11].Value) },
                        {"Notes",Convert.ToString(row.Cells[12].Value) },
                        {"InProcessId",Convert.ToInt32(row.Cells[14].Value) },
                    };
                    oper.InsertWithTransaction("ProcessesDetails_tbl", ProcessesDetails);
                    Dictionary<string, object> UpdateProcess = new Dictionary<string, object>
                    {
                        {"IsProcessed",true },
                    };
                    oper.UpdateWithTransaction("InProcess_tbl", UpdateProcess, $"Id = {Convert.ToInt32(row.Cells[14].Value)}");
                    Dictionary<string, object> UpdateOperation = new Dictionary<string, object>
                    {
                        {"IsProccess",true },
                    };
                    string condition = $"RecieveId = {Convert.ToInt32(txtRecieveId.Text)} AND ProductId = {Convert.ToInt32(row.Cells[0].Value)}";
                    oper.UpdateWithTransaction("RecieveDetails_tbl", UpdateOperation, condition);
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
        private void PrintProcess()
        {
            try
            {
                SaveProcess();
                rptProcess rpt = new rptProcess();
                DataSet.dsRecieve data = new DataSet.dsRecieve();
                for (int i = 0; i <= dgvSale.Rows.Count - 1; i++)
                {
                    data.DataTable1.Rows.Add();
                    data.DataTable1.Rows[i]["ProductName"] = dgvSale.Rows[i].Cells[2].Value;
                    data.DataTable1.Rows[i]["Weight"] = dgvSale.Rows[i].Cells[3].Value;
                    data.DataTable1.Rows[i]["DiscountWeight"] = dgvSale.Rows[i].Cells[8].Value;
                    data.DataTable1.Rows[i]["NetWeight"] = dgvSale.Rows[i].Cells[9].Value;
                }
                rpt.DataSource = data;
                rpt.lblProcessId.Text = txtProcessId.Text;
                rpt.lblRecieveId.Text = txtRecieveId.Text;
                ImageSource source = new ImageSource(Company.CompanyLogo);
                rpt.picCompany.ImageSource = source;
                if (Printer.PrintCompanyName)
                {
                    rpt.lblCompanyName.Text = Company.CompanyName;
                }
                else
                {
                    rpt.lblCompanyName.Visible = false;
                }
                if (Printer.PrintCompanyDescription)
                {
                    rpt.lblDescription.Text = Company.CompanyDescription;
                }
                else
                {
                    rpt.lblDescription.Visible = false;
                }
                if (Printer.PrintCompanyLogo)
                {
                    rpt.picCompany.Visible = true;
                }
                else
                {
                    rpt.picCompany.Visible = false;
                }
                rpt.lblDate.Text = DateTime.Now.ToString("dd/MM/yyyy  hh:mm:ss  tt");
                rpt.lblVendor.Text = cmbVendor.Text;
                rpt.lblCount.Text = lblCount.Text;
                rpt.lblBoxesCount.Text = lblBoxCount.Text;
                rpt.lblTotalWeight.Text = lblTotalWeight.Text;
                rpt.lblTotalDiscount.Text = lblDiscountWeight.Text;
                rpt.lblNetWeight.Text = lblTotalNetWeight.Text;
                rpt.lblBadWeight.Text = lblBadWeight.Text;
                rpt.lblRecieveWeight.Text = lblRecieveWeight.Text;
                rpt.lblDifferWeight.Text = lblDifferentWeight.Text;
                if (Printer.PreviewBeforePrint)
                {
                    rpt.ShowPreviewDialog();
                }
                else
                {
                    if (Printer.RecipteCopy > 1)
                    {
                        for (int i = 1; i <= Printer.NoCopy; i++)
                        {
                            rpt.Print(Printer.PrinterRecipteName);
                        }
                    }
                    if (Printer.RecipteCopy == 1)
                    {
                        rpt.Print(Printer.PrinterRecipteName);
                    }
                }
            }
            catch { }
        }

        private void btnMakeReceive_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(lblDifferentWeight.Text) > 0)
            {
                return;
            }
                int printType = Printer.PrintAfterSave;
            switch (printType)
            {
                case 0:
                    SaveProcess();
                    break;
                case 1:
                    PrintProcess();
                    break;
                case 2:
                    break;
            }
        }
        private void DeleteRecord(int recordId)
        {
            try
            {
                if (con.Connection.State == ConnectionState.Closed)
                {
                    con.OpenConnection();
                }
                oper.Delete("InProcess_tbl", $"Id = {recordId}");
            }
            catch (Exception ex) {
                MyBox.Show($"خطأ غير متوقع : {Environment.NewLine} {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally { con.CloseConnection(); }
           
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
        private void dgvSale_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
                if (dgvSale.CurrentCell.ColumnIndex == 7)
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
        private void dgvSale_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
        private void dgvSale_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.ColumnIndex == 15)
            {
                //if (!UserPermission.DeleteMakeReceive)
                //{
                //    MyBox.Show($"ليس لديك صلاحية للحذف", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
                if (MyBox.Show($"سيتم حذف العملية هل أنت متأكد من الحذف؟", "سؤال", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                
                DeleteRecord(Convert.ToInt32(dgvSale.CurrentRow.Cells[14].Value));
                int recieveId = Convert.ToInt32(txtRecieveId.Text);
                FillData(recieveId);

            }
        }
        decimal GetDecimalValue(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return 0;

            decimal.TryParse(value.ToString(), out decimal result);
            return result;
        }
        private void dgvSale_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 7)
            {
                dgvSale.CurrentRow.Cells[8].Value = Math.Round(GetDecimalValue(dgvSale.CurrentRow.Cells[4].Value) + GetDecimalValue(dgvSale.CurrentRow.Cells[6].Value) + GetDecimalValue(dgvSale.CurrentRow.Cells[7].Value), 3);
                dgvSale.CurrentRow.Cells[9].Value = Math.Round(GetDecimalValue(dgvSale.CurrentRow.Cells[3].Value) - GetDecimalValue(dgvSale.CurrentRow.Cells[8].Value),3);
                CulcInv();
            }
            int ProccessId = Convert.ToInt32(dgvSale.CurrentRow.Cells[14].Value);
            UpdateProcessWeight(ProccessId);
        }
        private void UpdateProcessWeight(int ProcessId)
        {
            try
            {
                Dictionary<string, object> OperationData = new Dictionary<string, object>
                {

                    {"DiscountWeight",GetDecimalValue(dgvSale.CurrentRow.Cells[7].Value) },
                    {"TotalDiscount",GetDecimalValue(dgvSale.CurrentRow.Cells[8].Value) },
                    {"NetWeight",GetDecimalValue(dgvSale.CurrentRow.Cells[9].Value) },

                };
                if (con.Connection.State == ConnectionState.Closed)
                    con.Connection.Open();
                oper.Update("InProcess_tbl", OperationData, $"Id = {ProcessId}");
            }
            catch (Exception ex) {MyBox.Show($"خطأ في الاتصال بقاعد البيانات :{Environment.NewLine} تفاصيل الخطأ : {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { con.CloseConnection(); }
            
        }
    }
}
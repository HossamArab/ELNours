using DataBaseOperations;
using DevExpress.ClipboardSource.SpreadsheetML;
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
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ELNour.Frm
{
    public partial class frmReceive : DevExpress.XtraEditors.XtraForm
    {
        DatabaseConnection con;
        DatabaseOperation oper;
        GetCompanyData Company = new GetCompanyData();
        GetPrinterData Printer = new GetPrinterData();
        IMaxID max = new MaxID();
        public frmReceive()
        {
            InitializeComponent();
            con = new DatabaseConnection(Connections.Constr);
            oper = new DatabaseOperation(con);
        }
        public void CulcInv()
        {
            decimal TotalCount = 0;
            decimal TotalNetWeight = 0;
            decimal DiscountWeight = 0;
            decimal Weight = 0;
            DataGridViewRowCollection rows = dgvSale.Rows;
            foreach (DataGridViewRow row in rows)
            {
                TotalCount+= Convert.ToDecimal(row.Cells[1].Value);
                Weight += Convert.ToDecimal(row.Cells[3].Value);
                DiscountWeight += Convert.ToDecimal(row.Cells[8].Value);
                TotalNetWeight += Convert.ToDecimal(row.Cells[9].Value);
            }
            lblCount.Text = dgvSale.Rows.Count.ToString();
            lblBoxCount.Text = TotalCount.ToString();
            lblTotalWeight.Text = Weight.ToString();
            lblDiscountWeight.Text = DiscountWeight.ToString();
            lblTotalNetWeight.Text = TotalNetWeight.ToString();
        }
        private void PrintRecieve()
        {
            try
            {
                SaveRecieve();
                rptRecieve rpt = new rptRecieve();
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
                if (Printer.PreviewBeforePrint)
                {
                    rpt.ShowPreviewDialog();
                }
                else
                {
                    if(Printer.RecipteCopy > 1)
                    {
                        for(int i = 1; i <= Printer.NoCopy; i++)
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
        private void SaveRecieve()
        {
            txtRecieveId.Text = (max.MaxIDs("Id", "Recieve_tbl") + 1).ToString();
            con.OpenConnection();
            con.BeginTransaction();

            try
            {
                Dictionary<string, object> RecieveData = new Dictionary<string, object> // بيانات العملية الأساسية
                {
                    {"Id",Convert.ToInt32(txtRecieveId.Text) },
                    {"RecieveDate",DateTime.Now },
                    {"UserId",User.UserID },
                    {"VendorId",Convert.ToInt32(cmbVendor.SelectedValue) },
                    {"Count",int.Parse(lblCount.Text) },
                    {"Type",Convert.ToInt32(cmbType.SelectedIndex) },
                    {"BoxesCount",int.Parse(lblBoxCount.Text) },
                    {"TotalWeight",decimal.Parse(lblTotalWeight.Text) },
                    {"TotalDiscount",decimal.Parse(lblDiscountWeight.Text) },
                    {"TotalNetWeight", decimal.Parse(lblTotalNetWeight.Text) },
                };
                oper.InsertWithTransaction("Recieve_tbl", RecieveData);
                foreach (DataGridViewRow row in dgvSale.Rows)
                {
                    Dictionary<string, object> RecieveDetails = new Dictionary<string, object>//تفاصيل  العملية 
                    {
                        {"RecieveId",Convert.ToInt32(txtRecieveId.Text) },
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
                        {"OperId",Convert.ToInt32(row.Cells[14].Value) },
                        {"RecieveDate",DateTime.Now},
                    };
                    oper.InsertWithTransaction("RecieveDetails_tbl", RecieveDetails);
                    Dictionary<string, object> UpdateOperation = new Dictionary<string, object>
                    {
                        {"IsRecieved",true },
                    };
                    oper.UpdateWithTransaction("Operation_tbl", UpdateOperation,$"Id = {Convert.ToInt32(row.Cells[14].Value)}");
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

        private void btnMakeReceive_Click(object sender, EventArgs e)
        {
            if(cmbType.SelectedIndex == -1)
            {
                MyBox.Show("يجب اختيار نوع السند","تنبيه",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                cmbType.Focus();
                return;
            }
            int printType = Printer.PrintAfterSave;
            switch (printType)
            {
                case 0:
                    SaveRecieve();
                    break;
                case 1:
                    PrintRecieve();
                    break;
                case 2:
                    break;
            }
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTitle.Text = cmbType.Text;
        }

        
    }
}
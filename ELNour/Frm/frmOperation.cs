using DataBaseOperations;
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
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ELNour.Frm
{
    public partial class frmOperation : DevExpress.XtraEditors.XtraForm
    {
        private readonly GetScaleDataFromFiles getScale = new GetScaleDataFromFiles();
        SerialPort port;
        private readonly Fills fills = new Fills();
        private bool ISColsing = false;
        DatabaseConnection con;
        DatabaseOperation oper;
        IMaxID max = new MaxID();
        private readonly int vendorType;
        public frmOperation(int _vendorType = 0)
        {
            InitializeComponent();
            vendorType = _vendorType;
            con = new DatabaseConnection(Connections.Constr);
            oper = new DatabaseOperation(con);
            LoadData();
            LoadScaleData();
        }
        private void Updatelabel2(string weightData) // Change Label lblWeight1 Text when Weight Changed 
        {
            try
            {
                if (lblMainWeight.InvokeRequired)
                {
                    lblMainWeight.Invoke(new Action<string>(Updatelabel2), weightData);
                }
                else
                {
                    lblMainWeight.Text = weightData;
                }
            }
            catch { return; }

        }
        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (ISColsing)
                {
                    port.Close();
                    return;
                }
                string weightData = "";
                if (port.ReadLine() != null)
                {
                    weightData = port.ReadLine();
                }
                else
                {
                    weightData = port.ReadExisting();
                }                
                double Weight = 0;
                try
                {
                    Weight = Convert.ToDouble(weightData.Substring(getScale.StartWeight, getScale.EndWeight - getScale.StartWeight).Replace(" ", ""));
                }
                catch { }
                Updatelabel2(Weight.ToString("0.####"));

            }
            catch
            {


            }
        }
        private void LoadScaleData()
        {
            try
            {
                port = new SerialPort(getScale.Comport, Convert.ToInt32(getScale.BuadRate), Parity.None, 8, StopBits.One);
                if (port == null)
                { return; }
                if (!port.IsOpen)
                {
                    port.Open();

                }
                port.DataReceived += Port_DataReceived;
                

            }
            catch (Exception ex) { MyBox.Show($"خطأ غير متوقع :{Environment.NewLine} تفاصيل الخطأ : {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void LoadData()
        {
            if (tsPlatteWeight.IsOn)
            {
                txtPlateWeight.Text = "22";
            }
            else
            {
                txtPlateWeight.Text = "0";
            }
            fills.fillComboBox(cmbItems, "Product_tbl", "Id", "Name");
            fills.fillComboBox(cmbVendor, "Vendor_tbl", "Id", "Name","Type", vendorType);
            cmbItems.SelectedValue = 0;
            cmbVendor.SelectedValue = 0;
            txtUser.Text = User.FullName;
            txtQA.Text = "";
            txtNotes.Text = "";
            txtCountBoxes.Text = "0";
            txtBoxesWeight.Text = "0";
            txtDiscountWeight.Text = "0";

        }
        private void Refill()
        {
            txtUser.Text = User.FullName;
            txtQA.Text = "";
            txtNotes.Text = "";
            txtCountBoxes.Text = "0";
            txtBoxesWeight.Text = "0";
            txtDiscountWeight.Text = "0";
        }
            
        private void txtpress(object sender, KeyPressEventArgs e) // Make TextBoxes Don't Accept Chars Only Number Or Decimal
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
        private void SumInPanel1() // Sum NetWeight
        {
            txtWeight.Text = lblMainWeight.Text;
            txtBoxesWeight.Text = (Convert.ToDecimal(txtBoxWeight.Text) * Convert.ToDecimal(txtCountBoxes.Text)).ToString();
            txtTotalDiscount.Text = (Convert.ToDecimal(txtBoxesWeight.Text) + Convert.ToDecimal(txtPlateWeight.Text) + Convert.ToDecimal(txtDiscountWeight.Text)).ToString();
            lblNetWeight.Text = (Convert.ToDecimal(txtWeight.Text) - Convert.ToDecimal(txtTotalDiscount.Text)).ToString();
        }
        private void WeightChanged(object sender, EventArgs e) // WeightChanged
        {
            if (txtBoxWeight.Text == "")
            {
                txtBoxWeight.Text = "0";
            }
            if (txtCountBoxes.Text == "")
            {
                txtCountBoxes.Text = "0";
            }
            if (txtPlateWeight.Text == "")
            {
                txtPlateWeight.Text = "0";
            }
            if (txtDiscountWeight.Text == "")
            {
                txtDiscountWeight.Text = "0";
            }
            SumInPanel1();
        }

        private void tsPlatteWeight_Toggled(object sender, EventArgs e)
        {
            if (tsPlatteWeight.IsOn)
            {
                txtPlateWeight.Text = "22";
            }
            else
            {
                txtPlateWeight.Text = "0";
            }
        }
        private void Save()
        {
            if (Convert.ToDouble(lblNetWeight.Text) <= 0)
            {
                MyBox.Show("  الوزن لا يجب أن يكون صفر أو أقل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbItems.SelectedIndex == -1)
            {
                MyBox.Show("  يجب أختيار المنتج", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbVendor.SelectedIndex == -1)
            {
                MyBox.Show("  يجب أختيار المورد", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                int operationId;
                operationId = (max.MaxIDs("Id", "Operation_tbl") + 1);
                Dictionary<string, object> OperationData = new Dictionary<string, object>
                {
                    {"Id",operationId },
                    {"OperationDate",DateTime.Now },
                    {"VendorId",Convert.ToInt32(cmbVendor.SelectedValue) },
                    {"ProductId",Convert.ToInt32(cmbItems.SelectedValue) },
                    {"BoxesCount",int.Parse(txtCountBoxes.Text) },
                    {"PalleteWeight",Convert.ToDecimal(txtPlateWeight.Text) },
                    {"BoxWeight",Convert.ToDecimal(txtBoxWeight.Text) },
                    {"BoxesWeight",Convert.ToDecimal(txtBoxesWeight.Text) },
                    {"QA",txtQA.Text },
                    {"UserId",User.UserID },
                    {"Notes",txtNotes.Text },
                    {"Weight",Convert.ToDecimal(txtWeight.Text) },
                    {"DiscountWeight",Convert.ToDecimal(txtDiscountWeight.Text) },
                    {"TotalDiscount",Convert.ToDecimal(txtTotalDiscount.Text) },
                    {"NetWeight",Convert.ToDecimal(lblNetWeight.Text) },
                };
                if (con.Connection.State == ConnectionState.Closed)
                    con.Connection.Open();
                oper.Insert("Operation_tbl", OperationData);
                MyBox.Show("تم الحفظ بنجاح", "إجراء سليم", MessageBoxButtons.OK, MessageBoxIcon.None);
                Refill();
            }
            catch (SqlException ex) { MyBox.Show($"خطأ في الاتصال بقاعد البيانات :{Environment.NewLine} تفاصيل الخطأ : {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception exe) { MyBox.Show($"خطأ غير متوقع :{Environment.NewLine} تفاصيل الخطأ : {exe.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void frmOperation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3) { btnSave_Click(null, null); }
        }

        private void frmOperation_FormClosing(object sender, FormClosingEventArgs e)
        {
            ISColsing = true;
        }
    }
}
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
            cmbItems.SelectedIndex = -1;
            cmbVendor.SelectedIndex = -1;
            txtUser.Text = User.FullName;
            txtQA.Text = "";
            txtNotes.Text = "";
            txtCountBoxes.Text = "0";
            txtBoxesWeight.Text = "0";
            txtDiscountWeight.Text = "0";

        }
        private void Refill()
        {
            cmbItems.SelectedIndex = -1;
            txtUser.Text = User.FullName;
            txtQA.Text = "";
            txtNotes.Text = "";
            txtCountBoxes.Text = "";
            txtBoxesWeight.Text = "";
            txtDiscountWeight.Text = "";
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
            decimal Weight = 0;
            decimal BoxWeight = 0;
            decimal BoxCount = 0;
            decimal BoxesWeight = 0;
            decimal PlateWeight = 0;
            decimal DiscountWeight = 0;
            decimal TotalDiscount = 0;
            decimal NetWeight = 0;
            if (txtBoxWeight.Text != "")
            {
                BoxWeight = decimal.Parse(txtBoxWeight.Text);
            }
            if (txtCountBoxes.Text != "")
            {
                BoxCount = decimal.Parse(txtCountBoxes.Text);
            }
            if (txtPlateWeight.Text != "")
            {
                PlateWeight = decimal.Parse(txtPlateWeight.Text);
            }
            if (txtDiscountWeight.Text != "")
            {
                DiscountWeight = decimal.Parse(txtDiscountWeight.Text);
            }
            txtWeight.Text = lblMainWeight.Text;
            Weight = Convert.ToDecimal(txtWeight.Text);
            BoxesWeight = BoxWeight * BoxCount;
            txtBoxesWeight.Text = (BoxesWeight).ToString();
            TotalDiscount = (BoxesWeight + PlateWeight + DiscountWeight);
            txtTotalDiscount.Text = TotalDiscount.ToString();
            NetWeight = Weight - TotalDiscount;
            lblNetWeight.Text = NetWeight.ToString();
        }
        private void WeightChanged(object sender, EventArgs e) // WeightChanged
        {
            
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
                    {"BoxesCount",GetDecimalValue(txtCountBoxes.Text) },
                    {"PalleteWeight",GetDecimalValue(txtPlateWeight.Text) },
                    {"BoxWeight",GetDecimalValue(txtBoxWeight.Text) },
                    {"BoxesWeight",GetDecimalValue(txtBoxesWeight.Text) },
                    {"QA",txtQA.Text },
                    {"UserId",User.UserID },
                    {"Notes",txtNotes.Text },
                    {"Weight",GetDecimalValue(txtWeight.Text) },
                    {"DiscountWeight",GetDecimalValue(txtDiscountWeight.Text) },
                    {"TotalDiscount",GetDecimalValue(txtTotalDiscount.Text) },
                    {"NetWeight",GetDecimalValue(lblNetWeight.Text) },
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
        decimal GetDecimalValue(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return 0;

            decimal.TryParse(value.ToString(), out decimal result);
            return result;
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
            if (port != null)
            {
                port.DataReceived -= Port_DataReceived;
                port.Dispose();
            }
        }
    }
}
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

namespace ELNour.Frm
{
    public partial class frmMakeProcess : DevExpress.XtraEditors.XtraForm
    {
        private readonly GetScaleDataFromFiles getScale = new GetScaleDataFromFiles();
        SerialPort port;
        private bool ISColsing = false;
        DatabaseConnection con;
        DatabaseOperation oper;
        IMaxID max = new MaxID();
        frmProcess frmProcess;
        public frmMakeProcess(frmProcess _frmProcess)
        {
            InitializeComponent();
            frmProcess = _frmProcess;
            con = new DatabaseConnection(Connections.Constr);
            oper = new DatabaseOperation(con);
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

                port.Close() ;
            }
        }
        private void LoadScaleData()
        {
            if (tsPlatteWeight.IsOn)
            {
                txtPlateWeight.Text = "22";
            }
            else
            {
                txtPlateWeight.Text = "0";
            }
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
            if (tsCount.IsOn) 
            {
                
                txtWeight.Text = lblMainWeight.Text;
            }
            if (txtWeight.Text != "")
            {
                Weight = Convert.ToDecimal(txtWeight.Text);
            }
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
        private void tsCount_Toggled(object sender, EventArgs e)
        {
            if (!tsCount.IsOn)
            {
                txtWeight.ReadOnly = false;
                ISColsing = true;
            }
            else
            {
                txtWeight.ReadOnly = true;
                ISColsing = false;
                if (!port.IsOpen)
                {
                    port.Open();
                }
            }
            
        }
        decimal GetDecimalValue(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return 0;

            decimal.TryParse(value.ToString(), out decimal result);
            return result;
        }
        private void frmMakeProcess_FormClosing(object sender, FormClosingEventArgs e)
        {
            ISColsing = true ;
            if(port != null)
            {
                port.DataReceived -= Port_DataReceived;
                port.Dispose();
            }
        }
        private void InsertNewWeight()
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
                int processId;
                processId = (max.MaxIDs("Id", "InProcess_tbl") + 1);
                Dictionary<string, object> OperationData = new Dictionary<string, object>
                {
                    {"Id",processId },
                    {"RecieveId",Convert.ToInt32(txtRecieveId.Text) },
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
                oper.Insert("InProcess_tbl", OperationData);
                MyBox.Show("تم الحفظ بنجاح", "إجراء سليم", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            catch (SqlException ex) { MyBox.Show($"خطأ في الاتصال بقاعد البيانات :{Environment.NewLine} تفاصيل الخطأ : {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception exe) { MyBox.Show($"خطأ غير متوقع :{Environment.NewLine} تفاصيل الخطأ : {exe.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }
        private void UpdateWeight()
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
                
                InProcess inProcess = new InProcess(Convert.ToInt32(txtRecieveId.Text), Convert.ToInt32(cmbItems.SelectedValue));
                inProcess.BoxesCount += (int)GetDecimalValue(txtCountBoxes.Text); 
                inProcess.PalleteWeight += GetDecimalValue(txtPlateWeight.Text);
                inProcess.BoxesWeight += GetDecimalValue(txtBoxesWeight.Text);
                inProcess.BoxWeight += GetDecimalValue(txtBoxWeight.Text);
                inProcess.Weight += GetDecimalValue(txtWeight.Text);
                inProcess.DiscountWeight += GetDecimalValue(txtDiscountWeight.Text);
                inProcess.TotalDiscount += GetDecimalValue(txtTotalDiscount.Text);
                inProcess.NetWeight += GetDecimalValue(lblNetWeight.Text);
                if (con.Connection.State == ConnectionState.Closed) { con.OpenConnection(); }
                con.BeginTransaction();
                Dictionary<string, object> InProcessDaata = new Dictionary<string, object>()
                {
                    {"BoxesCount" ,inProcess.BoxesCount},
                    {"PalleteWeight" ,inProcess.PalleteWeight},
                    {"BoxesWeight" ,inProcess.BoxesWeight},
                    {"BoxWeight" ,inProcess.BoxWeight},
                    {"Weight" ,inProcess.Weight},
                    {"DiscountWeight" ,inProcess.DiscountWeight},
                    {"TotalDiscount" ,inProcess.TotalDiscount},
                    {"NetWeight" ,inProcess.NetWeight}
                };
                oper.UpdateWithTransaction("InProcess_tbl", InProcessDaata, $"RecieveId ={txtRecieveId.Text} and ProductId={cmbItems.SelectedValue}");
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
        bool chkData(int recieveId, int productId)
        {
            if (con.Connection.State == ConnectionState.Closed) { con.OpenConnection(); }
            string query = $@"Select * From InProcess_tbl Where RecieveId =@RecieveId and ProductId=@ProductId";
            using (SqlDataAdapter da = new SqlDataAdapter(query, con.Connection))
            {
                da.SelectCommand.Parameters.Add("@RecieveId", SqlDbType.Int).Value = recieveId;
                da.SelectCommand.Parameters.Add("@ProductId", SqlDbType.Int).Value = productId;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                { return true; }
            }
            return false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(chkData(Convert.ToInt32(txtRecieveId.Text),Convert.ToInt32(cmbItems.SelectedValue)))
            {
                UpdateWeight();
                frmProcess.Search();
            }
            else
            {
                InsertNewWeight();
                frmProcess.Search();
            }
        }
    }
}
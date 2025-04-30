using DataBaseOperations;
using ELNour.Classes;
using ELNour.Data;
using ELNour.Services;
using MessageBoxes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;

namespace ELNour.Frm
{
    public partial class frmMainData : DevExpress.XtraEditors.XtraForm
    {
        DatabaseConnection con;
        DatabaseOperation oper;
        SqlCommand cmd;
        bool ISMirror = false;
        bool ISColsing = false;
        private readonly GetCompanyData getCompany = new GetCompanyData();
        private readonly GetScaleDataFromFiles getScale = new GetScaleDataFromFiles();
        private readonly GetPrinterData getPrinter = new GetPrinterData();
        SerialPort port;
        public frmMainData()
        {
            InitializeComponent();
            con = new DatabaseConnection(Connections.Constr);
            oper = new DatabaseOperation(con);
            getData();
        }
        
        #region MainData
        //firstTabMainTab
        private void GetMainData()
        {
            txtName.Text = getCompany.CompanyName;
            txtNameEn.Text = getCompany.CompanyNameEn;
            txtDescription.Text = getCompany.CompanyDescription;
            txtPhone.Text = getCompany.CompanyPhone;
            txtMobile.Text = getCompany.CompanyMobile;
            txtEmail.Text = getCompany.CompanyEmail;
            txtAddress.Text = getCompany.CompanyAddress;
            try
            {
                picLogo.Image = getCompany.CompanyLogo;
            }
            catch { }
        }
        private void SaveMainData()
        {
            try
            {
                Dictionary<string, object> OrgData;
                if (picLogo.Image != null)
                {
                    byte[] logo = null;
                    OrgData = new Dictionary<string, object>
                    {
                    {"Id",1 },
                    {"OrgName",txtName.Text },
                    {"OrgNameEn",txtNameEn.Text },
                    {"OrgDescription",txtDescription.Text },
                    {"OrgPhone",txtPhone.Text },
                    {"OrgMobile",txtMobile.Text },
                    {"OrgEmail",txtEmail.Text },
                    {"OrgAddress",txtAddress.Text },
                    {"LogoName",logo.PicturetoArray(picLogo) },
                    };
                }
                else
                {
                    OrgData = new Dictionary<string, object>
                    {
                    {"Id",1 },
                    {"OrgName",txtName.Text },
                    {"OrgNameEn",txtNameEn.Text },
                    {"OrgDescription",txtDescription.Text },
                    {"OrgPhone",txtPhone.Text },
                    {"OrgMobile",txtMobile.Text },
                    {"OrgEmail",txtEmail.Text },
                    {"OrgAddress",txtAddress.Text },
                    {"LogoName",null },
                    };
                }
                
                if (con.Connection.State == ConnectionState.Closed)
                    con.Connection.Open();
                oper.Insert("Organization_tbl", OrgData);
            //MyBox.Show("تم الحفظ بنجاح", "إجراء سليم", MessageBoxButtons.OK, MessageBoxIcon.None);
        }
            catch (SqlException ex) { MyBox.Show($"خطأ في الاتصال بقاعد البيانات :{Environment.NewLine} تفاصيل الخطأ : {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception exe) { MyBox.Show($"خطأ غير متوقع :{Environment.NewLine} تفاصيل الخطأ : {exe.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void EditMainData()
        {
            try
            {


                Dictionary<string, object> OrgData;
                if (picLogo.Image != null)
                {
                    byte[] logo = null;
                    OrgData = new Dictionary<string, object>
                    {
                    {"Id",1 },
                    {"OrgName",txtName.Text },
                    {"OrgNameEn",txtNameEn.Text },
                    {"OrgDescription",txtDescription.Text },
                    {"OrgPhone",txtPhone.Text },
                    {"OrgMobile",txtMobile.Text },
                    {"OrgEmail",txtEmail.Text },
                    {"OrgAddress",txtAddress.Text },
                    {"LogoName",logo.PicturetoArray(picLogo) },
                    };
                }
                else
                {
                    OrgData = new Dictionary<string, object>
                    {
                    {"Id",1 },
                    {"OrgName",txtName.Text },
                    {"OrgNameEn",txtNameEn.Text },
                    {"OrgDescription",txtDescription.Text },
                    {"OrgPhone",txtPhone.Text },
                    {"OrgMobile",txtMobile.Text },
                    {"OrgEmail",txtEmail.Text },
                    {"OrgAddress",txtAddress.Text },
                    {"LogoName",Convert.DBNull },
                    };
                }
                if (con.Connection.State == ConnectionState.Closed)
                    con.Connection.Open();
                oper.Update("Organization_tbl", OrgData, $"Id = 1 ");
                MyBox.Show("تم الحفظ بنجاح", "إجراء سليم", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            catch (SqlException ex) { MyBox.Show($"خطأ في الاتصال بقاعد البيانات :{Environment.NewLine} تفاصيل الخطأ : {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception exe) { MyBox.Show($"خطأ غير متوقع :{Environment.NewLine} تفاصيل الخطأ : {exe.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void SaveOrEditMainData()
        {
            try
            {

                if (con.Connection.State == ConnectionState.Closed)
                    con.Connection.Open();

                using (cmd = new SqlCommand("SELECT Id FROM Organization_tbl WHERE Id = 1", con.Connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            EditMainData();
                        }
                        else
                        {
                            SaveMainData();
                        }
                    }
                }
            }
            catch (SqlException ex) { MyBox.Show($"خطأ في الاتصال بقاعد البيانات :{Environment.NewLine} تفاصيل الخطأ : {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception exe) { MyBox.Show($"خطأ غير متوقع :{Environment.NewLine} تفاصيل الخطأ : {exe.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void btnGetImage_Click(object sender, EventArgs e)
        {
            picLogo.ChoosePicture();
        }
        #endregion
        #region ScaleData
        private void GetScaleDataFromFile()
        {
            string[] portNames = SerialPort.GetPortNames();
            cmbCom.Properties.Items.AddRange(portNames);
            cmbCom.Text = getScale.Comport;
            cmbBuadRate.Text = getScale.BuadRate;
            trackStart.Value = getScale.StartWeight;
            trackEnd.Value = getScale.EndWeight;
        }
        private void ConnectSerialScale()
        {
            if (cmbCom.Text == "" || cmbBuadRate.Text == "")
            {
                return;
            }
            try
            {
                port = new SerialPort(cmbCom.Text, Convert.ToInt32(cmbBuadRate.Text), Parity.None, 8, StopBits.One);
                if (port == null)
                { return; }
                if (!port.IsOpen)
                {
                    port.Open();

                }
                port.DataReceived += Port_DataReceived;
                btnOpenConnect.Enabled = false;
                btnDisconnectPort.Enabled = true;

            }
            catch (Exception ex) { MyBox.Show($"خطأ غير متوقع :{Environment.NewLine} تفاصيل الخطأ : {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void DisConnectSerialScale()
        {
            port.DataReceived -= Port_DataReceived;
            port.Close();
            btnOpenConnect.Enabled = true;
            btnDisconnectPort.Enabled = false;
        }
        private void SavePortsForPc()
        {
            string Ports;
            string BuadRate;
            string WeightRange;
            string FileText;
            Ports = cmbCom.Text + "|";
            BuadRate = cmbBuadRate.Text + "|";
            WeightRange = trackStart.Value.ToString() + "|" + trackEnd.Value.ToString() + "|" + ISMirror.ToString();
            FileText = Ports + BuadRate + WeightRange;
            File.WriteAllText("" + System.Windows.Forms.Application.StartupPath + "\\ScaleSettings.txt", FileText, System.Text.Encoding.UTF8);
            //MyBox.Show("تم الحفظ بنجاح", "إجراء سليم", MessageBoxButtons.OK, MessageBoxIcon.None);
        }
        private void Updatelabel1(string weightData) // Change Label lblWeight1 Text when Weight Changed 
        {
            try
            {
                if (txtWeight.InvokeRequired)
                {
                    txtWeight.Invoke(new Action<string>(Updatelabel1), weightData);
                    txtWeight.Select(trackStart.Value, trackEnd.Value - trackStart.Value);
                }
                else
                {
                    txtWeight.Text = weightData;
                    txtWeight.Select(trackStart.Value, trackEnd.Value - trackStart.Value);
                }
            }
            catch { return; }

        }
        private void Updatelabel2(string weightData) // Change Label lblWeight1 Text when Weight Changed 
        {
            try
            {
                if (lblTrueWeight.InvokeRequired)
                {
                    lblTrueWeight.Invoke(new Action<string>(Updatelabel2), weightData);
                }
                else
                {
                    lblTrueWeight.Text = weightData;
                }
            }
            catch { return; }

        }
        private void trackStart_EditValueChanged(object sender, EventArgs e)
        {
            if (trackStart.Value > trackEnd.Value)
            {
                return;
            }
            txtWeight.Focus();
            txtWeight.Select(trackStart.Value, trackEnd.Value - trackStart.Value);
        }
        private void trackStart_ValueChanged(object sender, EventArgs e)
        {
            if (trackStart.Value > trackEnd.Value)
            {
                return;
            }
            txtWeight.Focus();
            txtWeight.Select(trackStart.Value, trackEnd.Value - trackStart.Value);
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
                //txtWeight.Text = weightData;
                if (trackStart.Value > trackEnd.Value)
                {

                }
                else
                {
                    txtWeight.Focus();
                    txtWeight.Select(trackStart.Value, trackEnd.Value - trackStart.Value);
                }
                if (ISMirror)
                {
                    try
                    {
                        string firstWeight = "";
                        string lastWeight = "";
                        double Weightdouble = 0;
                        firstWeight = weightData.Substring(trackStart.Value, trackEnd.Value - trackStart.Value).Replace(" ", "");
                        char[] chararray = firstWeight.ToCharArray();
                        Array.Reverse(chararray);
                        lastWeight = new string(chararray);
                        Weightdouble = Convert.ToDouble(lastWeight);
                        Updatelabel1(weightData);
                        Updatelabel2(Weightdouble.ToString());
                    }
                    catch(Exception ex) { MyBox.Show($"خطأ غير متوقع :{Environment.NewLine} تفاصيل الخطأ : {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    
                }
                else
                {
                    Updatelabel1(weightData);
                    double Weight = 0;
                    try
                    {
                        Weight = Convert.ToDouble(weightData.Substring(trackStart.Value, trackEnd.Value - trackStart.Value).Replace(" ", ""));
                    }
                    catch { }
                    Updatelabel2(Weight.ToString("0.####"));
                }
                
            }
            catch
            {


            }
        }
        private void btnDisconnectPort_Click(object sender, EventArgs e)
        {
            DisConnectSerialScale();
        }
        private void btnOpenConnect_Click(object sender, EventArgs e)
        {
            ConnectSerialScale();
        }
        private void btnMirrorWeight_Click(object sender, EventArgs e)
        {
            ISMirror = true;
        }
        #endregion
        #region ServerData
        private void GetServerData()
        {
            txtServerName.Text = Server.ServerName;
            txtUser.Text = Server.UserName;
            txtPassword.Text = Server.Password;
            
        }
        private void SaveServerForPc()
        {
            string Server;
            string User;
            string Pass;
            string FileText;
            Server = txtServerName.Text + "|";
            User = txtUser.Text + "|";
            Pass = txtPassword.Text + "|";
            FileText = Server + User + Pass;
            File.WriteAllText("" + System.Windows.Forms.Application.StartupPath + "\\ServerSettings.txt", FileText, System.Text.Encoding.UTF8);
            //MyBox.Show("تم الحفظ بنجاح", "إجراء سليم", MessageBoxButtons.OK, MessageBoxIcon.None);
        }
        #endregion
        #region PrinterData
        private void GetPrinterData()
        {
            PrintDialog a = new PrintDialog();

            foreach (string ar in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cmbPrinter.Properties.Items.Add(ar);
                cmbPrinter2.Properties.Items.Add(ar);
            }
            cmbPrinter.Text = getPrinter.PrinterA4Name;
            cmbPrinter2.Text = getPrinter.PrinterRecipteName;
            seCopyNo.Value = getPrinter.NoCopy;
            seRecepiteNo.Value = getPrinter.RecipteCopy;
            chkPrintCompanyName.Checked = getPrinter.PrintCompanyName;
            chkDescription.Checked = getPrinter.PrintCompanyDescription;
            chkLogoCompany.Checked = getPrinter.PrintCompanyLogo;
            chkPrintPreview.Checked = getPrinter.PreviewBeforePrint;
            rgPrinter.SelectedIndex = getPrinter.PrintAfterSave;
        }
        private void SavePrinterData()
        {
            string PrinterData = cmbPrinter.Text + "|" + cmbPrinter2.Text + "|" + seCopyNo.Value + "|" + seRecepiteNo.Value + "|";
            string PrinterSetting = chkPrintCompanyName.Checked.ToString() + "|" + chkLogoCompany.Checked.ToString() + "|" + chkDescription.Checked.ToString() + "|" + chkPrintPreview.Checked.ToString() + "|" + rgPrinter.SelectedIndex.ToString() + "|";
            File.WriteAllText("" + Application.StartupPath + "\\PrinterSettings.txt", PrinterData + Environment.NewLine + PrinterSetting, Encoding.UTF8);
        }
        #endregion
        private void getData()
        {
            GetScaleDataFromFile();
            GetServerData();
            GetMainData();
            GetPrinterData();
        }
        private void saveData()
        {
            SaveOrEditMainData();
            SavePrinterData();
            SavePortsForPc();
            SaveServerForPc();
            MyBox.Show("تم الحفظ بنجاح", "إجراء سليم", MessageBoxButtons.OK, MessageBoxIcon.None);
        }
        private void frmMainData_FormClosing(object sender, FormClosingEventArgs e)
        {
            ISColsing = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveData();
        }
    }
}
using DevExpress.XtraEditors;
using ELNour.Data;
using MessageBoxes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ELNour.Admin
{
    public partial class frmActivate : DevExpress.XtraEditors.XtraForm
    {
        public frmActivate()
        {
            InitializeComponent();
            GetDataForActivate();
        }
        private void GetDataForActivate()
        {
            Activation.secu();
            txtDiskSerial.Text = Activation.diskSN;
            txtID.Text = Activation.md5ha(Activation.Encrypt(txtDiskSerial.Text.Trim(), "Ceaser11@H2e.Com"));
            if (!Activation.chkActivate())
            {
                if (Activation.chkTrial())
                {
                    txtLincesKey.ReadOnly = false;
                    btnTrial.Enabled = false;
                    btnActivate.Enabled = true;
                    txtLincesKey.Text = "";
                    txtLincesKey.Select();

                }
                else
                {
                    txtLincesKey.ReadOnly = false;
                    btnTrial.Enabled = true;
                    btnActivate.Enabled = true;
                    txtLincesKey.Text = "";
                    txtLincesKey.Select();

                }

            }
            else
            {
                txtLincesKey.Text = Activation.GetKeyFromReg();
                txtLincesKey.ReadOnly = true;
                btnTrial.Enabled = false;
                btnActivate.Enabled = false;
            }


        }
        private void SaveActivate()
        {
            if (txtLincesKey.Text.Trim() == txtID.Text.Trim())
            {
                RegistryKey ActiveKey;
                ActiveKey = Registry.LocalMachine.OpenSubKey("SOFTWARE", true);
                ActiveKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\ElNourSystem", true);
                if (ActiveKey == null)
                {
                    ActiveKey = Registry.LocalMachine.OpenSubKey("SOFTWARE", true);
                    ActiveKey.CreateSubKey("ElNourSystem", RegistryKeyPermissionCheck.ReadWriteSubTree);
                    ActiveKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\ElNourSystem", true);
                    ActiveKey.SetValue("Key_Sales", txtLincesKey.Text);
                    ActiveKey.Close();
                    MyBox.Show("تم تفعيل البرنامج بنجاح مرحبا بكم", "التفعيل", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    this.Close();
                }
                else
                {
                    ActiveKey = Registry.LocalMachine.OpenSubKey("SOFTWARE", true);
                    ActiveKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\ElNourSystem", true);
                    ActiveKey.SetValue("Key_Sales", txtLincesKey.Text);
                    ActiveKey.Close();
                    MyBox.Show("تم تفعيل البرنامج بنجاح مرحبا بكم", "التفعيل", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    this.Close();
                }
            }
            else
            {
                MyBox.Show("رقم التفعيل الذي أدخلته غير صحيح برجاء الرجوع لقسم المبيعات " + Environment.NewLine + " هاتف رقم :  00201555311595", "التفعيل", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
        }
        private void SetActivateFor15Days()
        {
            RegistryKey ActiveKey;
            ActiveKey = Registry.LocalMachine.OpenSubKey("SOFTWARE", true);
            ActiveKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\ElNourSystem", true);
            if (ActiveKey == null)
            {
                ActiveKey = Registry.LocalMachine.OpenSubKey("SOFTWARE", true);
                ActiveKey.CreateSubKey("EagleSalesSystem", RegistryKeyPermissionCheck.ReadWriteSubTree);
                ActiveKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\ElNourSystem", true);
                ActiveKey.SetValue("StartDay", DateTime.Today);
                ActiveKey.SetValue("EndDay", DateTime.Today.AddDays(15));
                ActiveKey.Close();
                MyBox.Show("تم تفعيل البرنامج بنجاح لمدة 15 يوم مرحبا بكم", "التفعيل", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                this.Close();
            }
            else
            {
                ActiveKey = Registry.LocalMachine.OpenSubKey("SOFTWARE", true);
                ActiveKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\ElNourSystem", true);
                ActiveKey.SetValue("StartDay", DateTime.Today);
                ActiveKey.SetValue("EndDay", DateTime.Today.AddDays(15));
                ActiveKey.Close();
                MyBox.Show("تم تفعيل البرنامج بنجاح لمدة 15 يوم مرحبا بكم", "التفعيل", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                this.Close();
            }

        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTrial_Click(object sender, EventArgs e)
        {
            SetActivateFor15Days();
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            SaveActivate();
        }
    }
}
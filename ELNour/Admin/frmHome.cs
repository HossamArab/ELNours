using DevExpress.XtraWaitForm;
using ELNour.Admin;
using ELNour.Classes;
using ELNour.Data;
using ELNour.Frm;
using MessageBoxes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ELNour
{
    public partial class frmHome : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly GetServerData _serverData = new GetServerData();
        public frmHome()
        {
            InitializeComponent();
            _serverData.getServerData();
            GetActivate();
            SetPermission();
        }            
        private void btnMainData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new frmMainData().Show();
        }

        private void btnUsers_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new frmUser().Show();
        }

        private void btnVendors_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new frmVendors().Show();
        }

        private void btnProducts_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new frmProduct().Show();
        }

        private void btnOperations_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new frmOperation().Show();
        }

        private void btnBackUp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Server.Backup();
        }

        private void btnMakeReceive_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new frmMakeRecieve().Show();
        }

        private void btnCustomer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new frmCustomer().Show();
        }

        private void btnOutOperation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new frmOperation(1).Show();
        }

        private void btnAllOperation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new frmAllOperation().Show();
        }

        private void btnManageRecieve_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new frmManageRecieve().Show();
        }

        private void btnManageSupply_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new frmManageSupply().Show();
        }

        private void btnRestoreBackUp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Server.RestoreBackup();
        }
        private void SetPermission()
        {
            btnMainData.Enabled = UserPermission.Settings;
            btnUsers.Enabled = UserPermission.Users;
            btnVendors.Enabled = UserPermission.Vendor;
            btnProducts.Enabled = UserPermission.Product;
            btnOperations.Enabled = UserPermission.Operation;
            btnBackUp.Enabled = UserPermission.TakeBakeUp;
            btnRestoreBackUp.Enabled = UserPermission.RestoreBackUp;
            btnMakeReceive.Enabled = UserPermission.MakeReceive;
            btnCustomer.Enabled = UserPermission.Customer;
            btnOutOperation.Enabled = UserPermission.OutOperation;
            btnAllOperation.Enabled = UserPermission.AllOperations;
            btnManageRecieve.Enabled = UserPermission.ManageRecieve;
            btnManageSupply.Enabled = UserPermission.ManageSupply;
            btnProcess.Enabled = UserPermission.Process;
            btnAllProcess.Enabled = UserPermission.AllProcess;
        }

        private void btnLogin_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new frmLogIn().ShowDialog();
            SetPermission();
            btnLogin.Enabled = false;
            btnLogOut.Enabled = true;
        }

        private void btnLogOut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            User.ResetStaticPropertiesToDefault<UserPermission>();
            SetPermission();
            btnLogin.Enabled = true;
            btnLogOut.Enabled = false;
        }
        private void GetActivate()
        {

            if (!Activation.chkActivate())
            {
                if (Activation.chkTrial())
                {
                    MyBox.Show("هذا البرنامج يحتاج إلى تفعيل وهذه نسخة تجريبية", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnLogin.Enabled = true;
                    btnLogOut.Enabled = false;
                    btnActivate.Enabled = true;
                }
                else
                {
                    btnLogin.Enabled = false;
                    btnLogOut.Enabled = false;
                    btnActivate.Enabled = true;
                    MyBox.Show("هذا البرنامج يحتاج إلى تفعيل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    frmActivate frm = new frmActivate();
                    frm.ShowDialog();
                    GetActivate();
                }
            }
            else
            {
                btnLogin.Enabled = true;
                btnLogOut.Enabled = false;
                btnActivate.Enabled = false;
            }
            
        }

        private void btnActivate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmActivate frm = new frmActivate();
            frm.ShowDialog();
            GetActivate();
        }

        private void btnNewYear_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new frmNewYear().Show();
        }

        private void btnImportProduct_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new frmImportProduct().Show();
        }

        private void btnImportImporter_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new frmImportImprter().Show();
        }

        private void btnProcess_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new frmProcess().Show();
        }

        private void btnAllProcess_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new frmAllProcess().Show();
        }
    }
}

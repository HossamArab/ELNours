using DataBaseOperations;
using DevExpress.XtraEditors;
using ELNour.Classes;
using MessageBoxes;
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
using System.Xml.Linq;

namespace ELNour.Admin
{
    public partial class frmImportProduct : DevExpress.XtraEditors.XtraForm
    {
        DatabaseConnection con;
        DatabaseOperation oper;
        SqlCommand cmd;
        public frmImportProduct()
        {
            InitializeComponent();
            con = new DatabaseConnection(Connections.Constr);
            oper = new DatabaseOperation(con);
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xlsx;*.xlsm";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ImportFromExcel.Excel(openFileDialog.FileName, dgvProduct);
            }
        }
        private async Task Save()
        {
            await SaveDataAsync().ConfigureAwait(false);
        }
        private async Task SaveDataAsync()
        {
            try
            {
                // فحص حالة الاتصال بشكل غير متزامن إذا أمكن
                if (con.Connection.State == ConnectionState.Closed)
                    await con.Connection.OpenAsync().ConfigureAwait(false);

                // استخدام Task.Run لتنفيذ العمليات الثقيلة في خلفية
                await Task.Run(async () =>
                {
                    foreach (DataGridViewRow row in dgvProduct.Rows)
                    {
                        if (row.IsNewRow) continue;

                        int Id = Convert.ToInt32(row.Cells[0].Value);
                        string Name = row.Cells[1].Value?.ToString() ?? string.Empty;

                        Dictionary<string, object> ProductData = new Dictionary<string, object>
                {
                    {"Id", Id},
                    {"Name", Name},
                };

                        if (con.Connection.State == ConnectionState.Closed)
                            await con.Connection.OpenAsync().ConfigureAwait(false);

                        // افتراض أن oper.Insert لديه نسخة غير متزامنة
                        await Task.Run(() => oper.Insert("Product_tbl", ProductData)).ConfigureAwait(false);
                    }
                }).ConfigureAwait(false);

                // استخدام BeginInvoke لعرض الرسالة في thread واجهة المستخدم
                if (dgvProduct.InvokeRequired)
                {
                    dgvProduct.BeginInvoke(new Action(() =>
                        MyBox.Show("تم حفظ البيانات بنجاح", "حفظ", MessageBoxButtons.OK, MessageBoxIcon.None)));
                }
                else
                {
                    MyBox.Show("تم حفظ البيانات بنجاح", "حفظ", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
            }
            catch (SqlException ex)
            {
                ShowErrorMessage($"خطأ في الاتصال بقاعد البيانات :{Environment.NewLine} تفاصيل الخطأ : {ex.Message}");
            }
            catch (Exception exe)
            {
                ShowErrorMessage($"خطأ غير متوقع :{Environment.NewLine} تفاصيل الخطأ : {exe.Message}");
            }
        }

        // دالة مساعدة لعرض رسائل الخطأ بطريقة آمنة
        private void ShowErrorMessage(string message)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                    MyBox.Show(message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)));
            }
            else
            {
                MyBox.Show(message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnNewProduct_Click(object sender, EventArgs e)
        {
            try
            {
                await Save();

            }
            catch { }
        }
    }
}
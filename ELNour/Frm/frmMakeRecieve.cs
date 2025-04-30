using DataBaseOperations;
using DevExpress.XtraEditors;
using ELNour.Classes;
using ELNour.Data;
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

namespace ELNour.Frm
{
    public partial class frmMakeRecieve : DevExpress.XtraEditors.XtraForm
    {
        DatabaseConnection con;
        private readonly Fills fills = new Fills();
        public frmMakeRecieve()
        {
            InitializeComponent();
            con = new DatabaseConnection(Connections.Constr);
            LoadData();
        }
        private void LoadData()
        {
            txtFromDate.Value = DateTime.Today;
            txtToDate.Value = DateTime.Today.AddHours(24);
            Search();
            fills.fillComboBox(cmbVendor, "Vendor_tbl", "Id", "Name");
            fills.fillComboBox(cmbProduct, "Product_tbl", "Id", "Name");
            cmbVendor.SelectedIndex = -1;
            cmbProduct.SelectedIndex = -1;
        }
        public void Calc_Inv()
        {
            decimal totalPrice = 0;
            decimal totalCount = 0;
            decimal totalNetWeight = 0;

            foreach (DataGridViewRow row in dgvOperation.Rows)
            {
                if (row.Cells[7].Value != null && row.Cells[11].Value != null && row.Cells[12].Value != null)
                {
                    totalPrice += Convert.ToDecimal(row.Cells[7].Value);
                    totalCount += Convert.ToDecimal(row.Cells[11].Value);
                    totalNetWeight += Convert.ToDecimal(row.Cells[12].Value);
                }
            }

            lblTotalPrice.Text = totalPrice.ToString();
            lblTotalCount.Text = totalCount.ToString();
            lblTotalWeight.Text = totalNetWeight.ToString();
        }

        // جلب البيانات من قاعدة البيانات
        private void Search()
        {
            string condition = "(Operation_tbl.OperationDate >= @date1 AND Operation_tbl.OperationDate <= @date2) AND ";

            if (cmbVendor.SelectedIndex != -1 && Convert.ToInt32(cmbVendor.SelectedValue) != 0)
            {
                condition += "Operation_tbl.VendorId = " + Convert.ToInt32(cmbVendor.SelectedValue) + " AND ";
            }

            if (cmbProduct.SelectedIndex != -1 && Convert.ToInt32(cmbProduct.SelectedValue) != 0)
            {
                condition += "Operation_tbl.ProductId = " + Convert.ToInt32(cmbProduct.SelectedValue) + " AND ";
            }
            // إزالة الكلمة الأخيرة "AND"
            if (!string.IsNullOrEmpty(condition))
            {
                condition = condition.TrimEnd(" AND".ToCharArray());
            }

            GetData(condition);
            Calc_Inv();
        }

        // استعلام قاعدة البيانات وإضافة الصفوف إلى DataGridView
        private void GetData(string condition)
        {
            try
            {
                dgvOperation.SuspendLayout();
                dgvOperation.Rows.Clear();
                if (con.Connection.State == ConnectionState.Closed) { con.OpenConnection(); }

                string query = @"
                SELECT 
                    Operation_tbl.Id AS OperationId,
                    Operation_tbl.OperationDate AS OperationDate,
                    Operation_tbl.VendorId AS VendorID,
                    Operation_tbl.ProductId AS ProductID,
                    Operation_tbl.BoxesCount AS Count,
                    Operation_tbl.BoxWeight AS BoxWeight,
                    Operation_tbl.PalleteWeight AS PalleteWeight,
                    Operation_tbl.BoxesWeight AS BoxesWeight,
                    Operation_tbl.QA AS QA,
                    Operation_tbl.UserId AS UserId,
                    Operation_tbl.Notes AS Notes,
                    Operation_tbl.Weight AS Weight,
                    Operation_tbl.DiscountWeight AS DiscountWeight,
                    Operation_tbl.TotalDiscount AS TotalDiscount,
                    Operation_tbl.NetWeight AS NetWeight,
                    Product_tbl.Name AS ProductName,
                    Vendor_tbl.Name AS VendorName,
                    User_tbl.FullName AS UserName
                FROM 
                    Operation_tbl
                INNER JOIN 
                    Product_tbl ON Product_tbl.Id = Operation_tbl.ProductId
                INNER JOIN 
                    Vendor_tbl ON Vendor_tbl.Id = Operation_tbl.VendorId
                INNER JOIN 
                    User_tbl ON User_tbl.Id = Operation_tbl.UserId
                WHERE 
                    IsRecieved = 0 AND " + condition + @"
                ORDER BY 
                    Operation_tbl.Id"
                ;

                using (SqlDataAdapter da = new SqlDataAdapter(query, con.Connection))
                {
                    if (!string.IsNullOrEmpty(condition))
                    {
                        da.SelectCommand.Parameters.Add("@date1", SqlDbType.DateTime).Value = txtFromDate.Value;
                        da.SelectCommand.Parameters.Add("@date2", SqlDbType.DateTime).Value = txtToDate.Value;
                    }

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

        // إضافة صف إلى DataGridView
        private void AddRowToDataGridView(DataRow row)
        {
            int rowIndex = dgvOperation.Rows.Add();
            DataGridViewRow currentRow = dgvOperation.Rows[rowIndex];

            currentRow.Cells[1].Value = true;
            currentRow.Cells[0].Value = row["VendorId"];
            currentRow.Cells[2].Value = row["VendorName"];
            currentRow.Cells[3].Value = row["ProductId"];
            currentRow.Cells[4].Value = row["ProductName"];
            currentRow.Cells[5].Value = row["Count"];
            currentRow.Cells[6].Value = row["BoxWeight"];
            currentRow.Cells[7].Value = row["Weight"];
            currentRow.Cells[8].Value = row["PalleteWeight"];
            currentRow.Cells[9].Value = row["BoxesWeight"];
            currentRow.Cells[10].Value = row["DiscountWeight"];
            currentRow.Cells[11].Value = row["TotalDiscount"];
            currentRow.Cells[12].Value = row["NetWeight"];
            currentRow.Cells[13].Value = row["QA"];
            currentRow.Cells[14].Value = row["UserId"];
            currentRow.Cells[15].Value = row["Notes"];
            currentRow.Cells[16].Value = Convert.ToDateTime(row["OperationDate"]).ToString("dd MMMM");
            currentRow.Cells[17].Value = Convert.ToDateTime(row["OperationDate"]).ToString("hh:mm:ss tt");
            currentRow.Cells[19].Value = row["OperationId"];
            currentRow.Cells[20].Value = row["OperationDate"];
            currentRow.Cells[21].Value = row["UserName"];
        }
        private void Changed (object sender, EventArgs e)
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
        private void MakeReceive()
        {
            try
            {
                // إنشاء نموذج الفاتورة
                frmReceive frm = new frmReceive();
                // ملء قائمة الموردين في ComboBox
                fills.fillComboBox(frm.cmbVendor, "Vendor_tbl", "Id", "Name");
                frm.cmbVendor.SelectedIndex = -1;
                // تعين تاريخ الفاتورة
                frm.txtDate.Value = DateTime.Now;
                // تحديد اسم البائع
                frm.Salesman.Text = User.FullName;

                // الحصول على الصفوف المحددة من DataGridView
                DataGridViewRowCollection rows = dgvOperation.Rows;

                // استخدام Dictionary للتحقق من وجود عميل واحد فقط للفاتورة
                Dictionary<int, bool> customerCheck = new Dictionary<int, bool>();

                foreach (DataGridViewRow row in rows)
                {
                    // التحقق مما إذا كان الصف محددًا
                    if (Convert.ToBoolean(row.Cells[1].Value))
                    {
                        int VendorId = Convert.ToInt32(row.Cells[0].Value);

                        // التحقق من وجود عميل واحد فقط للفاتورة
                        if (!customerCheck.ContainsKey(VendorId))
                        {
                            customerCheck[VendorId] = true;

                            // تحديث بيانات العميل الأول
                            if (frm.cmbVendor.SelectedIndex == -1)
                            {
                                frm.cmbVendor.SelectedValue = VendorId;
                                frm.txtID.Text = VendorId.ToString();
                            }
                            else if (frm.cmbVendor.SelectedValue != null && Convert.ToInt32(frm.cmbVendor.SelectedValue) != VendorId)
                            {
                                MyBox.Show("يجب ألا يكون هناك موردان لفاتورة واحدة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        // إضافة البيانات إلى جدول المبيعات
                        frm.dgvSale.Rows.Add(
                            row.Cells[3].Value,
                            row.Cells[5].Value,
                            row.Cells[4].Value,
                            row.Cells[7].Value,
                            row.Cells[8].Value,
                            row.Cells[6].Value,
                            row.Cells[9].Value,
                            row.Cells[10].Value,
                            row.Cells[11].Value,
                            row.Cells[12].Value,
                            row.Cells[13].Value,
                            row.Cells[14].Value,
                            row.Cells[15].Value,
                            row.Cells[16].Value,
                            row.Cells[19].Value
                        );
                    }
                }
                // حساب الفاتورة
                frm.CulcInv();

                // عرض النموذج
                frm.ShowDialog();
            }
            catch(Exception ex) { MyBox.Show($"خطأ غير متوقع :{Environment.NewLine} تفاصيل الخطأ : {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnMakeReceive_Click(object sender, EventArgs e)
        {
            MakeReceive();
        }
    }
}
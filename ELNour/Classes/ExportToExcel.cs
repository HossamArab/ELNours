using MessageBoxes;
using System;
using System.Windows.Forms;
using OfficeOpenXml; // EPPlus
using OfficeOpenXml.Style;
using System.IO;

namespace ELNour.Classes
{
    class ExportToExcel
    {
        public static void Excel(DataGridView dgv)
        {
            //try
            //{
                // إنشاء لحفظ الملف
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Files |*.xlsx";
                if (sfd.ShowDialog() != DialogResult.OK) return;

                // إنشاء ملف Excel جديد
                using (var package = new ExcelPackage())
                {
                    // إضافة ورقة عمل
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");
                    worksheet.View.RightToLeft = true; // لغة عربية

                    // إضافة عنوان الأعمدة
                    for (int col = 0; col < dgv.Columns.Count; col++)
                    {
                        worksheet.Cells[1, col + 1].Value = dgv.Columns[col].HeaderText;
                    }

                    // إضافة بيانات الصفوف
                    for (int row = 0; row < dgv.Rows.Count; row++)
                    {
                        for (int col = 0; col < dgv.Columns.Count; col++)
                        {
                            worksheet.Cells[row + 2, col + 1].Value = dgv[col, row].Value?.ToString() ?? "";
                        }
                    }

                    // تطبيق التنسيق على نطاق البيانات
                    var usedRange = worksheet.Cells[1, 1, dgv.Rows.Count + 1, dgv.Columns.Count];
                    usedRange.Style.Font.Bold = true;
                    usedRange.Style.Font.Size = 13;
                    usedRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    usedRange.Style.Border.BorderAround(ExcelBorderStyle.Thin);

                    // ضبط عرض العمود تلقائيًا
                    worksheet.Cells.AutoFitColumns();

                    // حفظ الملف
                    using (var stream = File.Create(sfd.FileName))
                    {
                        package.SaveAs(stream);
                    }
                }

                // إظهار رسالة نجاح
                MyBox.Show("تم تصدير البيانات بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //catch (Exception ex)
            //{
            //    MyBox.Show($"حدث خطأ: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
    }
}

using MessageBoxes;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ELNour.Classes
{
    internal class ImportFromExcel
    {
        public static void Excel(string filePath, DataGridView dataGridView)
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                if (!File.Exists(filePath))
                {
                    MyBox.Show("الملف غير موجود", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataTable dt = new DataTable();
                FileInfo fileInfo = new FileInfo(filePath);

                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    if (package.Workbook.Worksheets.Count == 0)
                    {
                        MyBox.Show("لا توجد أوراق عمل في الملف", "تنبيه", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        return;
                    }

                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    // قراءة العناوين
                    int colCount = worksheet.Dimension.End.Column;
                    for (int col = 1; col <= colCount; col++)
                    {
                        dt.Columns.Add(worksheet.Cells[1, col].Text ?? $"Column{col}");
                    }

                    // قراءة البيانات
                    int rowCount = worksheet.Dimension.End.Row;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        DataRow dr = dt.NewRow();
                        bool rowIsEmpty = true;

                        for (int col = 1; col <= colCount; col++)
                        {
                            var cellValue = worksheet.Cells[row, col].Text;
                            dr[col - 1] = cellValue;

                            if (!string.IsNullOrEmpty(cellValue))
                                rowIsEmpty = false;
                        }

                        if (!rowIsEmpty)
                            dt.Rows.Add(dr);
                    }
                }
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dataGridView.Columns[i].HeaderText = dt.Columns[i].ColumnName;
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dataGridView.Rows.Add();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        dataGridView.Rows[i].Cells[j].Value = dt.Rows[i][j];
                    }
                }
                //dataGridView.DataSource = dt;
                //dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MyBox.Show($"حدث خطأ: {Environment.NewLine} {ex.Message}","خطأ",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

    }
}

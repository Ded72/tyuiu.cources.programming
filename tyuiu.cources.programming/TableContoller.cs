using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using tyuiu.cources.programming.interfaces;
using OfficeOpenXml;

namespace tyuiu.cources.programming
{

    public class TableContoller
    {
        public GitController gitController { get; }
        private readonly string taskNumber;

        public TableContoller(
            string taskNumber,
            GitController gitController)
        {
            this.taskNumber = taskNumber;
            this.gitController = gitController;
        }



        public string WriteExcelReport(string csvReportPath)
        {
            string studentResultExcelFile = $@"{Path.GetDirectoryName(csvReportPath)}\ExcelReport-Task{taskNumber}.xlsx";
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(new FileInfo(studentResultExcelFile)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

                if (worksheet == null)
                {
                    worksheet = package.Workbook.Worksheets.Add("Лист1");
                }
                string[] lines = File.ReadAllLines(csvReportPath); ;
                int rowIndex = 1;
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    for (int i = 0; i < parts.Length - 1; i++)
                    {
                        worksheet.Cells[rowIndex, i + 1].Value = parts[i];
                    }
                    Uri url;
                    if (Uri.TryCreate(parts[parts.Length - 1], UriKind.Absolute, out url))
                    {
                        worksheet.Cells[rowIndex, parts.Length].Hyperlink = url;
                        worksheet.Cells[rowIndex, parts.Length].Style.Font.UnderLine = true;
                        worksheet.Cells[rowIndex, parts.Length].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                    }
                    else
                    {
                        worksheet.Cells[rowIndex, parts.Length].Value = parts[parts.Length - 1];
                    }
                    rowIndex++;
                }
                worksheet.Cells.AutoFitColumns();
                package.Save();
            }
            return studentResultExcelFile;
        }

        public string MergeTables(string pathToTables)
        {
            string outputFilePath = Path.Combine(pathToTables, "MergedTables.xlsx");
            if (File.Exists(outputFilePath))
            {
                File.Delete(outputFilePath);
            }
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet mergedWorksheet = package.Workbook.Worksheets.Add("MergedData");
                string[] excelFiles = Directory.GetFiles(pathToTables, "*.xls*");
                mergedWorksheet.Cells["A1"].Value = "Группа";
                mergedWorksheet.Cells["B1"].Value = "ФИО";
                mergedWorksheet.Cells["C1"].Value = "Спринт";
                mergedWorksheet.Cells["D1"].Value = "Таск";
                mergedWorksheet.Cells["E1"].Value = "Вариант";
                mergedWorksheet.Cells["F1"].Value = "Дата сдачи";
                mergedWorksheet.Cells["G1"].Value = "Дата проверки";
                mergedWorksheet.Cells["H1"].Value = "Статус задания";
                mergedWorksheet.Cells["I1"].Value = "Баллы";
                mergedWorksheet.Cells["J1"].Value = "Бонус";
                mergedWorksheet.Cells["K1"].Value = "Сумма";
                mergedWorksheet.Cells["L1"].Value = "Сcылка";
                int startRow = 2;
                foreach (var file in excelFiles)
                {
                    using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(file)))
                    {
                        ExcelWorksheet currentWorksheet = excelPackage.Workbook.Worksheets[0];
                        int rowCount = currentWorksheet.Dimension.End.Row;
                        int colCount = currentWorksheet.Dimension.End.Column;
                        bool hasData = false;
                        for (int row = 2; row <= rowCount; row++)
                        {
                            for (int col = 1; col <= colCount; col++)
                            {
                                mergedWorksheet.Cells[startRow, col].Value = currentWorksheet.Cells[row, col].Value;
                                if (col == colCount)
                                {
                                    Uri url;
                                    string cellValue = (currentWorksheet.Cells[row, col].Value ?? "").ToString();
                                    if (Uri.TryCreate(cellValue, UriKind.Absolute, out url))
                                    {
                                        mergedWorksheet.Cells[startRow, col].Hyperlink = url;
                                        mergedWorksheet.Cells[startRow, col].Style.Font.UnderLine = true;
                                        mergedWorksheet.Cells[startRow, col].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                    }
                                }
                                if (currentWorksheet.Cells[row, col].Value != null)
                                {
                                    hasData = true;
                                }
                            }
                            if (hasData)
                            {
                                startRow++;
                                hasData = false;
                            }
                        }
                    }
                }
                mergedWorksheet.Cells.AutoFitColumns();
                package.SaveAs(new FileInfo(outputFilePath));
            }
            return outputFilePath;
        }

    }
}


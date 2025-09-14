using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Common;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using ClaimRequest.Application.Users.ExportPersonalSalary;

namespace ClaimRequest.Application.Users.ExportListSalary
{
    public class ExportListSalaryQueryHandler(IDbContext context) : IQueryHandler<ExportListSalaryQuery, byte[]>
    {
        public async Task<Result<byte[]>> Handle(ExportListSalaryQuery query, CancellationToken cancellationToken)
        {
            DateOnly previousMonthThirdDay = new DateOnly(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 1);


            var list = await context.Users
            .Include(u => u.SalaryPerMonths).ToListAsync();

            var userSalaryDict = list.ToDictionary(
                user => user,
                user => user.SalaryPerMonths.FirstOrDefault(s => s.MonthYear == previousMonthThirdDay)
            
            );
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Salary Report");

                string[] headers = { "Employee Name", "Month/Year", "Base Salary", "Overtime Hours", 
                    "Salary Per Overtime Hour", "Late/Early Leave Cases", "Abnormal Cases", 
                    "Fine Per Late/Early Case", "Fine Per Abnormal Case", "Other Money", "Total Salary" };

                int totalColumns = headers.Length;
                int startRow = 1;

                var titleRange = worksheet.Cells[1, 1, 1, totalColumns];
                titleRange.Merge = true;
                titleRange.Value = "SALARY REPORT";
                titleRange.Style.Font.Size = 16;
                titleRange.Style.Font.Bold = true;
                titleRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                titleRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                titleRange.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(48, 84, 150));
                titleRange.Style.Font.Color.SetColor(Color.White);

                int headerRow = 3;
                for (int col = 1; col <= headers.Length; col++)
                {
                    worksheet.Cells[headerRow, col].Value = headers[col - 1];
                    var headerCell = worksheet.Cells[headerRow, col];
                    headerCell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    headerCell.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(141, 180, 226));
                    headerCell.Style.Font.Bold = true;
                    headerCell.Style.Font.Color.SetColor(Color.FromArgb(0, 0, 0));
                    headerCell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    headerCell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }

                int row = headerRow + 1;
                    foreach (var pair in userSalaryDict)
                    {

                    /*decimal totalSalary = pair.Value.BaseSalary
                        + (pair.Value.OvertimeHours * pair.Value.SalaryPerOvertimeHour)
                        - (pair.Value.LateEarlyLeaveCases * pair.Value.FinePerLateEarlyCase)
                        - (pair.Value.AbnormalCases * pair.Value.FinePerAbnormalCase);*/

                        if (pair.Value == null) continue;
                        worksheet.Cells[row, 1].Value = pair.Key.FullName;
                        worksheet.Cells[row, 2].Value = pair.Value.MonthYear.ToDateTime(TimeOnly.MinValue);
                        worksheet.Cells[row, 3].Value = pair.Value.BaseSalary;
                        worksheet.Cells[row, 4].Value = pair.Value.OvertimeHours;
                        worksheet.Cells[row, 5].Value = pair.Value.SalaryPerOvertimeHour;
                        worksheet.Cells[row, 6].Value = pair.Value.LateEarlyLeaveCases;
                        worksheet.Cells[row, 7].Value = pair.Value.AbnormalCases;
                        worksheet.Cells[row, 8].Value = pair.Value.FinePerLateEarlyCase;
                        worksheet.Cells[row, 9].Value = pair.Value.FinePerAbnormalCase;
                        worksheet.Cells[row, 10].Value = pair.Value.OtherMoney; 
                        worksheet.Cells[row, 11].Value = pair.Value.TotalSalary;

                        worksheet.Cells[row, 2].Style.Numberformat.Format = "dd-mm-yyyy"; 
                        var rowColor = row % 2 == 0
                            ? Color.FromArgb(234, 234, 234)
                            : Color.White;
                        worksheet.Cells[row, 1, row, totalColumns].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[row, 1, row, totalColumns].Style.Fill.BackgroundColor.SetColor(rowColor);

                        var currencyColumns = new[] { 3, 5, 8, 9, 10, 11 };
                        foreach (var col in currencyColumns)
                        {
                            worksheet.Cells[row, col].Style.Numberformat.Format = "#,##0.00";
                            worksheet.Cells[row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        }

                        var numberColumns = new[] { 4, 6, 7 };
                        foreach (var col in numberColumns)
                        {
                            worksheet.Cells[row, col].Style.Numberformat.Format = "#,##0";
                            worksheet.Cells[row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        }

                        worksheet.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        worksheet.Cells[row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        row++;
                    }
                var totalRow = row;
                worksheet.Cells[totalRow, 1].Value = "Total";
                worksheet.Cells[totalRow, 1, totalRow, 2].Merge = true;
                worksheet.Cells[totalRow, 1, totalRow, 2].Style.Font.Bold = true;
                worksheet.Cells[totalRow, 1, totalRow, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                var sumColumns = new[] { 3, 4, 5, 6, 7, 8, 9, 10, 11 };
                foreach (var col in sumColumns)
                {
                    worksheet.Cells[totalRow, col].Formula = $"SUM({GetExcelColumnName(col)}{headerRow + 1}:{GetExcelColumnName(col)}{row - 1})";
                    worksheet.Cells[totalRow, col].Style.Font.Bold = true;
                    worksheet.Cells[totalRow, col].Style.Numberformat.Format = col == 4 || col == 6 || col == 7 ? "#,##0" : "#,##0.00";
                }

                worksheet.Cells[totalRow, 1, totalRow, totalColumns].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[totalRow, 1, totalRow, totalColumns].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(189, 215, 238));

                worksheet.Cells[headerRow, 1, row, totalColumns].AutoFitColumns();

                worksheet.Column(3).Width = Math.Max(worksheet.Column(3).Width, 15); // Base Salary
                worksheet.Column(10).Width = Math.Max(worksheet.Column(10).Width, 15); // Other Money
                worksheet.Column(11).Width = Math.Max(worksheet.Column(11).Width, 15); // Total Salary

                var dataRange = worksheet.Cells[headerRow, 1, totalRow, totalColumns];
                dataRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                dataRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                dataRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                return package.GetAsByteArray();
            }
        }

        private string GetExcelColumnName(int columnNumber)
        {
            string columnName = "";
            while (columnNumber > 0)
            {
                int remainder = (columnNumber - 1) % 26;
                columnName = Convert.ToChar('A' + remainder) + columnName;
                columnNumber = (columnNumber - 1) / 26;
            }
            return columnName;
        }
    }
}

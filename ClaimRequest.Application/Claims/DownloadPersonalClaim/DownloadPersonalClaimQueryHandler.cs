using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Common;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;


namespace ClaimRequest.Application.Claims.DownloadPersonalClaim
{
    public class DownloadPersonalClaimCommandHandler(IUserContext userContext, IDbContext context) : IQueryHandler<DownloadPersonalClaimQuery, byte[]>
    {
        public async Task<Result<byte[]>> Handle(DownloadPersonalClaimQuery query, CancellationToken cancellationToken)
        {
            var user = userContext.UserId;

            var username = await context.Users.FirstOrDefaultAsync(c => c.Id == user);

            var personalClaims = await context.Claims
                .Where(c => c.UserId == user)
                .ToListAsync(cancellationToken);

            var reasonDictionary = await context.Reasons
                .ToDictionaryAsync(r => r.Id, r => r.Name, cancellationToken);

            var approverDictionary = await context.Users
                .Where(u => personalClaims.Select(c => c.ApproverId).Contains(u.Id))
                .ToDictionaryAsync(u => u.Id, u => u.FullName, cancellationToken);

            var supervisorDictionary = await context.Users
                .Where(u => personalClaims.Select(c => c.SupervisorId).Contains(u.Id))
                .ToDictionaryAsync(u => u.Id, u => u.FullName, cancellationToken);

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Claims");

                // Headers
                string[] headers = { "Reason Name", "OtherReasonText", "Status", "Approver Name", "Supervisor Name",
                         "Claim Fee", "Partial", "Start Date", "End Date", "ExpectApproveDay" };

                int totalColumns = headers.Length;
                int startRow = 1;

                for (int col = 1; col <= headers.Length; col++)
                {
                    worksheet.Cells[1, col].Value = headers[col - 1];

                    // Format Title
                    worksheet.Cells[1, col].Style.Fill.PatternType = ExcelFillStyle.Solid; 
                    worksheet.Cells[1, col].Style.Fill.BackgroundColor.SetColor(Color.LightGray); // Background color
                    worksheet.Cells[1, col].Style.Font.Bold = true; // Bold Text
                    worksheet.Cells[1, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Center
                }

                //Rows
                int row = 2;
                foreach (var claim in personalClaims)
                {
                    worksheet.Cells[row, 1].Value = reasonDictionary.GetValueOrDefault(claim.ReasonId, "Unknown Reason");
                    worksheet.Cells[row, 2].Value = claim.OtherReasonText;
                    worksheet.Cells[row, 3].Value = claim.Status;
                    worksheet.Cells[row, 4].Value = approverDictionary.GetValueOrDefault(claim.ApproverId, "Unknown Approver");
                    worksheet.Cells[row, 5].Value = supervisorDictionary.GetValueOrDefault(claim.ApproverId, "Unknown Approver"); ;
                    /*
                    worksheet.Cells[row, 6].Value = claim.ClaimFee;
                    */
                    worksheet.Cells[row, 7].Value = claim.Partial;              
                    worksheet.Cells[row, 8].Value = claim.StartDate.ToDateTime(TimeOnly.MinValue);
                    worksheet.Cells[row, 9].Value = claim.EndDate.ToDateTime(TimeOnly.MinValue);     
                    worksheet.Cells[row, 10].Value = claim.ExpectApproveDay.ToDateTime(TimeOnly.MinValue);
                    
                    //Define Date Format
                    worksheet.Cells[row, 8].Style.Numberformat.Format = "dd-mm-yyyy";
                    worksheet.Cells[row, 9].Style.Numberformat.Format = "dd-mm-yyyy";
                    worksheet.Cells[row, 10].Style.Numberformat.Format = "dd-mm-yyyy";

                    //Define HorizontalAlignment Left
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Cells[row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Cells[row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Cells[row, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    //Define HorizontalAlignment Right
                    worksheet.Cells[row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                    //Define HorizontalAlignment Center
                    worksheet.Cells[row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;  
                    worksheet.Cells[row, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    row++;
                }
                worksheet.Cells.AutoFitColumns();

                // Borders
                int endRow = row - 1; 
                var range = worksheet.Cells[startRow, 1, endRow, totalColumns];

                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                return package.GetAsByteArray();
            }
        }
    }
}

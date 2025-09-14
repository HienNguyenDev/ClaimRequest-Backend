using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.ClaimOverTimes.GetOverTimeEffortToApprove;
using ClaimRequest.Application.Extensions;
using ClaimRequest.Domain.ClaimOverTime;
using ClaimRequest.Domain.ClaimOverTime.Errors;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Users;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Application.ClaimOverTimes.GetOverTimeEffortByOTMember
{
    public class GetOverTimeEffortByOTMemberQueryHandler(IDbContext context, IUserContext userContext)
    : IQueryHandler<GetOverTimeEffortByOTMemberQuery, Page<GetOverTimeEffortByOTMemberResponse>>
    {
        public async Task<Result<Page<GetOverTimeEffortByOTMemberResponse>>> Handle(GetOverTimeEffortByOTMemberQuery request, CancellationToken cancellationToken)
        {
            var user = userContext.UserId;

            var overTimeEfforts = context.OverTimeEffort
                .Where(e => e.OverTimeMember.UserId == user && e.Status == request.Status)
                .OrderByDescending(o => o.OverTimeDate.Date)
                .Select(e => new GetOverTimeEffortByOTMemberResponse
                {
                    Id = e.Id,
                    OverTimeMemberId = e.OverTimeMember.Id,
                    OverTimeDateId = e.OverTimeDate.Id,
                    DayHours = e.DayHours,
                    NightHours = e.NightHours,
                    TaskDescription = e.TaskDescription,
                    Status = e.Status,
                    OverTimeMember = new OverTimeMemberResponse
                    {
                        Id = e.OverTimeMember.Id,
                        UserId = e.OverTimeMember.UserId,
                        RequestId = e.OverTimeMember.RequestId
                    },
                    OverTimeDate = new OverTimeDateResponse
                    {
                        Id = e.OverTimeDate.Id,
                        Date = e.OverTimeDate.Date,
                        OverTimeRequestId = e.OverTimeDate.OverTimeRequestId
                    },
                    OverTimeRequest = new OverTimeRequestResponse
                    {
                        Id = e.OverTimeMember.Request.Id,
                        ProjectId = e.OverTimeMember.Request.ProjectId,
                        ApproverName = e.OverTimeMember.Request.Approver.FullName,
                        ProjectName = e.OverTimeMember.Request.Project.Name,

                    }
                });

            var total = await overTimeEfforts.CountAsync(cancellationToken);
            var result = await overTimeEfforts
                .ApplyPagination(request.PageNumber, request.PageSize)
                .ToListAsync(cancellationToken);

            return new Page<GetOverTimeEffortByOTMemberResponse>(result, total, request.PageNumber, request.PageSize);
        }
    }

}

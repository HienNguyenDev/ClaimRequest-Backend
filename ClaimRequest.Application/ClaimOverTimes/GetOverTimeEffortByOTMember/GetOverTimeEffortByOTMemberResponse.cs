using ClaimRequest.Domain.ClaimOverTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Application.ClaimOverTimes.GetOverTimeEffortByOTMember
{
    public class GetOverTimeEffortByOTMemberResponse
    {
        public Guid Id { get; set; }
        public Guid OverTimeMemberId { get; set; }
        public Guid OverTimeDateId { get; set; }
        public int Hours { get; set; }
        public int DayHours { get; set; }
        public int NightHours { get; set; }
        public string TaskDescription { get; set; } = null!;
        public OverTimeEffortStatus Status { get; set; }
        public OverTimeMemberResponse? OverTimeMember { get; set; }
        public OverTimeDateResponse? OverTimeDate { get; set; }

        public OverTimeRequestResponse? OverTimeRequest { get; set; }

    }

    public class OverTimeMemberResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RequestId { get; set; }
    }

    public class OverTimeRequestResponse
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public string ApproverName { get; set; }
        public string ProjectName { get; set; }

    }

    public class OverTimeDateResponse
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        public Guid OverTimeRequestId { get; set; }
    }
}

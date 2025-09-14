using ClaimRequest.Domain.ClaimOverTime;

namespace ClaimRequest.Application.ClaimOverTimes.GetOverTimeGroupByUserEffortOfARequest
{
    public class GetOverTimeGroupByUserEffortOfARequestQueryResponse
    {
        public Guid OverTimeMemberId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public List<EffortOfAMemberInARequestResponse>? MemberEfforts { get; set; }
    }

    public class EffortOfAMemberInARequestResponse
    {
        public Guid OverTimeEffortId { get; set; }
        public DateOnly Date { get; set; }
        public int Hours { get; set; }
        public int DayHours { get; set; }
        public int NightHours { get; set; }
        public string TaskDescription { get; set; } = null!;
        public OverTimeEffortStatus Status { get; set; }
    }
}

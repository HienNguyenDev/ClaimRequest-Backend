using ClaimRequest.Domain.ClaimOverTime;

namespace ClaimRequest.Application.ClaimOverTimes.GetListOverTimeRequestCurrentUserHasBeenPicked
{
    public class GetListOverTimeRequestCurrentUserHasBeenPickedQueryResponse
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
      
        public string? ProjectName { get; set; }
        public Guid ProjectManagerId { get; set; }
        public string ProjectManagerName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public OverTimeRequestStatus Status { get; set; }
        public Guid ApproverId { get; set; }
        public string? ApproverName { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public List<OverTimeMemBerResponse>? OverTimeMemBer { get; set; }
        public List<OverTimeDateResponse>? OverTimeDate { get; set; }
        public Guid RoomId { get; set; }
        public string? RoomName { get; set; }
        public Guid SiteId { get; set; }
        public string? SiteName { get; set; }

        public bool HasWeekend { get; set; }
        public bool HasWeekday { get; set; }
    }
    public class OverTimeMemBerResponse()
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public string UserName { get; set; }
        
    }

    public class OverTimeDateResponse() 
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        
    }

}

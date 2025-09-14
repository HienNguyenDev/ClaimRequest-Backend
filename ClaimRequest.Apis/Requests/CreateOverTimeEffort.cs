namespace ClaimRequest.Apis.Requests
{
    public class CreateOverTimeEffort
    {
        public Guid RequestId { get; set; }
        public Guid OverTimeDateId { get; set; }
        public Guid OverTimeMemberId { get; set; }
        public int DayHours { get; set; }
        public int NightHours { get; set; }
        public required string TaskDescription { get; set; }
    }
}

namespace ClaimRequest.Apis.Requests
{
    public class CreateReasonRequest
    {
        public Guid RequestTypeId { get; set; }
        public string Name { get; set; } = null!;
    }
}

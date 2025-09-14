
namespace ClaimRequest.Application.ReasonApplication.GetReasons
{
    public sealed record ReasonsResponse
    {
        public Guid Id { get; set; }
        public Guid RequestTypeId { get; set; }
        public string Name { get; set; } = null!;
        public bool IsOther { get; set; }
      
    }
}

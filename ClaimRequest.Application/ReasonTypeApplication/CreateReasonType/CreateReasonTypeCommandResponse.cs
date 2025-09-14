
namespace ClaimRequest.Application.ReasonTypeApplication.CreateReasonType
{
    public sealed record CreateReasonTypeCommandResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}

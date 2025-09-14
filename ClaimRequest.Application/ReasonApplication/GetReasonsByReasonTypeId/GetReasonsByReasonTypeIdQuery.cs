using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.ReasonApplication.GetReasons;


namespace ClaimRequest.Application.ReasonApplication.GetReasonsByReasonTypeId
{
    public class GetReasonsByReasonTypeIdQuery : IQuery<List<ReasonsResponse>>
    {
        public Guid Id { get; set; }
    }
}

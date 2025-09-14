using ClaimRequest.Application.Abstraction.Messaging;

namespace ClaimRequest.Application.ClaimOverTimes.GetOverTimeDateToDeclareEffort
{
    public class GetOverTimeDateToDeclareEffortQuery : IQuery<List<GetOverTimeDateToDeclareEffortQueryResponse>>
    {
        public Guid Id { get; set; }
    }
}

using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.ReasonTypeApplication.CreateReasonType;


namespace ClaimRequest.Application.ReasonTypeApplication.GetReasonTypeList
{
    public  class GetReasonTypeListQuery : IQuery<List<CreateReasonTypeCommandResponse>>
    {
    }
}

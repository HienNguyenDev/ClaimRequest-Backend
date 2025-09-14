using ClaimRequest.Application.Abstraction.Messaging;

namespace ClaimRequest.Application.ReasonTypeApplication.CreateReasonType
{
    public sealed class CreateReasonTypeCommand : ICommand<CreateReasonTypeCommandResponse>
    {
        public string Name { get; set; }
    }
}

using ClaimRequest.Application.Abstraction.Messaging;

namespace ClaimRequest.Application.EmailTemplates.UpdateEmailTemplate;

public class UpdateEmailTemplateCommand : ICommand<UpdateEmailTemplateResponse>
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string Header { get; set; }
    public string MainContent { get; set; }
}
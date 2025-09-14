namespace ClaimRequest.Apis.Requests;

public class UpdateEmailRequest
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string Header { get; set; }
    public string MainContent { get; set; }
}
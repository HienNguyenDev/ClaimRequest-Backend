namespace ClaimRequest.Application.Users.GetDepartments;

public class GetDepartmentsResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string Description { get; set; } = null!;
}
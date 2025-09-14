namespace ClaimRequest.Application.Users.CreateDepartment;

public class CreateDepartmentCommandResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Code { get; init; }
    public string Description { get; init; }
}
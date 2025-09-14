using ClaimRequest.Domain.Common;

namespace ClaimRequest.Domain.Users.Errors;

public class DepartmentErrors
{
    public static Error NotFound(Guid departmentId) => Error.NotFound(
        "Departments.NotFound",
        $"The department with the Id = '{departmentId}' was not found");
    
    public static readonly Error CodeNotUnique = Error.Conflict(
        "Departments.CodeNotUnique",
        "This name is already in use");
}
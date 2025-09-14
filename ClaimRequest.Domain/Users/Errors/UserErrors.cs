using ClaimRequest.Domain.Common;

namespace ClaimRequest.Domain.Users.Errors;

public static class UserErrors
{
    public static Error NotFound(Guid? userId) => Error.NotFound(
        "Users.NotFound",
        $"The user with the Id = '{userId}' was not found");

    public static readonly Error NotFoundByEmail = Error.NotFound(
        "Users.NotFoundByEmail",
        "The user with the specified email was not found");

    public static readonly Error EmailNotUnique = Error.Conflict(
        "Users.EmailNotUnique",
        "The provided email is not unique");

    public static readonly Error WrongPassword = Error.Conflict(
        "Users.WrongPassword",
        "The passsword for this account is wrong");
    public static readonly Error InActive = Error.Conflict(
        "Users.InActive",
        "The user is inactive");

    public static readonly Error InvalidRefreshToken = Error.Conflict(
        "Users.RefreshTokenInvalid",
        "The refresh token is invalid");
    
    public static readonly Error InvalidCurrentPassword = Error.Conflict(
        "Users.InvalidCurrentPassword",
        "The current password provided is incorrect.");

    public static readonly Error SamePassword = Error.Conflict(
           "SamePassword",
           "New password cannot be the same as the current password.");

    public static readonly Error IsNotVerified = Error.Conflict(
        "NotVerified",
        "Account is not verified");
    
    public static readonly Error BuLeaderAlreadyExist = Error.Failure(
        "BuLeaderAlreadyExist",
        "Can not have two bu leader in department");
    
    public static readonly Error NotAdmin = Error.Conflict(
    "Users.NotAdmin",
    "The user is not Admin");
    
    public static Error NotFound(List<Guid> userIds) => Error.NotFound(
        "Users.NotFound",
        $"The user with the Id = '{userIds}' was not found");
}
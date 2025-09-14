using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Users;
using ClaimRequest.Domain.Users.Errors;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.Users.UpdateUsers;

public sealed class UpdateUserCommandHandler(IDbContext context, IUserContext userContext) : ICommandHandler<UpdateUserCommand, UpdateUserResponse>
{
    public async Task<Result<UpdateUserResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
            if (user == null)
            {
                return Result.Failure<UpdateUserResponse>(UserErrors.NotFound(request.UserId));
            }
            
            user.FullName = request.FullName ?? user.FullName!;
            user.Email = request.Email ?? user.Email!;
            user.Role =  request.Role ?? user.Role!;
            user.Rank =  request.Rank ?? user.Rank!;
            user.BaseSalary =  request.BaseSalary ?? user.BaseSalary!;
            user.Status =  request.Status ?? user.Status!;

            if (request.DepartmentId.HasValue)
            {
                var departmentExists = await context.Departments.AnyAsync(d => d.Id == request.DepartmentId, cancellationToken);
                if (!departmentExists)
                {
                    return Result.Failure<UpdateUserResponse>(DepartmentErrors.NotFound(request.DepartmentId!.Value));
                }
                user.DepartmentId = request.DepartmentId.Value;
            }
            
            
            await context.SaveChangesAsync(cancellationToken);

            return Result.Success(new UpdateUserResponse(user));
        }
        catch (Exception ex)
        {
            return null;
        }
    }

}
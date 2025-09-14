using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Users.Errors;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.Users.SearchUserByEmail;

public class SearchUserByEmailQueryHandler (IDbContext dbContext) : IQueryHandler<SearchUserByEmailQuery, SearchUserByEmailItem>
{
    public async Task<Result<SearchUserByEmailItem>> Handle(SearchUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var thisMonthYear = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, 1);

        var user = await dbContext.Users
            .Where(u => u.Email.Trim().ToLower() == request.Email.Trim().ToLower())
            .Select(u => new SearchUserByEmailItem
            {
                    UserId = u.Id,
                    UserName = u.FullName,
                    Email = u.Email,
                    NumberOfOvertimeHours = u.SalaryPerMonths.Where(s => s.MonthYear == thisMonthYear && s.UserId == u.Id)
                                                             .Select(s => s.OvertimeHours)
                                                             .FirstOrDefault(),
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
        {
            return Result.Failure<SearchUserByEmailItem>(UserErrors.NotFoundByEmail);
        }

        return Result<SearchUserByEmailItem>.Success(user);
    }

}


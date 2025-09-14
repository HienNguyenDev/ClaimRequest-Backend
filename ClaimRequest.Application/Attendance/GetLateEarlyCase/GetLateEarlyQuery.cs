using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.Abstraction.Query;
using ClaimRequest.Domain.Common;

namespace ClaimRequest.Application.Attendance.GetLateEarlyCase;

public class GetLateEarlyQuery : IPageableQuery, IQuery<Page<LateEarlyQueryItem>> 
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}
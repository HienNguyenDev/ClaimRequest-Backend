using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.Abstraction.Query;
using ClaimRequest.Domain.Common;

namespace ClaimRequest.Application.Attendance.GetAbnormalCase;

public class GetAbnormalQuery : IPageableQuery,  IQuery<Page<GetAbnormalItem>>
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}
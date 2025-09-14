using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimRequest.Application.Abstraction.Query;
using ClaimRequest.Domain.Common;

namespace ClaimRequest.Application.Users.GetUsers
{
    public class GetUsersQuery : IPageableQuery, IQuery<Page<GetUsersResponse>>
    {
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
    }
}

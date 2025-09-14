using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ClaimRequest.Application.Extensions;

namespace ClaimRequest.Application.Users.GetUsers
{
    public class GetUsersQueryHandler(IDbContext context) : IQueryHandler<GetUsersQuery, Page<GetUsersResponse>>
    {
        public async Task<Result<Page<GetUsersResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            //var allowedUsers = new List<int> { (int)UserRole.Developer,(int)UserRole.ProjectManager, (int)UserRole.Finance,(int)UserRole.BULeader};

            var list = context.Users.Where(u => u.Role != UserRole.Admin);
            
            var result = await list.ApplyPagination(
                                request.PageNumber,
                                request.PageSize).Select(x => new GetUsersResponse {
                                                                Id = x.Id,
                                                                FullName = x.FullName,
                                                                Email = x.Email,
                                                                BaseSalary = x.BaseSalary,
                                                                Role = x.Role,
                                                                Rank = x.Rank,
                                                                // DepartmentId = x.DepartmentId,
                                                                Departments = x.Departments != null ? new GetDepartmentResponse
                                                                {
                                                                    Id = x.Departments.Id,
                                                                    Name = x.Departments.Name,
                                                                    Code = x.Departments.Code,
                                                                    Description = x.Departments.Description
                                                                } : null,
                                                                Status = x.Status,
                                                                IsSoftDelete = x.IsSoftDelete
                                                                }).ToListAsync();
            
            return new Page<GetUsersResponse>
                    (result, 
                    list.Count(), 
                    request.PageNumber, 
                    request.PageSize);
            
            /*var response =  list.Select(x => new GetUsersResponse {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email,
                Password = x.Password,
                BaseSalary = x.BaseSalary,
                Role = x.Role,
                Rank = x.Rank,
                DepartmentId = x.DepartmentId,
                Status = x.Status,
                IsSoftDelete = x.IsSoftDelete
            })
                .ToList();*/

        }
    }
}

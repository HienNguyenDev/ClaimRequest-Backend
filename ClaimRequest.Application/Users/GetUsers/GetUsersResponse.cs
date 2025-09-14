using ClaimRequest.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Application.Users.GetUsers
{
    public sealed class GetUsersResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
       // public string Password { get; set; } = null!;
        public UserRole Role { get; set; }
        public UserRank Rank { get; set; }
        public decimal BaseSalary { get; set; }
        public byte IsSoftDelete { get; set; }
        public UserStatus Status { get; set; }
        // public string DepartmentName { get; set; }
        // public Guid DepartmentId { get; set; }
        public GetDepartmentResponse Departments { get; set; }
    }
}

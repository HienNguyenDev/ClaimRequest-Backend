using ClaimRequest.Domain.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Application.Projects.GetCurrentUserIsPM
{
    public sealed class GetProjectCurrentUserIsPMQueryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Guid ProjectID { get; set; }
        public Guid UserID { get; set; }
        public RoleInProject RoleInProject { get; set; }
        
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        
        public string CustomerName { get; set; }
    }
}

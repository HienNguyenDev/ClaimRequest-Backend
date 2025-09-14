using ClaimRequest.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Domain.Projects.Errors
{
    public class ProjectmemberErrors
    {
        public static readonly Error MemberAlreadyExists = Error.Conflict(
            "ProjectMembers.MemberAlreadyExists",
            "The user is already a member of this project.");

        public static Error NotFound(Guid projectMemberId) => Error.NotFound(
            "ProjectMembers.NotFound",
            $"The project member with the Id = '{projectMemberId}' was not found.");

        public static readonly Error RoleOutOfRange = Error.Conflict(
            "ProjectMembers.RoleOutOfRange",
            "RoleInProject must be either 1 (Developer) or 2 (ProjectManager).");
        
        public static Error UserNotIsProjectManager(Guid userId, Guid projectId) => Error.NotFound(
            "ProjectMembers.UserNotIsProjectManager",
            $"Invalid project manager with id {userId} in project with Id = '{projectId}'.");
        
        public static Error UserNotInProject(Guid Id) => Error.NotFound(
            "Project.UserNotInProject",
            $"Invalid project manager with id {Id}");
        public static Error UserIsNotProjectManagerInAnyProject(Guid userId) => Error.NotFound(
            "ProjectMembers.UserNotIsProjectManager",
            $"This user with Id: {userId} is not project manager in any project");
    }
}


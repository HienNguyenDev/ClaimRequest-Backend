using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimRequest.Domain.Common;

namespace ClaimRequest.Domain.Projects.Errors
{
    public static class ProjectErrors
    {
        public static readonly Error CodeNotUnique = Error.Conflict(
        "Projects.CodeNotUnique",
        "The provided code is not unique");
        public static Error NotFound(Guid Id) => Error.NotFound(
            "Projects.NotFound",
            $"The project with the Id = '{Id}' was not found");
        public static readonly Error InvalidName = Error.Failure(
            "UpdateProject.InvalidName",
            "The project name is either empty or exceeds the maximum length of 255 characters");

        public static readonly Error CodeAlreadyExists = Error.Conflict(
            "UpdateProject.CodeAlreadyExists",
            "A project with this code already exists");

        public static readonly Error InvalidDateRange = Error.Failure(
            "UpdateProject.InvalidDateRange",
            "Start date must be before or equal to end date");

        public static readonly Error InvalidStatus = Error.Failure(
            "UpdateProject.InvalidStatus",
            "Invalid project status, only 'Finished' or 'InProgress' are allowed");

        public static readonly Error InvalidSoftDeleteValue = Error.Failure(
            "UpdateProject.InvalidSoftDeleteValue",
            "Invalid IsSoftDelete value, only 0 or 1 is allowed");
    }
}

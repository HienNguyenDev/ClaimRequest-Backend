using ClaimRequest.Application.Abstraction.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Application.Users.ReActiveUser
{
    public class ReActiveUserCommand : ICommand
    {
        public string Email { get; set; } = null!;
    }
}

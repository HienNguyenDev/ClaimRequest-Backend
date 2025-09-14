using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClaimRequest.Application.Users.ChangePassword
{
    public sealed class ChangePasswordCommand : Abstraction.Messaging.ICommand
    {
        public string CurrentPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
    }
}

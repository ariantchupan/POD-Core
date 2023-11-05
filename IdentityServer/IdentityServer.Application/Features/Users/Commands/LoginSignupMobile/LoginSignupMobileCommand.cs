using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace IdentityServer.Application.Features.Users.Commands.AddUser
{
    public class LoginSignupMobileCommand : IRequest<int>
    {
        public string MobileNumber { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace IdentityServer.Application.Features.Users.Commands.AddUser
{
    public class LoginSignupMobileCommandValidator: AbstractValidator<LoginSignupMobileCommand>
    {
        public LoginSignupMobileCommandValidator()
        {

        }
    }
}

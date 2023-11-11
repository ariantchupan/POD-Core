using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer.Application.Contracts.Persistence;

using MediatR;

namespace IdentityServer.Application.Features.Users.Commands.AddUser
{
    internal class LoginSignupMobileCommandHandler : IRequestHandler<LoginSignupMobileCommand, int>
    {


       
        public async Task<int> Handle(LoginSignupMobileCommand request, CancellationToken cancellationToken)
        {
            Random random = new Random();
            int code = random.Next(9999);
            //var user = _localUserService.GetUserByUserNameAsync(request.MobileNumber).Result;
            //if (user == null)
            //{
            //    _localUserService.AddUser(new Domain.Entities.User
            //    {
            //        Active = true,
            //        Id = new Guid(),
            //        UserName = request.MobileNumber,
            //        CreatedDate = DateTime.UtcNow,
            //        SecurityCode=code.ToString(),
            //        LastModifiedDate=DateTime.UtcNow,

            //    }, code.ToString()); ;
            //}
            //else
            //{
            //    if (user.LastModifiedDate > DateTime.UtcNow.AddMinutes(2))
            //    {
            //        //code expired
            //       await _localUserService.RefreshSecurityToken(user.Id,code.ToString());
            //    }
            //    else
            //    {
            //        code = 0;
            //    }

            //}


            return code;
        }
    }
}

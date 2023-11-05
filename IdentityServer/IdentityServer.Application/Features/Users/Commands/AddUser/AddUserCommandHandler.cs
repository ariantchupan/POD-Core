using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer.Application.Contracts.Persistence;
using MediatR;

namespace IdentityServer.Application.Features.Users.Commands.AddUser
{
    internal class AddUserCommandHandler : IRequestHandler<AddUserCommand, int>
    {
        private readonly ILocalUserService _localUserService;

        public AddUserCommandHandler(ILocalUserService localUserService)
        {
            _localUserService = localUserService ?? throw new ArgumentNullException(nameof(localUserService));
        }
        public Task<int> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

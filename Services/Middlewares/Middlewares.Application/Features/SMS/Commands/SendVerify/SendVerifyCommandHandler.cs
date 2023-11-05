using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Kavenegar.Core.Models;
using MediatR;
using Middlewares.Application.Contracts.Infrastructure;

namespace Middlewares.Application.Features.SMS.Commands.SendVerify
{
    public class SendVerifyCommandHandler : IRequestHandler<SendVerifyCommand, SendResult>
    {
        private readonly IKavenegarService _kavenegarService;
        private readonly IMapper _mapper;
        public SendVerifyCommandHandler( IKavenegarService kavenegarService, IMapper mapper)
        {
                _kavenegarService = kavenegarService ?? throw new ArgumentNullException(nameof(kavenegarService));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<SendResult> Handle(SendVerifyCommand request, CancellationToken cancellationToken)
        {
            return  await _kavenegarService.VerifyLookupAsync(request.MobileNumber, request.Code);
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kavenegar.Core.Models;
using MediatR;

namespace Middlewares.Application.Features.SMS.Commands.SendVerify
{
    public class SendVerifyCommand :IRequest<SendResult>
    {
        public string MobileNumber { get; set; }
        public string Code { get; set; }

        public SendVerifyCommand(string mobileNumber,string code)
        {
            MobileNumber= mobileNumber ?? throw new ArgumentNullException(nameof(mobileNumber));
            Code = code ?? throw new ArgumentNullException(nameof(code));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middlewares.Application.Contracts.Infrastructure
{
    public interface IKavenegarService
    {
       Task<Kavenegar.Core.Models.SendResult> VerifyLookupAsync(string MobileNo, String Code);

    }
}

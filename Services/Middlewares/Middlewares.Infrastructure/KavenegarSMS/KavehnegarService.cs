using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kavenegar;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using Middlewares.Application.Contracts.Infrastructure;
using Middlewares.Application.Model.Kavenegar;
using Kavenegar.Core.Models;

namespace Middlewares.Infrastructure.KavenegarSMS
{
    public class KavehnegarService : IKavenegarService
    {
        public KavenegarSettings _kavenegarSettings { get; }

        public KavehnegarService(IOptions<KavenegarSettings> kavenegarSettings)
        {
            _kavenegarSettings = kavenegarSettings.Value;
        }

        public async Task<Kavenegar.Core.Models.SendResult> VerifyLookupAsync(string MobileNo, string Code)
        {
            var kavenegar = new KavenegarApi(_kavenegarSettings.ApiKey);
            return await kavenegar.VerifyLookup(MobileNo, Code, _kavenegarSettings.TemplateVerify);
        }
    }
}

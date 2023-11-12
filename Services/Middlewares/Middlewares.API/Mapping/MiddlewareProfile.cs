using AutoMapper;
using Middlewares.Application.Features.SMS.Commands.SendVerify;

namespace Middlewares.API.Mapping
{
    public class MiddlewareProfile :Profile
    {
        public MiddlewareProfile()
        {
            CreateMap<SendVerifyCommand,SendVerifyCommand>().ReverseMap();
        }
    }
}

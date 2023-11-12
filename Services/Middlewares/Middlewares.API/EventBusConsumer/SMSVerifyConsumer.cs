using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Middlewares.Application.Features.SMS.Commands.SendVerify;

namespace Middlewares.API.EventBusConsumer
{
    public class SMSVerifyConsumer : IConsumer<SendVerifyEvent>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public SMSVerifyConsumer(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task Consume(ConsumeContext<SendVerifyEvent> context)
        {
            //var command = _mapper.Map<SendVerifyCommand>(context.Message);
            SendVerifyCommand tmp = new SendVerifyCommand(context.Message.MobileNumber, context.Message.Code);
            var result= _mediator.Send(tmp).Result;
        }
    }
}

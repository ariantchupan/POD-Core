using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Validators;

namespace Middlewares.Application.Features.SMS.Commands.SendVerify
{
    public class SendVerifyCommandValidator : AbstractValidator<SendVerifyCommand>
    {
        public SendVerifyCommandValidator()
        {
            RuleFor(p => p.MobileNumber)
                .NotEmpty()
                .NotNull();
            RuleFor(p => p.Code)
                .NotEmpty()
                .NotNull();

        }
    }
}

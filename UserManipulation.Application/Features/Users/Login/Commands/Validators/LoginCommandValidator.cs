using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManipulation.Application.DOTs.Auth;

namespace UserManipulation.Application.Features.Users.Login.Commands.Validators
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.UserName)
                 .Must(x => !string.IsNullOrEmpty(x))
                 .WithMessage("User Name is mandatory");

            RuleFor(x => x.Password)
                .Must(x => !string.IsNullOrEmpty(x))
                .WithMessage("Password is mandatory");
        }
    }
}

using FluentValidation;
using Xamalytics.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamalytics.Application.User.Commands
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IUserService _userService;

        public CreateUserCommandValidator(IUserService userService)
        {
            _userService = userService;
        }
    }
}

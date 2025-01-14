using FluentValidation;
using Xamalytics.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamalytics.Application.User.Commands
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        private readonly IUserService _userService;

        public UpdateUserCommandValidator(IUserService userService)
        {
            _userService = userService;
        }
    }
}

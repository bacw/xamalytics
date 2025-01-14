using AutoMapper;
using MediatR.Wrappers;
using Xamalytics.Dto;
using Xamalytics.Services.Interface.Common;
using Xamalytics.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamalytics.Common;
using Xamalytics.Data;

namespace Xamalytics.Application.User.Commands
{
    public class CreateUserCommand : IRequestWrapper<UserDto>
    {
        public long Id { get; set; }
        public int? ClaimStatusId { get; set; }
        public string? Username { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Token { get; set; }

    }

    public class CreateUserCommandHandler : IRequestHandlerWrapper<CreateUserCommand, Dto.UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public CreateUserCommandHandler(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<ServiceResult<UserDto>> Handle(CreateUserCommand createUserCommand, CancellationToken cancellationToken)
        {
            var userDto = _mapper.Map<UserDto>(createUserCommand);

            await _userService.AddUser(userDto, cancellationToken);

            return ServiceResult.Success(userDto);
        }

    }
}

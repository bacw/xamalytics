using AutoMapper;
using Xamalytics.Dto;
using Xamalytics.Services.Interface.Common;
using Xamalytics.Services.Interface;
using Xamalytics.Common;

namespace Xamalytics.Application.User.Commands
{
    public class UpdateUserCommand : IRequestWrapper<UserDto>
    {
        public long Id { get; set; }
        public int? ClaimStatusId { get; set; }
        public string? Username { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? Token { get; set; }

    }

    public class UpdateUserCommandHandler : IRequestHandlerWrapper<UpdateUserCommand, Dto.UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UpdateUserCommandHandler(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<ServiceResult<UserDto>> Handle(UpdateUserCommand updateUserCommand, CancellationToken cancellationToken)
        {
            var userDto = _mapper.Map<UserDto>(updateUserCommand);

            await _userService.UpdateUser(updateUserCommand.Username, userDto, cancellationToken);

            return ServiceResult.Success(userDto);
        }

    }
}

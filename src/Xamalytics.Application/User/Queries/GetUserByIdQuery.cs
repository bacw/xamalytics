using AutoMapper;
using Xamalytics.Common;
using Xamalytics.Dto;
using Xamalytics.Services.Interface;
using Xamalytics.Services.Interface.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamalytics.Application.User.Queries
{
    public class GetUserByIdQuery : IRequestWrapper<UserDto>
    {
        public int UserId { get; set; }
    }

    public class GetUserByIdQueryHandler : IRequestHandlerWrapper<GetUserByIdQuery, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public GetUserByIdQueryHandler(IUserService userService, IMapper mapper)
        {
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<ServiceResult<UserDto>> Handle(GetUserByIdQuery getUserByIdQuery, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUser(getUserByIdQuery.UserId, cancellationToken);

            return user != null ? ServiceResult.Success(user) : ServiceResult.Failed<UserDto>(ServiceError.NotFound);
        }
    }


}

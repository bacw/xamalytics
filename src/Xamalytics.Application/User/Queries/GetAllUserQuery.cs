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
    public class GetAllUserQuery : IRequestWrapper<List<UserDto>>
    {
    }

    public class GetUserQueryHandler : IRequestHandlerWrapper<GetAllUserQuery, List<UserDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public GetUserQueryHandler(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<ServiceResult<List<UserDto>>> Handle (GetAllUserQuery getAllUserQuery, CancellationToken cancellationToken)
        {
            var userList = (await _userService.GetUsers()).ToList();

            return ServiceResult.Success(userList);
        }
    }
}

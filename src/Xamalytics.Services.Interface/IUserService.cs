using Xamalytics.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamalytics.Services.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetUsers();
        Task<UserDto> GetUser(long id, CancellationToken cancellationToken);
        Task<UserDto> AddUser(UserDto userDto, CancellationToken cancellationToken);
        Task<UserDto> GetUserByName(string userName, CancellationToken cancellationToken);
        Task<bool> UpdateUser(string userName, UserDto? userDto, CancellationToken cancellationToken);
    }
}

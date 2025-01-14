using AutoMapper;
using AutoMapper.QueryableExtensions;
using Xamalytics.Common.Exceptions;
using Xamalytics.Data;
using Xamalytics.Data.Context;
using Xamalytics.Dto;
using Xamalytics.Services.Interface;
using Xamalytics.Services.Interface.Common;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Xamalytics.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly XamalyticsContext _xamalyticsContext;
        private readonly IDateTimeService _dateTimeService;
        private readonly IMapper _mapper;
        private readonly Serilog.ILogger _logger;

        public UserService(XamalyticsContext xamalyticsContext, IDateTimeService dateTimeService, 
            IMapper mapper, Serilog.ILogger logger)
        {
            _xamalyticsContext = xamalyticsContext;
            _dateTimeService = dateTimeService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            return await _xamalyticsContext.Users.ProjectTo<UserDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<UserDto> GetUser(long id, CancellationToken cancellationToken)
        {
            var user = await _xamalyticsContext.Users
                .Where(x => x.Id == id)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            return user!;
        }

        public async Task<UserDto> GetUserByName(string userName, CancellationToken cancellationToken)
        {
            var user = await _xamalyticsContext.Users.
                Where(u => u.Username == userName).
                FirstOrDefaultAsync(cancellationToken);

            if(user != null)
            {
                return _mapper.Map<UserDto>(user);
            }
            return null!;
        }


        public async Task<UserDto> AddUser(UserDto userDto, CancellationToken cancellationToken)
        {
           
            userDto.CreatedDate = _dateTimeService.Now;

            var user = _mapper.Map<User>(userDto);
            _xamalyticsContext.Users.Add(user);
            try
            {
                await _xamalyticsContext.SaveChangesAsync(cancellationToken);
                userDto.Id = user.Id;
            }
            catch (DbUpdateException exception)
            {
                _logger.Error(exception, $"Could not add user. Details: {JsonConvert.SerializeObject(user)}");
                if (UserExists(user.Id))
                {
                    return null!;
                }

                throw;
            }

            return userDto;
        }

        public async Task<bool> UpdateUser(string userName, UserDto? userDto, CancellationToken cancellationToken)
        {
            var user = await _xamalyticsContext.Users.FirstOrDefaultAsync(u => u.Username == userName);

            if (user == null)
            {
                throw new NotFoundException(nameof(User));
            }
            user.ClaimStatusId = userDto.ClaimStatusId;
            user.Token = userDto.Token;                     

            try
            {
                _xamalyticsContext.Entry(user).State = EntityState.Modified;

                await _xamalyticsContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                _logger.Error(exception.Message, $"Could not update userDto. Details: {JsonConvert.SerializeObject(user)}");
                if (!UserExists(userName))
                {
                    return false;
                }

                throw;
            }
            catch (Exception exception)
            {
                _logger.Error(exception.Message, $"Could not update userDto. Details: {JsonConvert.SerializeObject(user)}");
                throw;
            }
        }

        private bool UserExists(string userName)
        {
            return _xamalyticsContext.Users.Any(e => e.Username == userName);
        }

        private bool UserExists(long id)
        {
            return _xamalyticsContext.Users.Any(e => e.Id == id);
        }
    }
}

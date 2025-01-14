using AutoMapper;
using AutoMapper.QueryableExtensions;
using Xamalytics.Data;
using Xamalytics.Data.Context;
using Xamalytics.Dto;
using Xamalytics.Services.Interface;
using Xamalytics.Services.Interface.Common;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;

namespace Xamalytics.Services.Implementation
{
    public class AuditUserService : IAuditUserService
    {
        private readonly XamalyticsContext _xamalyticsContext;
        private readonly IDateTimeService _dateTimeService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public AuditUserService(XamalyticsContext xamalyticsContext, IDateTimeService dateTimeService, IMapper mapper, ILogger logger)
        {
            _xamalyticsContext = xamalyticsContext;
            _dateTimeService = dateTimeService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<AuditUserDto>> GetAuditUsers()
        {
            var auditUsers = await _xamalyticsContext.AuditUsers
                                                .Include(j => j.ActivityType)
                                                .ToListAsync();

            var query = from auditUser in auditUsers
                        select new AuditUserDto
                        {
                            Id = auditUser.Id,
                            UserName = auditUser.UserName,
                            ActivityDate = auditUser.ActivityDate,
                            ActivityDetails = auditUser.ActivityDetails,
                            ActivityTypeId = auditUser.ActivityTypeId,
                            ActivityTypeDescription = auditUser.ActivityType.Name
                        };
            return query.ToList();
        }

        public async Task<AuditUserDto> GetAuditUserById(long id, CancellationToken cancellationToken)
        {
            var auditUser = await _xamalyticsContext.AuditUsers
                                               .Include(j => j.ActivityType)
                                               .Where(r => r.Id == id)
                                               .ProjectTo<AuditUserDto>(_mapper.ConfigurationProvider)
                                               .FirstOrDefaultAsync(cancellationToken);

            return auditUser!;
        }

        public async Task<AuditUserDto> AddAuditUser(AuditUserDto auditUserDto, string currentUser, CancellationToken cancellationToken)
        {
            auditUserDto.ActivityDate = _dateTimeService.Now;

            var auditUser = _mapper.Map<AuditUser>(auditUserDto);

            _xamalyticsContext.AuditUsers.Add(auditUser);
            try
            {
                await _xamalyticsContext.SaveChangesAsync(cancellationToken);
                auditUserDto.Id = auditUser.Id;
            }
            catch (DbUpdateException exception)
            {
                _logger.Error(exception.Message, $"Could not add auditUser. Details: {JsonConvert.SerializeObject(auditUser)}");               
            }
            return auditUserDto;
        }
        private bool AuditUserExists(long id)
        {
            return _xamalyticsContext.AuditUsers.Any(e => e.Id == id);
        }


    }
}

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Xamalytics.Data.Context;
using Xamalytics.Dto;
using Xamalytics.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Xamalytics.Services.Implementation
{
    public class ClaimStatusesService : IClaimStatusService
    {
        private readonly XamalyticsContext _xamalyticsContext;
        private readonly IMapper _mapper;

        public ClaimStatusesService(XamalyticsContext xamalyticsContext, IMapper mapper)
        {
            _xamalyticsContext = xamalyticsContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClaimStatusesDto>> GetClaims()
        {
            return await _xamalyticsContext.ClaimStatuses.ProjectTo<ClaimStatusesDto>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}

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

namespace Xamalytics.Application.ClaimStatus.Queries
{
    public class GetAllClaimsStatuses : IRequestWrapper<List<ClaimStatusesDto>>
    {
    }

    public class GetClaimStatusQueryHandler : IRequestHandlerWrapper<GetAllClaimsStatuses, List<ClaimStatusesDto>>
    {
        private readonly IMapper _mapper;
        private readonly IClaimStatusService _claimStatusService;

        public GetClaimStatusQueryHandler(IMapper mapper, IClaimStatusService claimStatusService)
        {
            _mapper = mapper;
            _claimStatusService = claimStatusService;
        }

        public async Task<ServiceResult<List<ClaimStatusesDto>>> Handle(GetAllClaimsStatuses getAllClaimsStatuses , CancellationToken cancellationToken)
        {
            var claimStatusesList = (await _claimStatusService.GetClaims()).ToList();

            return ServiceResult.Success(claimStatusesList);
        }
    }
}

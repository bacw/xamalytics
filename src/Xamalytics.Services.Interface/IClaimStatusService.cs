using Xamalytics.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamalytics.Services.Interface
{
    public interface IClaimStatusService
    {
        Task<IEnumerable<ClaimStatusesDto>> GetClaims();
    }
}

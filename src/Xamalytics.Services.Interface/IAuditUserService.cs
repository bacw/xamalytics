using Xamalytics.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamalytics.Services.Interface
{
    public interface IAuditUserService
    {
        Task<IEnumerable<AuditUserDto>> GetAuditUsers();
        Task<AuditUserDto> GetAuditUserById(long Id, CancellationToken cancellationToken);
        Task<AuditUserDto> AddAuditUser(AuditUserDto auditUserDto, string currentUser, CancellationToken cancellationToken);
    }
}

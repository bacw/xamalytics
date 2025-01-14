#nullable disable

using AutoMapper;
using Xamalytics.Application.Request.Commands;
using Xamalytics.Application.User.Commands;
using Xamalytics.Data;
using Xamalytics.Dto;
using Newtonsoft.Json;

namespace Xamalytics.Api.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AuditUser, AuditUserDto>().ReverseMap();
            CreateMap<Request, RequestDto>().ReverseMap();
            CreateMap<Request, Request>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<ClaimStatuses, ClaimStatusesDto>().ReverseMap();
            
            //CreateCommand Mappings
            CreateMap<CreateRequestCommand, RequestDto>();
            CreateMap<CreateUserCommand, UserDto>();

            //UpdateCommand Mappings
            CreateMap<UpdateRequestCommand, RequestDto>();
            CreateMap<UpdateUserCommand, UserDto>();
        }
    }
}
using FluentValidation;
using Xamalytics.Data.Context;
using Xamalytics.Services.Interface;

namespace Xamalytics.Application.Request.Commands
{
    public class CreateRequestCommandValidator : AbstractValidator<CreateRequestCommand>
    {
        private readonly IRequestService _requestService;

        public CreateRequestCommandValidator(IRequestService requestService)
        {
            _requestService = requestService;
        }
    }
}

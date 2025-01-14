using FluentValidation;
using Xamalytics.Data.Context;

namespace Xamalytics.Application.Request.Commands
{
    public class UpdateRequestCommandValidator : AbstractValidator<UpdateRequestCommand>
    {
        private readonly XamalyticsContext _context;
        public UpdateRequestCommandValidator(XamalyticsContext context)
        {
            _context = context;
        }
    }
}

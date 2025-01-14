using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Xamalytics.Common.Exceptions
{
    public class ValidateModelStateDictionary : ModelStateDictionary
    {

        public ValidateModelStateDictionary()
        {
            Errors = new Dictionary<string, List<string>>();
        }


        public ValidateModelStateDictionary(ModelStateDictionary modelState)
            : this()
        {
            foreach (var key in modelState.Keys)
            {
                var property = modelState.GetValueOrDefault(key);

                var errors = property?.Errors.Select(error => error.ErrorMessage).ToList();

                Errors.Add(key, errors ?? new List<string>());
            }
        }

        public IDictionary<string, List<string>> Errors { get; }
    }
}

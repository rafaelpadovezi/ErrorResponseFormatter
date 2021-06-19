using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace ErrorResponseFormatter
{
    public static class ModelStateConverter
    {
        public static ValidationErrorResponse Convert(ModelStateDictionary modelState)
        {
            var response = new ValidationErrorResponse();
            foreach (var (key, value) in modelState)
                response.Error.Add(key, value.Errors.Select(e => e.ErrorMessage).ToArray());

            return response;
        }
    }
}
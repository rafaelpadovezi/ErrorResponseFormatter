using System.Collections.Generic;

namespace ErrorResponseFormatter
{
    public class ValidationErrorResponse
    {
        public IDictionary<string, string[]> Error { get; set; } = new Dictionary<string, string[]>();
        public string Type => "VALIDATION_ERRORS";
    }
}
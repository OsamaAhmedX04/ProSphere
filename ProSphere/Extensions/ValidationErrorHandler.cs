using FluentValidation.Results;

namespace ProSphere.Extensions
{
    public static class ValidationErrorHandler
    {
        public static Dictionary<string, List<string>> ConvertErrorsToDictionary(this ValidationResult result)
        {
            return result.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    e => e.Key,
                    e => e.Select(d => d.ErrorMessage).ToList());
        }
    }
}

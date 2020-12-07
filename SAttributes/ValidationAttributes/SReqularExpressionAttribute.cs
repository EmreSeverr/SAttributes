using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SAttributes.ValidationAttributes
{
    public sealed class SReqularExpressionAttribute : ValidationAttribute
    {
        public bool IsRequired { get; set; } = true;

        private readonly string _RegexPattern;
        public SReqularExpressionAttribute(string regexPattern)
        {
            _RegexPattern = regexPattern;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var httpContext = validationContext.GetRequiredService<IHttpContextAccessor>().HttpContext;

            if (value == null || value.Equals(""))
            {
                if (IsRequired)
                {
                    httpContext.Items[validationContext.MemberName] = ErrorMessage;
                    return new ValidationResult(FormatErrorMessage(""));
                }
            }
            else
            {
                var val = value.ToString();

                var isCorrect = Regex.IsMatch(val, _RegexPattern);

                if (!isCorrect)
                {
                    httpContext.Items[validationContext.MemberName] = ErrorMessage;
                    return new ValidationResult(FormatErrorMessage(""));
                }
            }

            return ValidationResult.Success;
        }
    }
}

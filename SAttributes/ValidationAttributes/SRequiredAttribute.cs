using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;

namespace SAttributes.ValidationAttributes
{
    public class SRequiredAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var httpContext = validationContext.GetRequiredService<IHttpContextAccessor>().HttpContext;

            if (value == null || value.Equals(""))
            {
                httpContext.Items[validationContext.MemberName] = ErrorMessage;
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}

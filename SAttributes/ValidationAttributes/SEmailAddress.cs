using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;

namespace SAttributes.ValidationAttributes
{
    public class SEmailAddress : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var httpContext = validationContext.GetRequiredService<IHttpContextAccessor>().HttpContext;

            if(value != null)
            {
                var objectType = value.GetType();

                if (!objectType.IsAssignableFrom(typeof(string))) throw new InvalidOperationException("This attribute can only be used in data types that contain string.");

                String val = value.ToString();

                if (val.Length > 0) 
                {
                    if (!val.Contains("@"))
                    {
                        httpContext.Items[validationContext.MemberName] = ErrorMessage;
                        return new ValidationResult(ErrorMessage);
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}

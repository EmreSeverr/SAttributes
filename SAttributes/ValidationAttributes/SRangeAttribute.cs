using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SAttributes.ValidationAttributes
{
    public sealed class SRangeAttribute : ValidationAttribute
    {
        private readonly int _MinValue;
        private readonly int _MaxValue;

        public SRangeAttribute(int minValue, int maxValue)
        {
            _MinValue = minValue;
            _MaxValue = maxValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var httpContext = validationContext.GetRequiredService<IHttpContextAccessor>().HttpContext;

            var objectType = value.GetType();

            if (objectType.IsAssignableFrom(typeof(decimal)) || objectType.IsAssignableFrom(typeof(int)) || objectType.IsAssignableFrom(typeof(float)) || objectType.IsAssignableFrom(typeof(double))) { }
            else throw new InvalidOperationException("This attribute can only be used in data types that contain numbers.");
            
            if (value == null || value.Equals(""))
            {
                httpContext.Items[validationContext.MemberName] = ErrorMessage;
                return new ValidationResult(FormatErrorMessage(""));
            }
            else
            {
                var number = Convert.ToInt32(value);

                if (number < _MinValue)
                {
                    httpContext.Items[validationContext.MemberName] = ErrorMessage;
                    return new ValidationResult(FormatErrorMessage(""));
                }
                if (number > _MaxValue)
                {
                    httpContext.Items[validationContext.MemberName] = ErrorMessage;
                    return new ValidationResult(FormatErrorMessage(""));
                }
            }

            return ValidationResult.Success;
        }
    }
}

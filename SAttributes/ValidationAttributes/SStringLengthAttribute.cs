using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SAttributes.ValidationAttributes
{
    public sealed class SStringLengthAttribute : ValidationAttribute
    {
        private readonly int _MinValue;
        private readonly int _MaxValue;

        public SStringLengthAttribute(int minValue, int maxValue)
        {
            _MaxValue = maxValue;
            _MinValue = minValue;
        }

        public SStringLengthAttribute(int maxValue)
        {
            _MaxValue = maxValue;
            _MinValue = 0;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var httpContext = validationContext.GetRequiredService<IHttpContextAccessor>().HttpContext;

            if (value == null || value.Equals(""))
            {
                httpContext.Items[validationContext.MemberName] = ErrorMessage;
                return new ValidationResult(FormatErrorMessage(""));
            }
            else
            {
                var objectType = value.GetType();

                if (objectType.IsAssignableFrom(typeof(string))) { }
                else throw new InvalidOperationException("This attribute is only available for data types that contain characters.");

                var val = value.ToString();

                if (val.Length < _MinValue)
                {
                    httpContext.Items[validationContext.MemberName] = ErrorMessage;
                    return new ValidationResult(FormatErrorMessage(""));
                }
                if (val.Length > _MaxValue)
                {
                    httpContext.Items[validationContext.MemberName] = ErrorMessage;
                    return new ValidationResult(FormatErrorMessage(""));
                }
            }

            return ValidationResult.Success;
        }
    }
}

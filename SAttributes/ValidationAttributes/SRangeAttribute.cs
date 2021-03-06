﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;

namespace SAttributes.ValidationAttributes
{
    public class SRangeAttribute : ValidationAttribute
    {
        private readonly int _MinValue;
        private readonly int? _MaxValue;

        public SRangeAttribute(int minValue)
        {
            _MinValue = minValue;
            _MaxValue = null;
        }

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
            
            if (value == null)
            {
                httpContext.Items[validationContext.MemberName] = ErrorMessage;
                return new ValidationResult(ErrorMessage);
            }
            else
            {
                var number = Convert.ToInt32(value);

                if (number < _MinValue)
                {
                    httpContext.Items[validationContext.MemberName] = ErrorMessage;
                    return new ValidationResult(ErrorMessage);
                }
                if (_MaxValue != null)
                {
                    if (number > _MaxValue)
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

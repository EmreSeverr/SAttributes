﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SAttributes.ValidationAttributes
{
    public class SVerifyIdAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var httpContext = validationContext.GetRequiredService<IHttpContextAccessor>().HttpContext;

            if (value == null)
            {
                httpContext.Items[validationContext.MemberName] = ErrorMessage;
                return new ValidationResult(ErrorMessage);
            }
            else
            {
                var objectType = value.GetType();

                if (objectType.IsAssignableFrom(typeof(Guid)) || objectType.IsAssignableFrom(typeof(int))) { }
                else throw new InvalidOperationException("This attribute can only be used in data types that contain numbers.");


                if (objectType.IsAssignableFrom(typeof(Guid)))
                {
                    var guidRegex = @"(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}";

                    var isCorrect = Regex.IsMatch(value.ToString(), guidRegex);

                    if (!isCorrect)
                    {
                        httpContext.Items[validationContext.MemberName] = ErrorMessage;
                        return new ValidationResult(ErrorMessage);
                    }
                }

                if (objectType.IsAssignableFrom(typeof(int)))
                {
                    var id = Convert.ToInt32(value);

                    if (id <= 0) 
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

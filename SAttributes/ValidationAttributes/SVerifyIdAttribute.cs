using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace SAttributes.ValidationAttributes
{
    public sealed class SVerifyIdAttribute : ValidationAttribute
    {
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

                if (objectType.IsAssignableFrom(typeof(Guid)) || objectType.IsAssignableFrom(typeof(int))) { }
                else throw new InvalidOperationException("Bu attribute sadece id tutan veri tiplerinde geçerlidir.");


                if (objectType.Name.ToLower() == "guid")
                {
                    var guidRegex = @"(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}";

                    var isCorrect = Regex.IsMatch(value.ToString(), guidRegex);

                    if (!isCorrect)
                    {
                        httpContext.Items[validationContext.MemberName] = ErrorMessage;
                        return new ValidationResult(FormatErrorMessage(""));
                    }
                }

                if (objectType.Name.ToLower() == "int")
                {
                    var id = Convert.ToInt32(value);

                    if (id <= 0) 
                    {
                        httpContext.Items[validationContext.MemberName] = ErrorMessage;
                        return new ValidationResult(FormatErrorMessage(""));
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}

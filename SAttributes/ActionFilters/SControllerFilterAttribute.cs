using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SAttributes.Exceptions;
using SAttributes.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAttributes.ActionFilters
{
    public class SControllerFilterAttribute : ActionFilterAttribute
    {
        public String RequirementDisabledProperties { get; set; }

        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                var httpContext = actionContext.HttpContext;

                IEnumerable<ModelError> modelErrors = actionContext.ModelState.Values.SelectMany(v => v.Errors);

                var errors = new HashSet<string> { };

                foreach (var item in modelErrors)
                    errors.Add(item.ErrorMessage);

                var properties = GetProperties(RequirementDisabledProperties);

                if (!properties.IsNullOrEmpty())
                    foreach (var props in properties)
                        if (httpContext.Items[props] != null)
                            errors.Remove(httpContext.Items[props].ToString());

                if (!errors.IsNullOrEmpty())
                    throw new SAttributesException(string.Join(",", errors));
            }
        }

        private List<string> GetProperties(string props)
        {
            string[] prop = null;

            if (!string.IsNullOrEmpty(props))
            {
                prop = props.Split(',');

                for (int i = 0; i < prop.Length; i++)
                {
                    prop[i] = prop[i].Trim();
                }
            }

            return prop.IsNullOrEmpty() ? null : prop.ToList();
        }
    }
}

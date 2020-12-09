using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Collections;

namespace SAttributes.Extensions
{
    public static class SAttributesExtensions
    {
        public static void ConfigureSAttributes(this IServiceCollection service)
        {
            service.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            service.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        internal static bool IsNullOrEmpty(this IEnumerable @this)
        {
            return @this == null || @this.GetEnumerator().MoveNext() == false;
        }
    }
}

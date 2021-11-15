using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Collections;

namespace SAttributes.Extensions
{
    public static class SAttributesExtensions
    {
        public static void ConfigureSAttributes(this IServiceCollection service)
        {
            service.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            service.AddHttpContextAccessor();
        }

        internal static bool IsNullOrEmpty(this IEnumerable @this) => @this == null || @this.GetEnumerator().MoveNext() == false;
    }
}

using Edu.Svc.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edu.Api.Infrastructure.Iocs
{
    public class ServiceResolver
    {
        public  static T GetService<T>(HttpContext context)
        {
            var serviceProvidersFeature = context.Features.Get<IServiceProvidersFeature>();
            var services = serviceProvidersFeature.RequestServices;
            var service = (T)services.GetService(typeof(T));
            return service;
        }

        public static T GetService<T>(ActionExecutingContext context)
        {
            var serviceProvidersFeature = context.HttpContext.Features.Get<IServiceProvidersFeature>();
            var services = serviceProvidersFeature.RequestServices;
            var service = (T)services.GetService(typeof(T));
            return service;
        }        
        
        public static T GetService<T>(ExceptionContext context)
        {
            var serviceProvidersFeature = context.HttpContext.Features.Get<IServiceProvidersFeature>();
            var services = serviceProvidersFeature.RequestServices;
            var service = (T)services.GetService(typeof(T));
            return service;
        }

    }
}

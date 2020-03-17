using Atom.Lib;
using Atom.Logger;
using Edu.Api.Infrastructure.Iocs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edu.Api.Infrastructure.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {

        public void OnException(ExceptionContext context)
        {
            var errorBr = new Br<string>(context.Exception.Message,-1,context.Exception.Message,stackTrance:context.Exception.StackTrace);
            context.Result = new JsonResult(errorBr);
            context.ExceptionHandled = true;

            IALogger logger = ServiceResolver.GetService<IALogger>(context);
            logger.Exception(context.Exception);
        }
    }
}

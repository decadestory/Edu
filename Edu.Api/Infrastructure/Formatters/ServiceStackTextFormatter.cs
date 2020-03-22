using Microsoft.AspNetCore.Mvc.Formatters;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Edu.Api.Infrastructure.Formatters
{
    public class ServiceStackTextInputFormatter : IInputFormatter
    {
        public bool CanRead(InputFormatterContext context)
        {
            return true;
        }

        public Task<InputFormatterResult> ReadAsync(InputFormatterContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var request = context.HttpContext.Request;
            var reader = context.ReaderFactory(request.Body, Encoding.UTF8);
            var scope = JsConfig.BeginScope();
            scope.DateHandler = DateHandler.ISO8601;
            var result = JsonSerializer.DeserializeFromReader(reader, context.ModelType);
            scope.Dispose();
            reader.Dispose();
            return InputFormatterResult.SuccessAsync(result);

        }
    }

    public class ServiceStackTextOutputFormatter : IOutputFormatter
    {

        public bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            return true;
        }

        public Task WriteAsync(OutputFormatterWriteContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = "application/json";

            if (context.Object == null)
            {
                response.Body.WriteByte(192);
                return Task.CompletedTask;
            }

            using (var writer = context.WriterFactory(response.Body, Encoding.UTF8))
            {
                JsonSerializer.SerializeToWriter(context.Object, writer);
                return  Task.CompletedTask;
            }

        }
    }


}

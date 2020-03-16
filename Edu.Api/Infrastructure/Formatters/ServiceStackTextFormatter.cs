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
    public class ServiceStackTextFormatter : MediaTypeFormatter,IInputFormatter
    {
        //Uses ISO8601 date by default
        private DateHandler _dateHandler = DateHandler.ISO8601;

        public ServiceStackTextFormatter(DateHandler dateHandler): this()
        {
            _dateHandler = dateHandler;
        }

        public ServiceStackTextFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
            SupportedEncodings.Add(new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true));
        }

        public override Task<object> ReadFromStreamAsync(Type type, System.IO.Stream stream, HttpContent content, IFormatterLogger formatterLogger)
        {
            return Task.Factory.StartNew(() =>
            {
                using (var scope = JsConfig.BeginScope())
                {
                    scope.DateHandler = _dateHandler;
                    JsConfig.DateHandler = _dateHandler;
                    var result = JsonSerializer.DeserializeFromStream(type, stream);
                    return result;
                }
            });
        }

        public override Task WriteToStreamAsync(Type type, object value, System.IO.Stream stream, HttpContent content, TransportContext transportContext)
        {
            return Task.Factory.StartNew(() =>
            {
                using (var scope = JsConfig.BeginScope())
                {
                    scope.DateHandler = _dateHandler;
                    JsonSerializer.SerializeToStream(value, type, stream);
                }
            });
        }

        public override bool CanReadType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            return true;
        }

        public override bool CanWriteType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            return true;
        }

        public bool CanRead(InputFormatterContext context)
        {
            throw new NotImplementedException();
        }

        public Task<InputFormatterResult> ReadAsync(InputFormatterContext context)
        {
            throw new NotImplementedException();
        }
    }
}

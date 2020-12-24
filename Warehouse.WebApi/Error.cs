using System;
using System.Collections;
using System.Net;

namespace Warehouse.WebApi
{
    public class Error
    {
        public HttpStatusCode Code { get; set; }

        public string Reason { get; set; }

        public object Details { get; set; }

        public static Error Unknown => new Error
        {
            Code = HttpStatusCode.InternalServerError,
            Reason = "Unknown"
        };

        public static Error WarehouseNotFound400 => new Error
        {
            Code = HttpStatusCode.BadRequest,
            Reason = "Warehouse not found"
        };

        public static Error WarehouseNotFound404 => new Error
        {
            Code = HttpStatusCode.NotFound,
            Reason = "Warehouse not found"
        };

        public static Error TerminalNotFound404 => new Error
        {
            Code = HttpStatusCode.NotFound,
            Reason = "Terminal not found"
        };

        public static Error TerminalAlreadyExists => new Error
        {
            Code = HttpStatusCode.Conflict,
            Reason = "Terminal already exists"
        };

        public static Error Interpret(Exception exception, bool showDetails)
        {
            var error = interpret(exception);
            if (showDetails)
            {
                error.Details = new
                {
                    exceptionMessage = exception.Message,
                    exceptionData = any(exception.Data) ? exception.Data : null,
                    innerExceptionMessage = exception.InnerException?.Message,
                    stackTrace = exception.StackTrace
                };
            }

            return error;
        }

        private static Error interpret(Exception exception)
        {
            return Unknown;
        }

        private static bool any(IEnumerable enumerable)
        {
            var enumerator = enumerable.GetEnumerator();
            return enumerator.MoveNext();
        }
    }
}
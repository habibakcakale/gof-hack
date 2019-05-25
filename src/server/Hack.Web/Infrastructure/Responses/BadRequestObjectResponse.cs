using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hack.Web
{
    public sealed class BadRequestObjectResponse<T> : BadRequestObjectResult, IObjectResponse<T>
    {
        private const int DefaultStatusCode = StatusCodes.Status400BadRequest;

        public new T Value { get; }

        public BadRequestObjectResponse(T value) : base(value)
        {
            Value = value;
            StatusCode = DefaultStatusCode;
        }
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hack.Web
{
    public sealed class OkObjectResponse<T> : OkObjectResult, IObjectResponse<T>
    {
        private const int DefaultStatusCode = StatusCodes.Status200OK;
        public new T Value { get; }

        public OkObjectResponse(T value) : base(value)
        {
            Value = value;
            StatusCode = DefaultStatusCode;
        }
    }
}
using System.Net;

namespace CongestionTaxCalculator.Service.Extensions.CustomExceptions
{
    public sealed class ServiceException(HttpStatusCode _statusCode, string _message) : Exception
    {
        public HttpStatusCode StatusCode => _statusCode;
        public new string Message => _message;
    }

}

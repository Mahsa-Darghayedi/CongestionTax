using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Service.Extensions.CustomExceptions;

public sealed record Exceptions(HttpStatusCode StatusCode, string Message)
{

    public static readonly Exceptions BadRequestException = new(HttpStatusCode.BadRequest, "The request is not valid.");
    public static readonly Exceptions InternalErrorException = new(HttpStatusCode.InternalServerError, "An error has occurred.");
    public static readonly Exceptions ItemNotFoundException = new(HttpStatusCode.NotFound, "The selected item is not valid.");
    public static readonly Exceptions TaxFreeException = new(HttpStatusCode.NotFound, "The selected vehicle is tax free.");

    public static implicit operator ServiceException(Exceptions error) => new(error.StatusCode, error.Message);
};




using Microsoft.VisualBasic;
using System;
using System.Linq.Expressions;
using System.Net;

namespace CongestionTaxCalculator.Service.Extensions.CustomExceptions;

public static class ExceptionHandler
{

    public static void ThrowIfNull(this Exceptions exception, object? obj)
    {
        if (CheckInputArgument(obj))
            throw exception;
    }

    public static void ThrowIf(this Exceptions exception, bool condition)
    {
        if (condition)
            throw exception;
    }

    private static bool CheckInputArgument(object? argument) =>
        argument is null
        || argument is string str && string.IsNullOrWhiteSpace(str)
        || argument is bool boolResult && !boolResult
        || argument is int digit && digit <= 0
        || argument is Collection lst && lst.Count == 0
        || argument is byte[] bt && bt.Length == 0
        || argument is Array arr && arr.Length == 0
    ;

}
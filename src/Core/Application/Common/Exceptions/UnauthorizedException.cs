using System.Net;

namespace NodeClutchGateway.Application.Common.Exceptions;

public class UnauthorizedException : CustomException
{
    public UnauthorizedException(string message)
       : base(message, null, HttpStatusCode.Unauthorized)
    {
    }
}
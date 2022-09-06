using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.Api.Infrastructure.Exceptions
{
    public class ApiDomainException : Exception
    {
        public ApiDomainException()
        { }

        public ApiDomainException(string message)
            : base(message)
        { }

        public ApiDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}

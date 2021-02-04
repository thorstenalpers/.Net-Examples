using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Examples.MediatR.Core.Requests
{
    public class SomeRequest : IRequest<bool>
    {
        public SomeRequest(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}

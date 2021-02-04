﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Requests
{
    public class SomeOneWayRequest : IRequest
    {
        public SomeOneWayRequest(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}

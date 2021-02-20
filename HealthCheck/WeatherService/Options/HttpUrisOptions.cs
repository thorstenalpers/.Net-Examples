using System;
using System.Collections.Generic;

namespace Tutorials.HealthCheck.WeatherService.Options
{
    public class HttpUrisOptions
    {
        public const string SECTION_NAME = "HttpUris";
        public IList<Uri> HttpUris { get; set; }

    }
}

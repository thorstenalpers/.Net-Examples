using System;
using System.Collections.Generic;

namespace Examples.HealthCheck.WeatherService.Options
{
	public class HttpUrisOptions
    {
        public const string SECTION_NAME = "HttpUris";
        public IList<Uri> Uris { get; set; }
    }
}
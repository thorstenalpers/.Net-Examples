namespace Tutorials.HealthCheck.WeatherService.Options
{
    public class HealthCheckOptions
    {
        public const string SECTION_NAME = "HealthCheck";

        public string ApiPathReadiness { get; set; }
        public string ApiPathLiveness { get; set; }
        public string UiPath { get; set; }

        public HttpUrisOptions RemoteDependencies { get; set; }
    }
}

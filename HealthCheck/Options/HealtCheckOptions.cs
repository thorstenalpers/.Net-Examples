namespace TutorialHealthCheck.Options
{
    using System;
    using System.Collections.Generic;

    public class HealtCheckOptions
    {
        public const string SECTION_NAME = "HealthCheck";

        public string ApiPathReadiness { get; set; }
        public string ApiPathLiveness { get; set; }
        public string UiPath { get; set; }

        public IList<Uri> RemoteDependencies { get; set; }
    }
}

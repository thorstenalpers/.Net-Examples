using Examples.HealthCheck.WeatherService.CustomHealthChecks;
using Examples.HealthCheck.WeatherService.Models;
using Examples.HealthCheck.WeatherService.Options;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;

namespace Examples.HealthCheck.WeatherService
{
	public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var hcOptions = new Options.HealthCheckOptions();
            Configuration.GetSection(Options.HealthCheckOptions.SECTION_NAME).Bind(hcOptions);

            services.Configure<Options.HealthCheckOptions>(Configuration.GetSection(Options.HealthCheckOptions.SECTION_NAME));
            services.Configure<HttpUrisOptions>(Configuration.GetSection(Options.HealthCheckOptions.SECTION_NAME).GetSection(HttpUrisOptions.SECTION_NAME));
            services.Configure<ExternalWeatherApiOptions>(myOptions =>
            {
                myOptions.Uri = Configuration.GetValue<Uri>(ExternalWeatherApiOptions.KEY_NAME);
            });

			services.AddControllers();
            services.AddHttpClient();
			services.AddDbContext<MyDbContext>();


			services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WeatherForecast Service", Version = "v1" });
            });

            var hcBuilder = services.AddHealthChecks();
            if (hcOptions?.RemoteDependencies?.Uris?.Any() ?? false)
            {
                hcBuilder.AddCheck<HttpHealthCheck>(nameof(hcOptions.RemoteDependencies), tags: new[] { nameof(EHealthCheckType.READINESS) });
            }
			hcBuilder.AddSqlite("sqlite");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<Options.HealthCheckOptions> hcOptions)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "sample_healthcheck v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks(hcOptions.Value.ApiPathReadiness, new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
                {
                    AllowCachingResponses = false,
                    Predicate = (check) => check.Tags.Contains(nameof(EHealthCheckType.READINESS)),
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecks(hcOptions.Value.ApiPathLiveness, new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
                {
                    AllowCachingResponses = false,
                    Predicate = (check) => check.Tags.Contains(nameof(EHealthCheckType.READINESS)) || check.Tags.Contains(nameof(EHealthCheckType.LIVENESS)),
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
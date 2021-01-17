using HealthChecks.UI.Client;
using k8s;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Linq;
using Tutorials.HealthCheck.Options;

namespace Tutorials.HealthCheck
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            CustomHealthCheckOptions hcOptions = new CustomHealthCheckOptions();
            Configuration.GetSection(CustomHealthCheckOptions.SECTION_NAME).Bind(hcOptions);

            services.Configure<CustomHealthCheckOptions>(Configuration.GetSection(CustomHealthCheckOptions.SECTION_NAME));
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tutorial Healthcheck", Version = "v1" });
            });

            var hcBuilder = services.AddHealthChecks();
            if (hcOptions?.RemoteDependencies?.Any() ?? false)
            {
                hcBuilder.AddCheck<HttpHealthCheck>(nameof(hcOptions.RemoteDependencies), tags: new[] { nameof(EHealthCheckType.READINESS), nameof(EHealthCheckType.LIVENESS) });
            }
            hcBuilder.AddKubernetes(setup =>
            {
                setup.WithConfiguration(new KubernetesClientConfiguration
                {
                    Host = "https://localhost:443",
                    SkipTlsVerify = true
                })
                .CheckDeployment("wordpress-one-wordpress", d => d.Status.Replicas == 2 && d.Status.ReadyReplicas == 2)
                .CheckService("DummyService", s => s.Spec.Type == "LoadBalancer")
                .CheckPod("myapp-pod", p => p.Metadata.Labels["app"] == "myapp"); ;
            }, tags: new[] { nameof(EHealthCheckType.LIVENESS) });


            services.AddHealthChecksUI(setupSettings: setup =>
            {
                setup.AddHealthCheckEndpoint("Readiness", hcOptions.ApiPathReadiness);
                setup.AddHealthCheckEndpoint("Liveness", hcOptions.ApiPathLiveness);
            }).AddInMemoryStorage();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<CustomHealthCheckOptions> hcOptions)
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

                   endpoints.MapHealthChecks(hcOptions.Value.ApiPathReadiness, new HealthCheckOptions
                   {
                       AllowCachingResponses = false,
                       Predicate = (check) => check.Tags.Contains(nameof(EHealthCheckType.READINESS)),
                       ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                   });
                   endpoints.MapHealthChecks(hcOptions.Value.ApiPathLiveness, new HealthCheckOptions
                   {
                       AllowCachingResponses = false,
                       Predicate = (check) => check.Tags.Contains(nameof(EHealthCheckType.READINESS)) || check.Tags.Contains(nameof(EHealthCheckType.LIVENESS)),
                       ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                   });

                   endpoints.MapHealthChecksUI(setup =>
                   {
                       setup.UIPath = hcOptions.Value.UiPath;
                   });

                   endpoints.MapDefaultControllerRoute();
               });
        }
    }
}

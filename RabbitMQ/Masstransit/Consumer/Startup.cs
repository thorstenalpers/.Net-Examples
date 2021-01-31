using Masstransit.Consumer.Consumers;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Masstransit.Consumer
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMassTransit(x =>
			{
				x.AddConsumer<MessageReceivedConsumer>();
				x.SetKebabCaseEndpointNameFormatter();
				x.UsingRabbitMq((context, cfg) =>
				{
					cfg.Host("localhost", "/", h =>
					{
						h.Username("Admin");
						h.Password("Password");
					});
					cfg.ConfigureEndpoints(context);

					//cfg.ReceiveEndpoint("messages-queue1", e =>
					//{
					//    e.Consumer<MessageConsumer>();
					//});
				});
			});

			//services.AddSingleton<MessageReceivedConsumer>();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();
		}
	}
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EasyNetQ.Consumer
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
			//services.AddMassTransit(x =>
			//{
			//    x.AddConsumer<MessageReceivedConsumer>();
			//    x.AddConsumer<GetAllMessageConsumer>();
			//    x.AddConsumersFromNamespaceContaining<MessageReceivedConsumer>();

			//    x.UsingRabbitMq((context, cfg) =>
			//    {
			//        //cfg.ReceiveEndpoint("messages-queue1", e =>
			//        //{
			//        //    e.Consumer<MessageConsumer>();
			//        //});
			//        //cfg.ReceiveEndpoint("messages-queue2", e =>
			//        //{
			//        //    e.Consumer<GetAllMessageConsumer>();
			//        //});
			//    });
			//});

			//services.AddMassTransitHostedService();
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

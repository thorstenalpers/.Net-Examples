namespace Examples.RabbitMQ.Consumer
{
	using Examples.RabbitMQ.Consumer.Consumers;
	using MassTransit;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;
	using System;

	class Program
	{
		static void Main(string[] args)
		{
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledExceptionHandler);

			var builder = Host.CreateDefaultBuilder()
				.ConfigureServices((hostContext, services) =>
				{
					services.AddMassTransit(cfg =>
					{
						cfg.AddConsumer<SomeEventReceivedHandler>()
							.Endpoint(e =>
							{

								//e.
								// override the default endpoint name
								//e.Name = "order-service-extreme";

								// specify the endpoint as temporary (may be non-durable, auto-delete, etc.)
								e.Temporary = false;

								// specify an optional concurrent message limit for the consumer
								e.ConcurrentMessageLimit = 8;

								// only use if needed, a sensible default is provided, and a reasonable
								// value is automatically calculated based upon ConcurrentMessageLimit if 
								// the transport supports it.
								e.PrefetchCount = 16;

								// set if each service instance should have its own endpoint for the consumer
								// so that messages fan out to each instance.
								//e.InstanceId = "something-unique";
							}

							);

						cfg.AddBus(provider =>
						{
							return Bus.Factory.CreateUsingRabbitMq(cfg =>
							{
								cfg.Host(host: "localhost", virtualHost: "Masstransit", h =>
								{
									h.Username("admin");
									h.Password("password");
								});
								cfg.ReceiveEndpoint("EventReceived", e =>
								{
									e.Consumer<SomeEventReceivedHandler>(provider);
								});
							});

						});
					});

					services.AddHostedService<MassTransitHostedService>();

					services.AddSingleton<SomeEventReceivedHandler>();
				});

			builder.Build().Run();
		}

		static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs args)
		{
			Exception exception = (Exception)args.ExceptionObject;
			Console.WriteLine("MyHandler caught : " + exception.Message);
			Console.WriteLine("Runtime terminating: {0}", args.IsTerminating);
		}
	}
}
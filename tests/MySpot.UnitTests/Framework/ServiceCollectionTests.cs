using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shouldly;

namespace MySpot.UnitTests.Framework;

public class ServiceCollectionTests
{
	[Fact]
	public void test()
	{
		var serviceCollection = new ServiceCollection();
		//	serviceCollection.AddTransient<IMessenger, Messenger>();
		//	serviceCollection.AddScoped<IMessenger, Messenger>();
		//	serviceCollection.AddScoped<IMessenger, Messenger2>();
		serviceCollection.AddSingleton<IMessenger, Messenger>();
		
		ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

		
		using IServiceScope scope = serviceProvider.CreateScope();
		{
			var messenger = scope.ServiceProvider.GetRequiredService<IMessenger>();
			messenger.Send();
			var messenger2 = scope.ServiceProvider.GetRequiredService<IMessenger>();
			messenger2.Send();

			messenger.ShouldNotBeNull();
			messenger.ShouldBeOfType<Messenger>();
			messenger.ShouldBe(messenger2);
		}

		using IServiceScope scope2 = serviceProvider.CreateScope();
		{
			var messenger = scope2.ServiceProvider.GetRequiredService<IMessenger>();
			messenger.Send();
			var messenger2 = scope2.ServiceProvider.GetRequiredService<IMessenger>();
			messenger2.Send();

			messenger.ShouldNotBeNull();
			messenger.ShouldBeOfType<Messenger>();
			messenger.ShouldBe(messenger2);
		}
		
		//	Get all services
		var messengers = serviceProvider.GetServices<IMessenger>();
		
	}
	
	private interface IMessenger
	{
		void Send();
	}
	
	private class Messenger : IMessenger
	{
		private readonly Guid _id = Guid.NewGuid();
		
		public void Send()
		{
			Console.WriteLine($"Sending message... [{_id}]");
		}
	}
	
	private class Messenger2 : IMessenger
	{
		private readonly Guid _id = Guid.NewGuid();
		
		public void Send()
		{
			Console.WriteLine($"Sending message... [{_id}]");
		}
	}

}
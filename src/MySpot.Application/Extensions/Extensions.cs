using Microsoft.Extensions.DependencyInjection;

namespace MySpot.Application.Extensions;

public static class Extensions
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		return services;
	}
}
using Microsoft.Extensions.DependencyInjection;

namespace MySpot.Core.Extensions;

public static class Extensions
{
	public static IServiceCollection AddCore(this IServiceCollection services)
	{
		return services;
	}
}
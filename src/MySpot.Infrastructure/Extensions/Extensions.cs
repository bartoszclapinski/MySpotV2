using Microsoft.Extensions.DependencyInjection;
using MySpot.Application.Services;
using MySpot.Infrastructure.Time;

namespace MySpot.Infrastructure.Extensions;

public static class Extensions
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services)
	{
		services
			.AddScoped<IClock, Clock>()
			.AddScoped<IReservationService, ReservationService>();

		return services;
	}
}
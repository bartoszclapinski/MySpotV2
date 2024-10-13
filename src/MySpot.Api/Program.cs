using MySpot.Api.Entities;
using MySpot.Api.Repositories;
using MySpot.Api.Services;
using MySpot.Api.ValueObjects;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
	.AddScoped<IClock, Clock>()
	.AddSingleton<IWeeklyParkingSpotRepository, WeeklyParkingSpotRepository>()
	.AddScoped<IReservationService, ReservationService>()
	.AddControllers();
builder.Services.AddEndpointsApiExplorer();

WebApplication app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();


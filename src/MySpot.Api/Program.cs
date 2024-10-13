using MySpot.Application.Extensions;
using MySpot.Core.Extensions;
using MySpot.Infrastructure.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
	.AddCore()
	.AddApplication()
	.AddInfrastructure()
	.AddControllers();

builder.Services.AddEndpointsApiExplorer();

WebApplication app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();


using MySpot.Application.Services;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Infrastructure.Repositories;

internal class WeeklyParkingSpotRepository(IClock clock) : IWeeklyParkingSpotRepository
{
	
	private readonly List<WeeklyParkingSpot> _weeklyParkingSpots =
	[
		new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(clock.Current()), "P1"),
		new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(clock.Current()), "P2"),
		new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(clock.Current()), "P3"),
		new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(clock.Current()), "P4"),
		new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(clock.Current()), "P5")
	];

	public IEnumerable<WeeklyParkingSpot> GetAll() => _weeklyParkingSpots;
	

	public WeeklyParkingSpot Get(Guid id) => _weeklyParkingSpots.SingleOrDefault(x => x.Id == id)!;


	public void Add(WeeklyParkingSpot weeklyParkingSpot) => _weeklyParkingSpots.Add(weeklyParkingSpot);
	

	public void Update(WeeklyParkingSpot weeklyParkingSpot)
	{
	}

	public void Delete(WeeklyParkingSpot weeklyParkingSpot) => _weeklyParkingSpots.Remove(weeklyParkingSpot);

}
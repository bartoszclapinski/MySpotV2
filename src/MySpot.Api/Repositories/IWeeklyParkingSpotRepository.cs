using MySpot.Api.Entities;

namespace MySpot.Api.Repositories;

public interface IWeeklyParkingSpotRepository
{
	IEnumerable<WeeklyParkingSpot> GetAll();
	WeeklyParkingSpot Get(Guid id);
	void Add(WeeklyParkingSpot weeklyParkingSpot);
	void Update(WeeklyParkingSpot weeklyParkingSpot);
	void Delete(WeeklyParkingSpot weeklyParkingSpot);
}
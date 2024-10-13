using MySpot.Core.Entities;

namespace MySpot.Core.Repositories;

public interface IWeeklyParkingSpotRepository
{
	IEnumerable<WeeklyParkingSpot> GetAll();
	WeeklyParkingSpot Get(Guid id);
	void Add(WeeklyParkingSpot weeklyParkingSpot);
	void Update(WeeklyParkingSpot weeklyParkingSpot);
	void Delete(WeeklyParkingSpot weeklyParkingSpot);
}
namespace MySpot.UnitTests.Entities;

public class WeeklyParkingSpotTests
{
	[Fact]
	public void given_invalid_date_add_reservation_should_fail()
	{
		var now = new DateTime(2022, 08, 10);
		DateTime invalidDate = now.AddDays(1);
		
	}
}
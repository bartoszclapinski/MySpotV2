using MySpot.Core.Entities;
using MySpot.Core.Exceptions;
using MySpot.Core.ValueObjects;
using Shouldly;

namespace MySpot.UnitTests.Entities;

public class WeeklyParkingSpotTests
{
	#region Arrange

	private readonly Date _now;
	private readonly WeeklyParkingSpot _weeklyParkingSpot;
	
	public WeeklyParkingSpotTests()
	{
		_now = new Date(new DateTime(2024, 10, 09));
		_weeklyParkingSpot = new WeeklyParkingSpot(Guid.NewGuid(), new Week(_now), "P1");
	}
	
	#endregion
	
	[Theory]
	[InlineData("2024-10-06")]
	[InlineData("2024-10-14")]
	public void given_invalid_date_add_reservation_should_fail(string dateString)
	{
		//	Arrange
		DateTime invalidDate = DateTime.Parse(dateString);
		var reservation = new Reservation(Guid.NewGuid(), _weeklyParkingSpot.Id, "John Doe", "XYZ123", 
			new Date(invalidDate));
		
		//	Act
		Exception? exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(reservation, new Date(_now)));
		
		//	Assert
		exception.ShouldNotBeNull();
		exception.ShouldBeOfType<InvalidReservationDateException>();
	}

	[Fact]
	public void given_reservation_for_already_existing_date_add_reservation_should_be_fail()
	{
		//	Arrange
		Reservation reservation = new(Guid.NewGuid(), _weeklyParkingSpot.Id, "John Doe", "XYZ123", _now.AddDays(1));
		Reservation nextReservation = new(Guid.NewGuid(), _weeklyParkingSpot.Id, "Jane Doe", "ABC123", _now.AddDays(1));
		_weeklyParkingSpot.AddReservation(reservation, _now);
		
		//	Act
		Exception? exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(nextReservation, _now));
		
		//	Assert
		exception.ShouldNotBeNull();
		exception.ShouldBeOfType<ReservationAlreadyExistsException>();
	}
	
	[Fact]
	public void given_reservation_for_valid_date_add_reservation_should_succeed()
	{
		//	Arrange
		Reservation reservation = new(Guid.NewGuid(), _weeklyParkingSpot.Id, "John Doe", "XYZ123", _now.AddDays(1));
		
		//	Act
		_weeklyParkingSpot.AddReservation(reservation, _now);
		
		//	Assert
		_weeklyParkingSpot.Reservations.ShouldContain(reservation);
	}
}
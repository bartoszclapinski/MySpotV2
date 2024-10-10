using MySpot.Api.Commands;
using MySpot.Api.Entities;
using MySpot.Api.Services;
using MySpot.Api.ValueObjects;
using Shouldly;

namespace MySpot.UnitTests.Services;

public class ReservationServiceTests
{
    #region Arrange

    private readonly ReservationService _reservationService;
    private readonly List<WeeklyParkingSpot> _weeklyParkingSpots;

    public ReservationServiceTests()
    {
        _weeklyParkingSpots = 
        [
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(DateTimeOffset.Now), "P1"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(DateTimeOffset.Now), "P2"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(DateTimeOffset.Now), "P3"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(DateTimeOffset.Now), "P4"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(DateTimeOffset.Now), "P5")
        ];
        _reservationService = new ReservationService(_weeklyParkingSpots);
    }

    #endregion
    
    [Fact]
    public void given_reservation_for_not_taken_date_create_reservation_should_succeed()
    {
        //	Arrange
		Guid parkingSpotId = _weeklyParkingSpots.First().Id;        
        var command = new CreateReservation(Guid.NewGuid(), parkingSpotId, "John Doe", "XYZ123", 
            Date.Now);

        //	Act
        var result = _reservationService.Create(command);

        //	Assert
        result.ShouldNotBeNull();
        result.Value.ShouldBe(command.ReservationId);
    }
}

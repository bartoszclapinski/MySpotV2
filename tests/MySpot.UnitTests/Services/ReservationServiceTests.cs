using MySpot.Api.Commands;
using MySpot.Api.Entities;
using MySpot.Api.Repositories;
using MySpot.Api.Services;
using MySpot.Api.ValueObjects;
using MySpot.UnitTests.Utils;
using Shouldly;

namespace MySpot.UnitTests.Services;

public class ReservationServiceTests
{
    #region Arrange

    private readonly ReservationService _reservationService;
    private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;

    public ReservationServiceTests()
    {
        IClock clock = new Clock();
        _weeklyParkingSpotRepository = new WeeklyParkingSpotRepository(clock);
        _reservationService = new ReservationService(_weeklyParkingSpotRepository, clock);
    }

    #endregion
    
    [Fact]
    public void given_reservation_for_not_taken_date_create_reservation_should_succeed()
    {
        //	Arrange
		Guid parkingSpotId = _weeklyParkingSpotRepository.GetAll().First().Id;        
        var command = new CreateReservation(Guid.NewGuid(), parkingSpotId, "John Doe", "XYZ123", 
            Date.Now);

        //	Act
        var result = _reservationService.Create(command);

        //	Assert
        result.ShouldNotBeNull();
        result.Value.ShouldBe(command.ReservationId);
    }
}

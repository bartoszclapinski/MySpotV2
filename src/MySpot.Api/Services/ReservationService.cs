using MySpot.Api.Commands;
using MySpot.Api.Entities;
using MySpot.Api.Models;

namespace MySpot.Api.Services;

public class ReservationService
{
    private static readonly List<WeeklyParkingSpot> WeeklyParkingSpots =
    [
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"), DateTime.UtcNow, DateTime.UtcNow.AddDays(7), "P1"),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"), DateTime.UtcNow, DateTime.UtcNow.AddDays(7), "P2"),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"), DateTime.UtcNow, DateTime.UtcNow.AddDays(7), "P3"),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000004"), DateTime.UtcNow, DateTime.UtcNow.AddDays(7), "P4"),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000005"), DateTime.UtcNow, DateTime.UtcNow.AddDays(7), "P5")
    ];
        
    public ReservationDTO Get(Guid id) => GetAllWeekly().SingleOrDefault(x => x.Id == id);

    public IEnumerable<ReservationDTO> GetAllWeekly() => WeeklyParkingSpots
        .SelectMany(x => x.Reservations)
        .Select(x => new ReservationDTO
        {
            Id = x.Id,
            ParkingSpotId = x.ParkingSpotId,
            EmployeeName = x.EmployeeName,
            Date = x.Date
        });

    public Guid? Create(CreateReservation command)
    {
        var parkingSpot = WeeklyParkingSpots.SingleOrDefault(x => x.Id == command.ParkingSpotId);
        if (parkingSpot is null) return default;

        var reservation = new Reservation(
            command.ReservationId,
            command.ParkingSpotId, 
            command.EmployeeName, 
            command.LicensePlate, 
            command.Date);

        parkingSpot.AddReservation(reservation);
        return reservation.Id;
    }

    public bool Update(ChangeReservationLicensePlate command)
    {
        var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);
        if (weeklyParkingSpot is null) return false;

        var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id == command.ReservationId);
        if (existingReservation is null) return false;
        if (existingReservation.Date < DateTime.UtcNow.Date) return false;
        
        existingReservation.ChangeLicensePlate(command.LicensePlate);
        return true;
    }

    public bool Delete(DeleteReservation command)
    {
        var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);
        if (weeklyParkingSpot is null) return false;

        var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id == command.ReservationId);
        if (existingReservation is null) return false;

        return weeklyParkingSpot.RemoveReservation(command.ReservationId);
    }

    private WeeklyParkingSpot GetWeeklyParkingSpotByReservation(Guid reservationId) =>
        WeeklyParkingSpots.SingleOrDefault(x => x.Reservations.Any(r => r.Id == reservationId));
}
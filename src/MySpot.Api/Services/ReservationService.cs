using System.Runtime.CompilerServices;
using MySpot.Api.Commands;
using MySpot.Api.Entities;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Services;

public class ReservationService(List<WeeklyParkingSpot> weeklyParkingSpots)
{
    private static readonly Clock Clock = new();
    private readonly List<WeeklyParkingSpot> _weeklyParkingSpots = weeklyParkingSpots;
        
    public ReservationDTO Get(Guid id) => GetAllWeekly().SingleOrDefault(x => x.Id == id);

    public IEnumerable<ReservationDTO> GetAllWeekly() => _weeklyParkingSpots
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
        WeeklyParkingSpot parkingSpot = _weeklyParkingSpots.SingleOrDefault(x => x.Id == command.ParkingSpotId);
        if (parkingSpot is null) return default;

        var reservation = new Reservation(
            command.ReservationId,
            command.ParkingSpotId, 
            command.EmployeeName, 
            command.LicensePlate, 
            command.Date);

        parkingSpot.AddReservation(reservation, Clock.Current());
        return reservation.Id;
    }

    public bool Update(ChangeReservationLicensePlate command)
    {
        var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);
        if (weeklyParkingSpot is null) return false;

        var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id == command.ReservationId);
        if (existingReservation is null) return false;
        if (existingReservation.Date < Clock.Current()) return false;
        
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
        _weeklyParkingSpots.SingleOrDefault(x => x.Reservations.Any(r => r.Id == reservationId));
}
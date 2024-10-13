using MySpot.Application.Commands;
using MySpot.Application.DTO;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;

namespace MySpot.Application.Services;

public class ReservationService(IWeeklyParkingSpotRepository weeklyParkingSpotRepository, IClock clock) : IReservationService 
{
    public ReservationDTO Get(Guid id) => GetAllWeekly().SingleOrDefault(x => x.Id == id);

    public IEnumerable<ReservationDTO> GetAllWeekly() => weeklyParkingSpotRepository
        .GetAll()
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
        WeeklyParkingSpot parkingSpot = weeklyParkingSpotRepository.Get(command.ParkingSpotId);
            
        if (parkingSpot is null) return default;

        var reservation = new Reservation(
            command.ReservationId,
            command.ParkingSpotId, 
            command.EmployeeName, 
            command.LicensePlate, 
            command.Date);

        parkingSpot.AddReservation(reservation, clock.Current());
        return reservation.Id;
    }

    public bool Update(ChangeReservationLicensePlate command)
    {
        var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);
        if (weeklyParkingSpot is null) return false;

        var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id == command.ReservationId);
        if (existingReservation is null) return false;
        if (existingReservation.Date < clock.Current()) return false;
        
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
        weeklyParkingSpotRepository.GetAll().SingleOrDefault(x => x.Reservations.Any(r => r.Id == reservationId));
}
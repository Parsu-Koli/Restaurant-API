using BLL.DTOs;
using DAL.Interfaces;
using DAL.Models;
namespace BLL.Services
{
    public class ReservationTableServices(IReservationTableRepository repo) : IReservationTableRepository
    {
        private readonly IReservationTableRepository _repo = repo ?? throw new ArgumentNullException(nameof(repo));

        public void AddReservationTable(ReservationTable reservationTable)
        {
            _repo.AddReservationTable(reservationTable);
        }

        public void DeleteReservationTable(int id)
        {
            _repo.DeleteReservationTable(id);
        }

        public List<ReservationTable> GetAllReservationTables()
        {
            var reservations = _repo.GetAllReservationTables();
            var today = DateTime.Today;

            foreach (var r in reservations)
            {
                bool isExpiredDate = r.ReservedDate.Date < today;

                bool isStillValidStatus =
                    r.Status != null &&
                    (r.Status.Equals("Reserved", StringComparison.OrdinalIgnoreCase) ||
                     r.Status.Equals("Confirmed", StringComparison.OrdinalIgnoreCase));

                if (isExpiredDate && isStillValidStatus)
                {
                    r.Status = "Expired";
                    _repo.UpdateReservationTable(r);
                }
            }

            return reservations;
        }

        public ReservationTable GetReservationTableById(int id)
        {
            return _repo.GetReservationTableById(id);
        }

        public void UpdateReservationTable(ReservationTable reservationTable)
        {
            _repo.UpdateReservationTable(reservationTable);
        }

        public List<ReservationWithTableDto> GetAllReservationsWithStatus()
        {
            var reservations = _repo.GetAllReservationTables();

            return reservations.Select(r => new ReservationWithTableDto
            {
                ReservationId = r.ReservationId,
                CustomerId = r.CustomerId,
                Status = r.Status,
                ReservedDate = r.ReservedDate,
                TableNumber = r.RestaurantTable.TableNumber
            }).ToList();
        }
    }
}

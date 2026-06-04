using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementations
{
    public class ReservationTableRepository(AppDbContext context) : IReservationTableRepository
    {
        private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public List<ReservationTable> GetAllReservationTables()
        {
            return [.. _context.ReservationTables.Include(e=>e.Customer).Include(T=>T.RestaurantTable)];
        }

        public ReservationTable GetReservationTableById(int id)
        {
            var result = _context.ReservationTables
                .Include(r => r.Customer)
                .Include(r => r.RestaurantTable)
                .FirstOrDefault(r => r.ReservationId == id) ?? throw new Exception($"ReservationTable with id {id} not found.");
            return result;
        }

        public void AddReservationTable(ReservationTable reservationTable)
        {
            var start = reservationTable.ReservedDate.Date;
            var end = start.AddDays(1);

            // Check if already reserved
            var isAlreadyReserved = _context.ReservationTables.Any(r =>
                r.TableId == reservationTable.TableId &&
                r.Status == "Confirmed" &&
                r.ReservedDate >= start &&
                r.ReservedDate < end);

            if (isAlreadyReserved)
                throw new Exception("Table already reserved for this date.");

            // Check if currently occupied (only matters if today)
            var isOccupied = _context.Orders.Any(o =>
                o.TableId == reservationTable.TableId &&
                o.Status == "Active" &&
                reservationTable.ReservedDate.Date == DateTime.Today);

            if (isOccupied)
                throw new Exception("Table is currently occupied.");

            // ✅ Auto-confirm
            reservationTable.Status = "Confirmed";

            _context.ReservationTables.Add(reservationTable);
            _context.SaveChanges();
        }

        public void UpdateReservationTable(ReservationTable reservationTable)
        {
            var existing = _context.ReservationTables.Find(reservationTable.ReservationId);
            if (existing != null)
            {
                existing.TableId = reservationTable.TableId;
                existing.NumberOfGuests = reservationTable.NumberOfGuests;
                existing.ReservedDate = reservationTable.ReservedDate;
                existing.Status = reservationTable.Status;
                existing.CreatedAt = reservationTable.CreatedAt;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"ReservationTable with id {reservationTable.ReservationId} not found.");
            }
        }

        public void DeleteReservationTable(int id)
        {
            var result = _context.ReservationTables.Find(id);
            if (result != null)
            {
                _context.ReservationTables.Remove(result);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"ReservationTable with id {id} not found.");
            }
        }
    }
}

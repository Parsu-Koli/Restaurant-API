using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IReservationTableRepository
    {
            List<ReservationTable> GetAllReservationTables();
            ReservationTable GetReservationTableById(int id);
            void AddReservationTable(ReservationTable reservationTable);
            void UpdateReservationTable(ReservationTable reservationTable);
            void DeleteReservationTable(int id);
    }
}

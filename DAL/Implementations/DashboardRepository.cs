using DAL.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class DashboardRepository(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public int TotalTables()
        {
            var result = _context.RestaurantTables.Count();
            return result;
        }

        public int OccupiedTables()
        {
            return _context.RestaurantTables
                   .Count(t => t.Status == "Occupied");
        }

        public int TodaysOrder()
        {
            var start = DateTime.UtcNow.Date;
            var end = start.AddDays(1);

            return _context.Orders
                           .Count(o => o.CreatedAt >= start && o.CreatedAt < end);
        }

        public decimal TodayRevenue()
        {
            var start = DateTime.UtcNow.Date;
            var end = start.AddDays(1);

            return _context.Payments
                           .Where(p => p.PaidAt >= start && p.PaidAt < end)
                           .Sum(p => (decimal?)p.Amount) ?? 0;
        }
    }
}

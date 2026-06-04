using DAL.Implementations;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class DashboardServices(DashboardRepository repo) 
    {
        private readonly DashboardRepository _repo = repo;

        public int Total_tables()
        {
            return _repo.TotalTables();
        }

        public int Occupied_table()
        {
            return _repo.OccupiedTables();
        }

        public int Todays_orders()
        {
            return _repo.TodaysOrder();
        }

        public decimal Today_revenue()
        {
            return _repo.TodayRevenue();
        }
    }
}

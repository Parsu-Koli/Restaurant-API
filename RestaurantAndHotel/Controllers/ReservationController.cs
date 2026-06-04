using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantAndHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReservationController(ReservationTableServices services) : ControllerBase
    {
        private readonly ReservationTableServices _services = services ?? throw new ArgumentNullException(nameof(services));
        
        [HttpGet]
        public IActionResult GetAllReservations()
        {
            var reservations = _services.GetAllReservationTables();
            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public IActionResult GetReservationById(int id)
        {
            try
            {
                var reservation = _services.GetReservationTableById(id);
                return Ok(reservation);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateReservation([FromBody] ReservationTable reservationTable)
        {
            _services.AddReservationTable(reservationTable);
            return CreatedAtAction(nameof(GetReservationById), new { id = reservationTable.ReservationId }, reservationTable);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateReservation(int id, [FromBody] ReservationTable reservationTable)
        {
            if (id != reservationTable.ReservationId)
            {
                return BadRequest("ID mismatch");
            }
            try
            {
                _services.UpdateReservationTable(reservationTable);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReservation(int id)
        {
            try
            {
                _services.DeleteReservationTable(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetAllReservation")]
        public IActionResult GetAllReservationsWithDetails()
        {
            var reservations = _services.GetAllReservationsWithStatus();
            return Ok(reservations);
        }
    }
}

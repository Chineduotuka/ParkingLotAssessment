using Microsoft.AspNetCore.Mvc;
using ParkingLot.core.DTOs.Request;
using ParkingLot.core.Interfaces;

namespace ParkingLot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingLot : Controller
    {
        private readonly IParkingService _parkingService;

        public ParkingLot(IParkingService parkingService)
        {
            _parkingService= parkingService;
        }

        [HttpPost("park_vehicle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ParkVehicle([FromBody] ParkingRequestDTO request)
        {
            var result = await _parkingService.ParkVehicle(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("un_park_vehicle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UnPakVehicle([FromBody] UnParkRequestDTO request)
        {
            var result = await _parkingService.UnParkVehicle(request);
            return StatusCode(result.StatusCode, result);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingManagementAPI.Repository;

namespace ParkingManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingController : ControllerBase
    {
        private readonly ParkingRepository _parkingRepository;
        public ParkingController(ParkingRepository parkingRepository)
        {
            _parkingRepository = parkingRepository;
        }
        [HttpGet]
        public IActionResult GetParkings()
        {
            try
            {
                var parking = _parkingRepository.GetParkings();
                return Ok(parking);
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

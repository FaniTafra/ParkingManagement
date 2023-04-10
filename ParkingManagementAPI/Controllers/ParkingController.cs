using DomainModel.Models;
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
        [HttpGet("active")]
        public IActionResult GetActiveParkings()
        {
            try
            {
                var parking = _parkingRepository.GetActiveParkings();
                return Ok(parking);
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("archived")]
        public IActionResult GetArchivedParkings()
        {
            try
            {
                var parking = _parkingRepository.GetArchivedParkings();
                return Ok(parking);
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        public IActionResult AddParking([FromBody] Parking parking)
        {
            if (parking == null)
                return BadRequest();
            try
            {
                _parkingRepository.InsertParking(parking);
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut]
        public IActionResult UpdateParking([FromBody] Parking parking)
        {
            if (parking == null)
                return BadRequest();
            try
            {
                _parkingRepository.UpdateParking(parking);
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("{parkingId:int}")]
        public IActionResult GetParking(int parkingId)
        {
            try
            {
                return Ok(_parkingRepository.GetParking(parkingId));
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete("{parkingId:int}")]
        public IActionResult DeleteParking(int parkingId)
        {
            try
            {
                _parkingRepository.DeleteParking(parkingId);
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

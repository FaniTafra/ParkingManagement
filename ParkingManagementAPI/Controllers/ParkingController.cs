using DomainModel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ParkingManagementAPI.Repository;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
        [HttpGet("active/{userId}")]
        public IActionResult GetActiveParkings(int userId)
        {
            try
            {
                var parking = _parkingRepository.GetActiveParkings(userId);
                return Ok(parking);
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [Authorize]
        [HttpGet("archived/{userId}")]
        public IActionResult GetArchivedParkings(int userId)
        {
            try
            {
                var parking = _parkingRepository.GetArchivedParkings(userId);
                return Ok(parking);
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpPost]
        [Authorize]
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

        [HttpPut("{SelectedId:int}/{userId:int}")]
        public IActionResult SelectingParking(int SelectedId, int userId)
        {
            try
            {
                _parkingRepository.SelectingParking(SelectedId, userId);
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize]
        [HttpGet("selected/{userId}")]
        public IActionResult GetSelectedParkings(int userId)
        {
            try
            {
                var parking = _parkingRepository.GetSelectedParkings(userId);
                return Ok(parking);
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("cancel/{parkingId:int}")]
        public IActionResult CancelParking(int parkingId)
        {
            try
            {
                _parkingRepository.CancelParking(parkingId);
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

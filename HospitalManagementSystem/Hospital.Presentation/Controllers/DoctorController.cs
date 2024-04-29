using Hospital.Presentation.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            return Ok();
        }

        //[HttpGet]
        //public async Task<IActionResult> SearchDoctorsByASetOfProperties()
        //{
        //    return Ok();
        //}

        [HttpPost]
        public async Task<IActionResult> AddDoctor(DoctorDto doctor)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, DoctorDto doctor)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            return Ok();
        }
    }
}

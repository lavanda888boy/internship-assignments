using Hospital.Presentation.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllPatients()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            return Ok();
        }

        //[HttpGet]
        //public async Task<IActionResult> SearchPatientsByASetOfProperties()
        //{
        //    return Ok();
        //}

        [HttpPost]
        public async Task<IActionResult> AddPatient(PatientDto patient)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, PatientDto patient)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            return Ok();
        }
    }
}

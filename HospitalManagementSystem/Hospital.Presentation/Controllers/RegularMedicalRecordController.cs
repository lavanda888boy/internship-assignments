using Hospital.Presentation.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegularMedicalRecordController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllRegularMedicalRecords()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegularMedicalRecordById(int id)
        {
            return Ok();
        }

        //[HttpGet]
        //public async Task<IActionResult> SearchRegularMedicalRecordsByASetOfProperties()
        //{
        //    return Ok();
        //}

        [HttpPost]
        public async Task<IActionResult> AddRegularMedicalRecord(RegularMedicalRecordDto doctor)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRegularMedicalRecord(int id, RegularMedicalRecordDto doctor)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegularMedicalRecord(int id)
        {
            return Ok();
        }
    }
}

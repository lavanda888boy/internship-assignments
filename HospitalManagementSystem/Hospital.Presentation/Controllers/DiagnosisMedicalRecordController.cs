using Hospital.Presentation.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiagnosisMedicalRecordController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllDiagnosisMedicalRecords()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiagnosisMedicalRecordById(int id)
        {
            return Ok();
        }

        //[HttpGet]
        //public async Task<IActionResult> SearchDiagnosisMedicalRecordsByASetOfProperties()
        //{
        //    return Ok();
        //}

        [HttpPost]
        public async Task<IActionResult> AddDiagnosisMedicalRecord(DiagnosisMedicalRecordDto doctor)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDiagnosisMedicalRecord(int id, DiagnosisMedicalRecordDto doctor)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiagnosisMedicalRecord(int id)
        {
            return Ok();
        }
    }
}

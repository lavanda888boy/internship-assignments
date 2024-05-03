using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Commands;
using Hospital.Application.MedicalRecords.Queries;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Presentation.Dto.Record;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using DiagnosisMedicalRecordDto = Hospital.Presentation.Dto.Record.DiagnosisMedicalRecordDto;

namespace Hospital.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiagnosisMedicalRecordController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DiagnosisMedicalRecordController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDiagnosisMedicalRecords()
        {
            var command = new ListAllDiagnosisMedicalRecords();

            try
            {
                var records = await _mediator.Send(command);
                return Ok(records);
            }
            catch (DbException ex)
            {
                return StatusCode(500, $"Error in the database:\n{ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiagnosisMedicalRecordById(int id)
        {
            var command = new GetDiagnosisMedicalRecordById(id);

            try
            {
                var record = await _mediator.Send(command);
                return Ok(record);
            }
            catch (DbException ex)
            {
                return StatusCode(500, $"Error in the database:\n{ex.Message}");
            }
            catch (NoEntityFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddDiagnosisMedicalRecord(DiagnosisMedicalRecordDto record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Diagnosis medical record data for creation is invalid");
            }

            var command = new AddNewDiagnosisMedicalRecord(record.PatientId, record.DoctorId, record.ExaminationNotes,
                record.IllnessId, record.PrescribedMedicine, record.Duration);

            try
            {
                var newRecord = await _mediator.Send(command);
                return StatusCode(201, newRecord);
            }
            catch (DbException ex)
            {
                return StatusCode(500, $"Error in the database:\n{ex.Message}");
            }
            catch (NoEntityFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Search")]
        public async Task<IActionResult> SearchDiagnosisMedicalRecordsByASetOfProperties(DiagnosisMedicalRecordFilterDto recordFilter)
        {
            DiagnosisMedicalRecordFilters df = new DiagnosisMedicalRecordFilters()
            {
                ExaminedPatientId = recordFilter.ExaminedPatientId,
                ResponsibleDoctorId = recordFilter.ResponsibleDoctorId,
                DateOfExamination = recordFilter.DateOfExamination,
                DiagnosedIllnessName = recordFilter.DiagnosedIllnessName,
                PrescribedMedicine = recordFilter.PrescribedMedicine
            };
            var command = new SearchDiagnosisMedicalRecordsByASetOfProperties(df);

            try
            {
                var records = await _mediator.Send(command);
                return Ok(records);
            }
            catch (DbException ex)
            {
                return StatusCode(500, $"Error in the database:\n{ex.Message}");
            }
            catch (NoEntityFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("ExaminationNotes/{id}")]
        public async Task<IActionResult> UpdateDiagnosisMedicalRecordExaminationNotes(int id, [FromQuery] string notes)
        {
            var command = new AdjustDiagnosisMedicalRecordExaminationNotes(id, notes);

            try
            {
                var updatedRecord = await _mediator.Send(command);
                return Ok(updatedRecord);
            }
            catch (DbException ex)
            {
                return StatusCode(500, $"Error in the database:\n{ex.Message}");
            }
            catch (NoEntityFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("Treatment/{id}")]
        public async Task<IActionResult> UpdateDiagnosisMedicalRecordTreatmentDetails(int id, TreatmentDto treatment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Treatment data for diagnosis medical record update is invalid");
            }

            var command = new AdjustTreatmentDetailsWithinDiagnosisMedicalRecord(id, treatment.IllnessId,
                treatment.PrescribedMedicine, treatment.Duration);

            try
            {
                var updatedRecord = await _mediator.Send(command);
                return Ok(updatedRecord);
            }
            catch (DbException ex)
            {
                return StatusCode(500, $"Error in the database:\n{ex.Message}");
            }
            catch (NoEntityFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiagnosisMedicalRecord(int id)
        {
            var command = new RemoveWronglyAddedDiagnosisMedicalRecord(id);

            try
            {
                var deletedRecord = await _mediator.Send(command);
                return Ok(deletedRecord);
            }
            catch (DbException ex)
            {
                return StatusCode(500, $"Error in the database:\n{ex.Message}");
            }
            catch (NoEntityFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

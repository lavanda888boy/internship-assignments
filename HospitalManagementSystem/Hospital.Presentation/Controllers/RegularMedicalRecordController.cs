using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Commands;
using Hospital.Application.MedicalRecords.Queries;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Presentation.Dto.Record;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using RegularMedicalRecordDto = Hospital.Presentation.Dto.Record.RegularMedicalRecordDto;

namespace Hospital.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegularMedicalRecordController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RegularMedicalRecordController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegularMedicalRecords()
        {
            var command = new ListAllRegularMedicalRecords();

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
        public async Task<IActionResult> GetRegularMedicalRecordById(int id)
        {
            var command = new GetRegularMedicalRecordById(id);

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
        public async Task<IActionResult> AddRegularMedicalRecord(RegularMedicalRecordDto record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Regular medical record data for creation is invalid");
            }

            var command = new AddNewRegularMedicalRecord(record.PatientId, record.DoctorId, record.ExaminationNotes);

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
        public async Task<IActionResult> SearchRegularMedicalRecordsByASetOfProperties(RegularMedicalRecordFilterDto recordFilter)
        {
            RegularMedicalRecordFilters rf = new RegularMedicalRecordFilters()
            {
                ExaminedPatientId = recordFilter.ExaminedPatientId,
                ResponsibleDoctorId = recordFilter.ResponsibleDoctorId,
                DateOfExamination = recordFilter.DateOfExamination
            };
            var command = new SearchRegularMedicalRecordsByASetProperties(rf);

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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRegularMedicalRecordExaminationNotes(int id, [FromQuery] string notes)
        {
            var command = new AdjustRegularMedicalRecordExaminationNotes(id, notes);

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
        public async Task<IActionResult> DeleteRegularMedicalRecord(int id)
        {
            var command = new RemoveWronglyAddedRegularMedicalRecord(id);

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

﻿using System.ComponentModel.DataAnnotations;

namespace Hospital.Presentation.Dto
{
    public class RegularMedicalRecordDto
    {
        [Required]
        [Range(1, 500)]
        public required int PatientId { get; set; }

        [Required]
        [Range(1, 500)]
        public required int DoctorId { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(2000)]
        public required string ExaminationNotes { get; set; }
    }
}

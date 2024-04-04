﻿using Hospital.Domain.Models;

namespace Hospital.Application.Abstractions
{
    public interface IMedicalRecordRepository
    {
        MedicalRecord Add(MedicalRecord record);
        MedicalRecord Update(MedicalRecord record);
        bool Delete(MedicalRecord record);
        List<MedicalRecord>? GetByProperty(Func<MedicalRecord, bool> medicalRecordProperty);
        List<MedicalRecord> GetAll();
    }
}

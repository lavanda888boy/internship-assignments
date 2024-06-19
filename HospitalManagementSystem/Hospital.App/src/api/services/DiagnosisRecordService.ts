import { DiagnosisRecord } from "../../models/DiagnosisRecord";
import { PaginatedResult } from "../../models/PaginatedResult";
import api from "../axios";

class DiagnosisRecordService {
  public async getAllDiagnosisRecords(
    pageNumber: number,
    pageSize: number
  ): Promise<PaginatedResult<DiagnosisRecord>> {
    const response = await api.get("/DiagnosisMedicalRecord", {
      params: { pageNumber, pageSize },
    });
    return response.data;
  }

  public async getDiagnosisRecordById(id: number): Promise<DiagnosisRecord> {
    const response = await api.get(`/DiagnosisMedicalRecord/${id}`);
    return response.data;
  }

  public async searchDiagnosisRecordsByPatientId(
    id: number,
    pageNumber: number,
    pageSize: number
  ): Promise<PaginatedResult<DiagnosisRecord>> {
    const response = await api.post(
      "/DiagnosisMedicalRecord/Search",
      { examinedPatientId: id },
      {
        params: {
          pageNumber,
          pageSize,
        },
      }
    );
    return response.data;
  }

  public async addDiagnosisRecord(record: any): Promise<number> {
    const response = await api.post("/DiagnosisMedicalRecord", record);
    return response.data;
  }

  public async updateDiagnosisRecordExaminationNotes(
    id: number,
    notes: string
  ): Promise<number> {
    console.log(notes);
    const response = await api.put(
      `/DiagnosisMedicalRecord/ExaminationNotes/${id}`,
      null,
      { params: { notes: notes } }
    );
    return response.data;
  }

  public async updateDiagnosisRecordTreatmentDetails(
    id: number,
    illnessId: number | null,
    medicine: string,
    duration: number
  ): Promise<number> {
    const response = await api.put(`/DiagnosisMedicalRecord/Treatment/${id}`, {
      illnessId: illnessId,
      prescribedMedicine: medicine,
      duration: duration,
    });
    return response.data;
  }

  public async deleteDiagnosisRecord(id: number): Promise<number> {
    const response = await api.delete(`/DiagnosisMedicalRecord/${id}`);
    return response.data;
  }
}

export default DiagnosisRecordService;

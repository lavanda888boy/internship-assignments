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

  public async addDiagnosisRecord(record: any): Promise<number> {
    const response = await api.post("/DiagnosisMedicalRecord", record);
    return response.data;
  }

  //   public async updatePatient(patient: any, id: number): Promise<number> {
  //     const response = await api.put(`/Patient/Info/${id}`, patient);
  //     return response.data;
  //   }

  public async deleteDiagnosisRecord(id: number): Promise<number> {
    const response = await api.delete(`/DiagnosisMedicalRecord/${id}`);
    return response.data;
  }
}

export default DiagnosisRecordService;

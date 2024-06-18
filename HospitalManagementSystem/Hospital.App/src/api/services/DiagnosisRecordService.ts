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

  //   public async addPatient(patient: any): Promise<number> {
  //     const response = await api.post("/Patient", patient);
  //     return response.data;
  //   }

  //   public async updatePatient(patient: any, id: number): Promise<number> {
  //     const response = await api.put(`/Patient/Info/${id}`, patient);
  //     return response.data;
  //   }

  //   public async deletePatient(id: number): Promise<number> {
  //     const response = await api.delete(`/Patient/${id}`);
  //     return response.data;
  //   }
}

export default DiagnosisRecordService;

import { PaginatedResult } from "../../models/PaginatedResult";
import api from "../axios";
import { RegularRecord } from "../../models/RegularRecord";

class RegularRecordService {
  public async getAllRegularRecords(
    pageNumber: number,
    pageSize: number
  ): Promise<PaginatedResult<RegularRecord>> {
    const response = await api.get("/RegularMedicalRecord", {
      params: { pageNumber, pageSize },
    });
    return response.data;
  }

  public async getRegularRecordById(id: number): Promise<RegularRecord> {
    const response = await api.get(`/RegularMedicalRecord/${id}`);
    return response.data;
  }

  public async addRegularRecord(record: any): Promise<number> {
    const response = await api.post("/RegularMedicalRecord", record);
    return response.data;
  }

  //   public async updatePatient(patient: any, id: number): Promise<number> {
  //     const response = await api.put(`/Patient/Info/${id}`, patient);
  //     return response.data;
  //   }

  public async deleteRegularRecord(id: number): Promise<number> {
    const response = await api.delete(`/RegularMedicalRecord/${id}`);
    return response.data;
  }
}

export default RegularRecordService;

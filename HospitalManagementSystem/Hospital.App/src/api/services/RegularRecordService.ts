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

  public async searchRegularRecordsByPatientId(
    id: number,
    pageNumber: number,
    pageSize: number
  ): Promise<PaginatedResult<RegularRecord>> {
    const response = await api.post(
      "/RegularMedicalRecord/Search",
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

  public async addRegularRecord(record: any): Promise<number> {
    const response = await api.post("/RegularMedicalRecord", record);
    return response.data;
  }

  public async updateRegularRecordExaminationNotes(
    id: number,
    notes: string
  ): Promise<number> {
    const response = await api.put(`/RegularMedicalRecord/${id}`, null, {
      params: { notes: notes },
    });
    return response.data;
  }

  public async deleteRegularRecord(id: number): Promise<number> {
    const response = await api.delete(`/RegularMedicalRecord/${id}`);
    return response.data;
  }
}

export default RegularRecordService;

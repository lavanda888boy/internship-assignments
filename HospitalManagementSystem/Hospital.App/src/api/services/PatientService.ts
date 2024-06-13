import { PaginatedResult } from "../../models/PaginatedResult";
import { Patient } from "../../models/Patient";
import api from "../axios";

class PatientService {
  public async getAllPatients(
    pageNumber: number,
    pageSize: number
  ): Promise<PaginatedResult<Patient>> {
    const response = await api.get("/Patient", {
      params: { pageNumber, pageSize },
    });
    return response.data;
  }

  public async addPatient(patient: any): Promise<number> {
    const response = await api.post("/Patient", patient);
    return response.data;
  }

  public async updatePatient(patient: any, id: number): Promise<number> {
    const response = await api.put(`/Patient/Info/${id}`, patient);
    return response.data;
  }

  public async deletePatient(id: number): Promise<number> {
    const response = await api.delete(`/Patient/${id}`);
    return response.data;
  }
}

export default PatientService;

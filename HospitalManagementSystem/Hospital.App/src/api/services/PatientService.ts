import { Patient } from "../../models/Patient";
import api from "../axios";

interface PaginatedResult<T> {
  totalItems: number;
  items: T[];
}

class PatientService {
  public async getAllPatients(
    pageNumber: number,
    pageSize: number
  ): Promise<PaginatedResult<Patient>> {
    try {
      const response = await api.get("/Patient", {
        params: { pageNumber, pageSize },
      });
      return response.data;
    } catch (error) {
      throw error;
    }
  }

  public async addPatient(patient: any): Promise<number> {
    try {
      const response = await api.post("/Patient", patient);
      return response.data;
    } catch (error) {
      throw error;
    }
  }

  public async updatePatient(patient: any, id: number): Promise<number> {
    try {
      const response = await api.put(`/Patient/Info/${id}`, patient);
      return response.data;
    } catch (error) {
      throw error;
    }
  }

  public async deletePatient(id: number): Promise<number> {
    try {
      const response = await api.delete(`/Patient/${id}`);
      return response.data;
    } catch (error) {
      throw error;
    }
  }
}

export default new PatientService();

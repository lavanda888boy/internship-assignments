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
}

export default new PatientService();

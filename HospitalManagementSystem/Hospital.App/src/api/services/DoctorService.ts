import { PaginatedResult } from "../../models/PaginatedResult";
import { Doctor } from "../../models/Doctor";
import api from "../axios";

class DoctorService {
  public async getAllDoctors(
    pageNumber: number,
    pageSize: number
  ): Promise<PaginatedResult<Doctor>> {
    try {
      const response = await api.get("/Doctor", {
        params: { pageNumber, pageSize },
      });
      return response.data;
    } catch (error) {
      throw error;
    }
  }

  public async addDoctor(doctor: any): Promise<number> {
    try {
      const response = await api.post("/Doctor", doctor);
      return response.data;
    } catch (error) {
      throw error;
    }
  }

  public async updateDoctor(doctor: any, id: number): Promise<number> {
    try {
      const response = await api.put(`/Doctor/Info/${id}`, doctor);
      return response.data;
    } catch (error) {
      throw error;
    }
  }

  public async deleteDoctor(id: number): Promise<number> {
    try {
      const response = await api.delete(`/Doctor/${id}`);
      return response.data;
    } catch (error) {
      throw error;
    }
  }
}

export default new DoctorService();

import { PaginatedResult } from "../../models/PaginatedResult";
import { Doctor } from "../../models/Doctor";
import api from "../axios";

class DoctorService {
  public async getAllDoctors(
    pageNumber: number,
    pageSize: number
  ): Promise<PaginatedResult<Doctor>> {
    const response = await api.get("/Doctor", {
      params: { pageNumber, pageSize },
    });
    return response.data;
  }

  public async getDoctorById(id: number): Promise<Doctor> {
    const response = await api.get(`/Doctor/${id}`);
    return response.data;
  }

  public async addDoctor(doctor: any): Promise<number> {
    const response = await api.post("/Doctor", doctor);
    return response.data;
  }

  public async updateDoctor(doctor: any, id: number): Promise<number> {
    const response = await api.put(`/Doctor/Info/${id}`, doctor);
    return response.data;
  }

  public async deleteDoctor(id: number): Promise<number> {
    const response = await api.delete(`/Doctor/${id}`);
    return response.data;
  }
}

export default DoctorService;

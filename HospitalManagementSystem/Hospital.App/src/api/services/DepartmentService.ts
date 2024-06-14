import { Department } from "../../models/Department";
import api from "../axios";

class DepartmentService {
  public async getAllDepartments(): Promise<Department[]> {
    const response = await api.get("/Department");
    return response.data;
  }
}

export default DepartmentService;

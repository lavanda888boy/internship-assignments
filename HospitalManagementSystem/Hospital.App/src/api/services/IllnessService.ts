import { Illness } from "../../models/Illness";
import api from "../axios";

class IllnessService {
  public async getAllIllnesses(): Promise<Illness[]> {
    const response = await api.get("/Illness");
    return response.data;
  }
}

export default IllnessService;

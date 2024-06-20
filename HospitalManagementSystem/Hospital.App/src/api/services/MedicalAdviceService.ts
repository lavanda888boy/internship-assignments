import api from "../axios";

class MedicalAdviceService {
  public async getMedicalAdviceByPatientId(patientId: number): Promise<string> {
    const response = await api.get(`/MedicalAdvice/${patientId}`);
    return response.data;
  }
}

export default MedicalAdviceService;

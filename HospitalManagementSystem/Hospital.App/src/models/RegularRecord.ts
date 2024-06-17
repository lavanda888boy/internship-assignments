import { DoctorPartial } from "./DoctorPartial";
import { PatientPartial } from "./PatientPartial";

export interface RegularRecord {
  id: number;
  examinedPatient: PatientPartial;
  responsibleDoctor: DoctorPartial;
  dateOfExamination: string;
  examinationNotes: string;
}

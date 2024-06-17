import { PatientPartial } from "./PatientPartial";

export interface Doctor {
  id: number;
  name: string;
  surname: string;
  address: string;
  phoneNumber: string;
  department: string;
  workingHours?: DoctorSchedule;
  patients?: PatientPartial[];
}

interface DoctorSchedule {
  startShift: string;
  endShift: string;
  weekDays: string[];
}

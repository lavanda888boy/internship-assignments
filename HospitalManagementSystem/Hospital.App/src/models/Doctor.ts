export interface Doctor {
  id: number;
  name: string;
  surname: string;
  address: string;
  phoneNumber: string;
  department: string;
  workingHours?: DoctorSchedule;
  patients?: PatientShortInfo[];
}

interface DoctorSchedule {
  startShift: string;
  endShift: string;
  weekDays: string[];
}

interface PatientShortInfo {
  id: number;
  fullName: string;
}

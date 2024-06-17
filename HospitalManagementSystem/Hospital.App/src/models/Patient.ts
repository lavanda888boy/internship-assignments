import { DoctorPartial } from "./DoctorPartial";

export interface Patient {
  id: number;
  name: string;
  surname: string;
  age: number;
  gender: string;
  address: string;
  phoneNumber?: string;
  insuranceNumber?: string;
  doctors?: DoctorPartial[];
}

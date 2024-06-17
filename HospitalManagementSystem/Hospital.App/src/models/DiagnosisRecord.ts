import { RegularRecord } from "./RegularRecord";

export interface DiagnosisRecord extends RegularRecord {
  diagnosedIllness?: IllnessDto;
  proposedTreatment?: TreatmentDto;
}

interface IllnessDto {
  name: string;
  severity: string;
}

interface TreatmentDto {
  prescribedMedicine: string;
  durationInDays: number;
}

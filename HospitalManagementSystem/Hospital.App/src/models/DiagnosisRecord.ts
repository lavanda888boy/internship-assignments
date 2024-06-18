import { Illness } from "./Illness";
import { RegularRecord } from "./RegularRecord";

export interface DiagnosisRecord extends RegularRecord {
  diagnosedIllness?: Illness;
  proposedTreatment?: TreatmentDto;
}

interface TreatmentDto {
  prescribedMedicine: string;
  durationInDays: number;
}

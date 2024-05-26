import { Record } from "../../models/Record";
import "./RecordCard.css";

interface RecordCardProps {
  record: Record;
}

function RecordCard({ record: record }: RecordCardProps) {
  return (
    <>
      <div className="card-content">
        <div className="card-header">
          <p>{record.DateOfExamination}</p>
          <p>Examined patient: {record.PatientFullName}</p>
          <p>Responsible doctor: {record.DoctorFullName}</p>
        </div>
        <p className="card-info">
          Examination notes: {record.ExaminationNotes}
        </p>
        {record.DiagnosedIllness && (
          <p className="card-info">Diagnosis: {record.DiagnosedIllness}</p>
        )}
        {record.ProposedTreatment && (
          <p className="card-info">Treatment: {record.ProposedTreatment}</p>
        )}
      </div>
    </>
  );
}

export default RecordCard;

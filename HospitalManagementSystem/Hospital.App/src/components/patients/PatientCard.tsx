import { Patient } from "../../models/Patient";
import "./PatientCard.css";
import person from "../../assets/person.jpg";

interface PatientCardProps {
  patient: Patient;
}

function PatientCard({ patient }: PatientCardProps) {
  return (
    <>
      <div className="card-content">
        <img src={person} className="card-image" />
        <p className="card-info">{patient.Name + " " + patient.Surname}</p>
        <p className="card-info">Age: {patient.Age}</p>
        <p className="card-info">Gender: {patient.Gender}</p>
        <p className="card-info">Address: {patient.Address}</p>
        {patient.PhoneNumber && (
          <p className="card-info">Phone number: {patient.PhoneNumber}</p>
        )}
        {patient.InsuranceNumber && (
          <p className="card-info">Insurance: {patient.InsuranceNumber}</p>
        )}
      </div>
    </>
  );
}

export default PatientCard;

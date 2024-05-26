import { Doctor } from "../../models/Doctor";
import "./DoctorCard.css";
import person from "../../assets/person.jpg";

interface DoctorCardProps {
  doctor: Doctor;
}

function DoctorCard({ doctor }: DoctorCardProps) {
  return (
    <>
      <div className="card-content">
        <img src={person} className="card-image" />
        <p className="card-info">{doctor.Name + " " + doctor.Surname}</p>
        <p className="card-info">{doctor.Department}</p>
      </div>
    </>
  );
}

export default DoctorCard;

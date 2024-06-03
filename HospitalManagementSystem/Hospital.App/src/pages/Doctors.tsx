import DoctorCard from "../components/doctors/DoctorCard";
import { Doctor } from "../models/Doctor";
import "./Doctors.css";
import usePageTitle from "../hooks/PageTitleHook";

function Doctors() {
  usePageTitle("Doctors");

  const doctors: Doctor[] = [
    {
      id: 1,
      Name: "John",
      Surname: "Peters",
      Department: "Cardiology",
    },
    {
      id: 2,
      Name: "Jane",
      Surname: "Smith",
      Department: "Neurology",
    },
    {
      id: 3,
      Name: "Alice",
      Surname: "Johnson",
      Department: "Pediatrics",
    },
    {
      id: 4,
      Name: "John",
      Surname: "Peters",
      Department: "Cardiology",
    },
    {
      id: 5,
      Name: "Jane",
      Surname: "Smith",
      Department: "Neurology",
    },
    {
      id: 6,
      Name: "Alice",
      Surname: "Johnson",
      Department: "Pediatrics",
    },
  ];

  return (
    <>
      <section className="doctors-content">
        <div className="content-list">
          {doctors.map((doctor, index) => (
            <DoctorCard key={index} doctor={doctor} />
          ))}
        </div>
      </section>
    </>
  );
}

export default Doctors;

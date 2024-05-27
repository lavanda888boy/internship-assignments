import { useContext, useEffect } from "react";
import DoctorCard from "../components/doctors/DoctorCard";
import { Doctor } from "../models/Doctor";
import "./Doctors.css";
import { PageContext } from "../PageContext";

function Doctors() {
  const pageContextProps = useContext(PageContext);

  useEffect(() => {
    pageContextProps?.setPageName("Doctors");
  }, []);

  const doctors: Doctor[] = [
    {
      Name: "John",
      Surname: "Peters",
      Department: "Cardiology",
    },
    {
      Name: "Jane",
      Surname: "Smith",
      Department: "Neurology",
    },
    {
      Name: "Alice",
      Surname: "Johnson",
      Department: "Pediatrics",
    },
    {
      Name: "John",
      Surname: "Peters",
      Department: "Cardiology",
    },
    {
      Name: "Jane",
      Surname: "Smith",
      Department: "Neurology",
    },
    {
      Name: "Alice",
      Surname: "Johnson",
      Department: "Pediatrics",
    },
  ];

  return (
    <>
      <section className="page-content">
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

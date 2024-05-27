import PatientCard from "../components/patients/PatientCard";
import { Patient } from "../models/Patient";
import "./Patients.css";
import usePageTitle from "../hooks/PageTitleHook";

function Patients() {
  usePageTitle("Patients");

  const patients: Patient[] = [
    {
      Name: "John",
      Surname: "Peters",
      Age: 25,
      Gender: "Male",
      Address: "85 Qwertysdnsfd,snfdsm street",
    },
    {
      Name: "Jane",
      Surname: "Smith",
      Age: 25,
      Gender: "Male",
      Address: "15 Asdf street",
      PhoneNumber: "078945635",
      InsuranceNumber: "AB12574896",
    },
    {
      Name: "Alice",
      Surname: "Johnson",
      Age: 25,
      Gender: "Male",
      Address: "30 Zxcv street",
      PhoneNumber: "078945635",
    },
  ];

  return (
    <>
      <section className="page-content">
        <div className="content-list">
          {patients.map((patient, index) => (
            <PatientCard key={index} patient={patient} />
          ))}
        </div>
      </section>
    </>
  );
}

export default Patients;

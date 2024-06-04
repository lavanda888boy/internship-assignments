import { Patient } from "../models/Patient";
import PatientsTable from "../components/patients/PatientsTable";
import "./Patients.css";
import usePageTitle from "../hooks/PageTitleHook";
import CreateActionButton from "../components/shared/CreateActionButton";

function Patients() {
  usePageTitle("Patients");

  const patients: Patient[] = [
    {
      id: 1,
      Name: "John",
      Surname: "Peters",
      Age: 25,
      Gender: "Male",
      Address: "85 Qwertysdnsfd,snfdsm street",
    },
    {
      id: 2,
      Name: "Jane",
      Surname: "Smith",
      Age: 25,
      Gender: "Male",
      Address: "15 Asdf street",
      PhoneNumber: "078945635",
      InsuranceNumber: "AB12574896",
    },
    {
      id: 3,
      Name: "Alice",
      Surname: "Johnson",
      Age: 25,
      Gender: "Male",
      Address: "30 Zxcv street",
      PhoneNumber: "078945635",
    },
  ];

  return (
    <div className="patients-content">
      <CreateActionButton entityName="Patient" />
      <PatientsTable patients={patients}></PatientsTable>
    </div>
  );
}

export default Patients;

import { Patient } from "../models/Patient";
import PatientsTable from "../components/patients/PatientsTable";
import "./Patients.css";
import usePageTitle from "../hooks/PageTitleHook";
import CreateActionButton from "../components/shared/CreateActionButton";
import { useState } from "react";
import PatientFormDialog from "../components/patients/PatientFormDialog";

function Patients() {
  usePageTitle("Patients");

  const [createFormOpen, setCreateFormOpen] = useState(false);

  const handleCreateFormOpen = () => {
    setCreateFormOpen(true);
  };

  const handleCreateFormClose = () => {
    setCreateFormOpen(false);
  };

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
      <CreateActionButton
        entityName="Patient"
        clickAction={handleCreateFormOpen}
      />
      <PatientsTable patients={patients}></PatientsTable>
      <PatientFormDialog
        open={createFormOpen}
        onClose={handleCreateFormClose}
      />
    </div>
  );
}

export default Patients;

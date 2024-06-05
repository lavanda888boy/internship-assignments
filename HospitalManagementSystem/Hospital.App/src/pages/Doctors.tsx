import DoctorCard from "../components/doctors/DoctorCard";
import { Doctor } from "../models/Doctor";
import "./Doctors.css";
import usePageTitle from "../hooks/PageTitleHook";
import CreateActionButton from "../components/shared/CreateActionButton";
import { useState } from "react";
import DoctorFormDialog from "../components/doctors/DoctorFormDialog";

function Doctors() {
  usePageTitle("Doctors");

  const [createFormOpen, setCreateFormOpen] = useState(false);

  const handleCreateFormOpen = () => {
    setCreateFormOpen(true);
  };

  const handleCreateFormClose = () => {
    setCreateFormOpen(false);
  };

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
          <CreateActionButton
            entityName="Doctor"
            clickAction={handleCreateFormOpen}
          />
          {doctors.map((doctor, index) => (
            <DoctorCard key={index} doctor={doctor} />
          ))}
        </div>
        <DoctorFormDialog
          open={createFormOpen}
          onClose={handleCreateFormClose}
        />
      </section>
    </>
  );
}

export default Doctors;

import RecordCard from "../components/records/RecordCard";
import { Record } from "../models/Record";
import "./Records.css";
import usePageTitle from "../hooks/PageTitleHook";
import CreateActionButton from "../components/shared/CreateActionButton";
import { useState } from "react";
import RecordFormDialog from "../components/records/RecordFormDialog";

function Records() {
  usePageTitle("Medical Records");

  const [createFormOpen, setCreateFormOpen] = useState(false);

  const handleCreateFormOpen = () => {
    setCreateFormOpen(true);
  };

  const handleCreateFormClose = () => {
    setCreateFormOpen(false);
  };

  const records: Record[] = [
    {
      id: 1,
      PatientFullName: "Mike Jordan",
      DoctorFullName: "John Doe",
      DateOfExamination: new Date().toDateString(),
      ExaminationNotes: "The patient was alright when I examined him.",
      DiagnosedIllness: "Happiness",
      ProposedTreatment: "Citramon, 3 times a day, 7 days",
    },
    {
      id: 2,
      PatientFullName: "Mike Jordan",
      DoctorFullName: "John Doe",
      DateOfExamination: new Date().toDateString(),
      ExaminationNotes: "The patient was alright when I examined him.",
      DiagnosedIllness: "Happiness",
      ProposedTreatment: "Citramon, 3 times a day, 7 days",
    },
    {
      id: 3,
      PatientFullName: "Mike Jordan",
      DoctorFullName: "John Doe",
      DateOfExamination: new Date().toDateString(),
      ExaminationNotes: "The patient was alright when I examined him.",
      DiagnosedIllness: "Happiness",
      ProposedTreatment: "Citramon, 3 times a day, 7 days",
    },
  ];

  return (
    <>
      <section className="records-content">
        <div className="content-list">
          <CreateActionButton
            entityName="Record"
            clickAction={handleCreateFormOpen}
          />
          {records.map((record, index) => (
            <RecordCard key={index} record={record} />
          ))}
        </div>
        <RecordFormDialog
          open={createFormOpen}
          onClose={handleCreateFormClose}
        />
      </section>
    </>
  );
}

export default Records;

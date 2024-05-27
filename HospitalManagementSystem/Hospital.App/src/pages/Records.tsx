import { useContext, useEffect } from "react";
import RecordCard from "../components/records/RecordCard";
import { Record } from "../models/Record";
import "./Records.css";
import { PageContext } from "../PageContext";

function Records() {
  const pageContextProps = useContext(PageContext);

  useEffect(() => {
    pageContextProps?.setPageName("Medical Records");
  }, []);

  const records: Record[] = [
    {
      PatientFullName: "Mike Jordan",
      DoctorFullName: "John Doe",
      DateOfExamination: new Date().toDateString(),
      ExaminationNotes: "The patient was alright when I examined him.",
      DiagnosedIllness: "Happiness",
      ProposedTreatment: "Citramon, 3 times a day, 7 days",
    },
    {
      PatientFullName: "Mike Jordan",
      DoctorFullName: "John Doe",
      DateOfExamination: new Date().toDateString(),
      ExaminationNotes: "The patient was alright when I examined him.",
      DiagnosedIllness: "Happiness",
      ProposedTreatment: "Citramon, 3 times a day, 7 days",
    },
    {
      PatientFullName: "Mike Jordan",
      DoctorFullName: "John Doe",
      DateOfExamination: new Date().toDateString(),
      ExaminationNotes: "The patient was alright when I examined him.",
      DiagnosedIllness: "Happiness",
      ProposedTreatment: "Citramon, 3 times a day, 7 days",
    },
    {
      PatientFullName: "Mike Jordan",
      DoctorFullName: "John Doe",
      DateOfExamination: new Date().toDateString(),
      ExaminationNotes: "The patient was alright when I examined him.",
      DiagnosedIllness: "Happiness",
      ProposedTreatment: "Citramon, 3 times a day, 7 days",
    },
    {
      PatientFullName: "Mike Jordan",
      DoctorFullName: "John Doe",
      DateOfExamination: new Date().toDateString(),
      ExaminationNotes: "The patient was alright when I examined him.",
      DiagnosedIllness: "Happiness",
      ProposedTreatment: "Citramon, 3 times a day, 7 days",
    },
    {
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
      <section className="page-content">
        <div className="content-list">
          {records.map((record, index) => (
            <RecordCard key={index} record={record} />
          ))}
        </div>
      </section>
    </>
  );
}

export default Records;

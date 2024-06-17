import RecordCard from "../components/records/RecordCard";
import { DiagnosisRecord } from "../models/DiagnosisRecord";
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
          isOpened={createFormOpen}
          onClose={handleCreateFormClose}
        />
      </section>
    </>
  );
}

export default Records;

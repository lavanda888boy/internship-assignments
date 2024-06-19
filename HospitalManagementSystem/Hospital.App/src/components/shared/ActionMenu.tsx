import React, { useState } from "react";
import { IconButton, Menu, MenuItem } from "@mui/material";
import { Patient } from "../../models/Patient";
import PatientFormDialog from "../patients/PatientFormDialog";
import { MoreHoriz } from "@mui/icons-material";
import { Doctor } from "../../models/Doctor";
import DoctorFormDialog from "../doctors/DoctorFormDialog";
import RecordFormDialog from "../records/RecordFormDialog";

interface ActionMenuProps {
  rowId: number;
  patient?: Patient | null;
  doctor?: Doctor | null;
  record?: any;
  anchorEl: HTMLElement | null;
  handleMenuClick: (
    event: React.MouseEvent<HTMLButtonElement>,
    id: number
  ) => void;
  handleMenuClose: () => void;
  onEntityDelete: () => void;
}

function ActionMenu({
  rowId,
  patient,
  doctor,
  record,
  anchorEl,
  handleMenuClick,
  handleMenuClose,
  onEntityDelete,
}: ActionMenuProps) {
  const [isDialogOpen, setIsDialogOpen] = useState(false);

  const handleOpenDialog = () => {
    setIsDialogOpen(true);
  };

  const handleCloseDialog = () => {
    setIsDialogOpen(false);
  };

  const handleUpdateEntity = () => {
    window.location.reload();
  };

  const handleDeleteEntity = () => {
    onEntityDelete();
    handleMenuClose();
  };

  return (
    <>
      <IconButton
        aria-label="actions"
        onClick={(event) => handleMenuClick(event, rowId)}
      >
        <MoreHoriz />
      </IconButton>
      <Menu
        anchorEl={anchorEl}
        open={Boolean(anchorEl)}
        onClose={handleMenuClose}
      >
        <MenuItem onClick={handleOpenDialog}>Update</MenuItem>
        <MenuItem onClick={handleDeleteEntity}>Delete</MenuItem>
      </Menu>
      {patient && (
        <PatientFormDialog
          isOpened={isDialogOpen}
          onClose={handleCloseDialog}
          onPatientAdded={() => {}}
          onPatientUpdated={handleUpdateEntity}
          patient={patient}
        />
      )}
      {doctor && (
        <DoctorFormDialog
          isOpened={isDialogOpen}
          onClose={handleCloseDialog}
          onDoctorAdded={() => {}}
          doctor={doctor}
          onDoctorUpdated={handleUpdateEntity}
        />
      )}
      {record && (
        <RecordFormDialog
          isOpened={isDialogOpen}
          onClose={handleCloseDialog}
          onRecordAdded={() => {}}
          record={record}
          onRecordUpdated={handleUpdateEntity}
        />
      )}
    </>
  );
}

export default ActionMenu;

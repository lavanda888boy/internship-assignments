import React, { useState } from "react";
import { IconButton, Menu, MenuItem } from "@mui/material";
import MoreVertIcon from "@mui/icons-material/MoreVert";
import { Patient } from "../../models/Patient";
import PatientFormDialog from "../patients/PatientFormDialog";

interface ActionMenuProps {
  rowId: number;
  patient: Patient | null;
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
  };

  return (
    <>
      <IconButton
        aria-label="actions"
        onClick={(event) => handleMenuClick(event, rowId)}
      >
        <MoreVertIcon />
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
    </>
  );
}

export default ActionMenu;

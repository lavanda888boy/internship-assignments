import React from "react";
import { IconButton, Menu, MenuItem } from "@mui/material";
import MoreVertIcon from "@mui/icons-material/MoreVert";

interface ActionMenuProps {
  rowId: number;
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
  anchorEl,
  handleMenuClick,
  handleMenuClose,
  onEntityDelete,
}: ActionMenuProps) {
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
        <MoreVertIcon />
      </IconButton>
      <Menu
        anchorEl={anchorEl}
        open={Boolean(anchorEl)}
        onClose={handleMenuClose}
      >
        <MenuItem onClick={handleMenuClose}>Update</MenuItem>
        <MenuItem onClick={handleDeleteEntity}>Delete</MenuItem>
      </Menu>
    </>
  );
}

export default ActionMenu;

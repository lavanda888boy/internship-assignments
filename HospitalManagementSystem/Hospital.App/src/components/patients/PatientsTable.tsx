import { DataGrid, GridColDef } from "@mui/x-data-grid";
import { Patient } from "../../models/Patient";
import React, { useState } from "react";
import { IconButton, Menu, MenuItem } from "@mui/material";
import MoreVertIcon from "@mui/icons-material/MoreVert";

interface PatientsTableProps {
  patients: Patient[];
}

function PatientsTable({ patients }: PatientsTableProps) {
  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
  const [selectedRowId, setSelectedRowId] = useState(0);

  const handleMenuClick = (
    event: React.MouseEvent<HTMLButtonElement>,
    id: number
  ) => {
    setAnchorEl(event.currentTarget);
    setSelectedRowId(id);
  };

  const handleMenuClose = () => {
    setAnchorEl(null);
  };

  const columns: GridColDef[] = [
    { field: "Name", headerName: "Name", width: 120 },
    { field: "Surname", headerName: "Surname", width: 120 },
    { field: "Age", headerName: "Age", width: 50 },
    { field: "Gender", headerName: "Gender", width: 100 },
    { field: "Address", headerName: "Address", width: 270 },
    { field: "PhoneNumber", headerName: "Phone", width: 130 },
    { field: "InsuranceNumber", headerName: "Insurance", width: 130 },
    {
      field: "actions",
      headerName: "Actions",
      width: 50,
      renderCell: (params) => (
        <IconButton
          aria-label="actions"
          onClick={(event) => handleMenuClick(event, params.row.id)}
        >
          <MoreVertIcon />
        </IconButton>
      ),
    },
  ];

  return (
    <>
      <DataGrid
        rows={patients}
        columns={columns}
        disableRowSelectionOnClick
        initialState={{
          pagination: {
            paginationModel: { page: 1, pageSize: 5 },
          },
        }}
        pageSizeOptions={[5, 10]}
        sx={{
          padding: "1% 2% 1% 2%",
          border: "5px solid #B8B8FF",
          borderRadius: "5px",
        }}
      />
      <Menu
        anchorEl={anchorEl}
        open={Boolean(anchorEl)}
        onClose={handleMenuClose}
      >
        <MenuItem>Update</MenuItem>
        <MenuItem>Delete</MenuItem>
      </Menu>
    </>
  );
}

export default PatientsTable;

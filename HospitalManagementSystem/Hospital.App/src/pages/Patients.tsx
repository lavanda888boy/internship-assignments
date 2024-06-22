import { useState, useEffect, useContext } from "react";
import {
  Container,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Pagination,
  Select,
  MenuItem,
  Typography,
  SelectChangeEvent,
  Box,
} from "@mui/material";
import usePageTitle from "../hooks/PageTitleHook";
import CreateActionButton from "../components/shared/CreateActionButton";
import PatientFormDialog from "../components/patients/PatientFormDialog";
import PatientService from "../api/services/PatientService";
import { Patient } from "../models/Patient";
import { UserRoleContext } from "../context/UserRoleContext";
import ActionMenu from "../components/shared/ActionMenu";
import { NotificationState } from "../models/utils/NotificationState";
import ActionResultNotification from "../components/shared/ActionResultNotification";

function Patients() {
  usePageTitle("Patients");

  const userRoleContextProps = useContext(UserRoleContext);

  const patientService: PatientService = new PatientService();

  const [patients, setPatients] = useState<Patient[]>([]);
  const [createFormOpen, setCreateFormOpen] = useState(false);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalItems, setTotalItems] = useState(0);
  const [pageSize, setPageSize] = useState(10);

  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const [selectedPatient, setSelectedPatient] = useState<Patient | null>(null);

  const [notification, setNotification] = useState<NotificationState>({
    open: false,
    message: "",
    severity: "error",
  });

  useEffect(() => {
    const fetchPatients = async () => {
      try {
        const response = await patientService.getAllPatients(
          currentPage,
          pageSize
        );
        setPatients(response.items);
        setTotalItems(response.totalItems);
      } catch (error) {
        console.log(error);
      }
    };

    fetchPatients();
  }, [currentPage, pageSize]);

  const handleMenuClick = (
    event: React.MouseEvent<HTMLButtonElement>,
    patient: Patient
  ) => {
    setAnchorEl(event.currentTarget);
    setSelectedPatient(patient);
  };

  const handleMenuClose = () => {
    setAnchorEl(null);
    setSelectedPatient(null);
  };

  const handleCreateFormOpen = () => {
    setCreateFormOpen(true);
  };

  const handleCreateFormClose = () => {
    setCreateFormOpen(false);
  };

  const handleAddPatient = (newPatient: Patient) => {
    setPatients((prevPatients) => [newPatient, ...prevPatients]);
  };

  const handleDeletePatient = async () => {
    try {
      if (selectedPatient) {
        await patientService.deletePatient(selectedPatient.id);
        setPatients((prevPatients) =>
          prevPatients.filter((p) => p.id !== selectedPatient.id)
        );
      }
    } catch (error) {
      setNotification({
        open: true,
        message: "Failed to remove patient.",
        severity: "error",
      });

      console.log(error);
    }
  };

  const handlePageChange = (
    _event: React.ChangeEvent<unknown>,
    newPage: number
  ) => {
    setCurrentPage(newPage);
  };

  const handlePageSizeChange = (event: SelectChangeEvent<number>) => {
    setPageSize(parseInt(event.target.value as string));
    setCurrentPage(1);
  };

  const handleCloseNotification = () => {
    setNotification((prev: NotificationState) => ({ ...prev, open: false }));
  };

  return (
    <Container
      sx={{
        position: "absolute",
        height: "auto",
        zIndex: 1,
        padding: "1.5% 2% 2% 2%",
        marginTop: "8%",
        marginLeft: "8%",
        borderRadius: "5px",
        backgroundColor: "white",
      }}
    >
      <ActionResultNotification
        state={notification}
        onClose={handleCloseNotification}
      />
      {userRoleContextProps?.userRole === "Admin" && (
        <CreateActionButton
          entityName="Patient"
          clickAction={handleCreateFormOpen}
        />
      )}
      <TableContainer
        component={Paper}
        sx={{
          mt: 3,
          borderRadius: "10px",
        }}
      >
        <Table>
          <TableHead sx={{ backgroundColor: "lightgray" }}>
            <TableRow>
              <TableCell align="center">
                <Typography>Name</Typography>
              </TableCell>
              <TableCell align="center">
                <Typography>Surname</Typography>
              </TableCell>
              <TableCell align="center">
                <Typography>Age</Typography>
              </TableCell>
              <TableCell align="center">
                <Typography>Gender</Typography>
              </TableCell>
              <TableCell align="center">
                <Typography>Phone</Typography>
              </TableCell>
              <TableCell align="center">
                <Typography>Insurance</Typography>
              </TableCell>
              {userRoleContextProps?.userRole === "Admin" && (
                <>
                  <TableCell align="center">
                    <Typography>Doctors</Typography>
                  </TableCell>
                  <TableCell align="center">
                    <Typography>Actions</Typography>
                  </TableCell>
                </>
              )}
            </TableRow>
          </TableHead>
          <TableBody>
            {patients.map((patient) => (
              <TableRow key={patient.id}>
                <TableCell align="center">
                  <Typography>{patient.name}</Typography>
                </TableCell>
                <TableCell align="center">
                  <Typography>{patient.surname}</Typography>
                </TableCell>
                <TableCell align="center">
                  <Typography>{patient.age}</Typography>
                </TableCell>
                <TableCell align="center">
                  <Typography>{patient.gender}</Typography>
                </TableCell>
                <TableCell align="center">
                  <Typography>{patient.phoneNumber}</Typography>
                </TableCell>
                <TableCell align="center">
                  <Typography>{patient.insuranceNumber}</Typography>
                </TableCell>
                {userRoleContextProps?.userRole === "Admin" && (
                  <>
                    <TableCell align="center">
                      {patient.doctors?.map((doctor) => (
                        <Typography key={doctor.id}>
                          {doctor.name} {doctor.surname} ({doctor.department})
                        </Typography>
                      ))}
                    </TableCell>
                    <TableCell align="center">
                      <ActionMenu
                        rowId={patient.id}
                        anchorEl={anchorEl}
                        handleMenuClick={(event) =>
                          handleMenuClick(event, patient)
                        }
                        handleMenuClose={handleMenuClose}
                        onEntityDelete={handleDeletePatient}
                        patient={selectedPatient}
                      />
                    </TableCell>
                  </>
                )}
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
      <Box
        sx={{
          display: "flex",
          alignItems: "center",
          mt: 2,
          ml: 37,
        }}
      >
        <Typography sx={{ mr: 1 }}>Patients per page:</Typography>
        <Select
          value={pageSize}
          onChange={handlePageSizeChange}
          sx={{ mr: 0.65 }}
        >
          <MenuItem value={5}>5</MenuItem>
          <MenuItem value={10}>10</MenuItem>
          <MenuItem value={15}>15</MenuItem>
        </Select>
        <Pagination
          count={Math.ceil(totalItems / pageSize)}
          page={currentPage}
          onChange={handlePageChange}
          color="primary"
        />
      </Box>
      <PatientFormDialog
        isOpened={createFormOpen}
        onClose={handleCreateFormClose}
        onPatientAdded={handleAddPatient}
      />
    </Container>
  );
}

export default Patients;

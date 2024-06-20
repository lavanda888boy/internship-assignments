import DoctorCard from "../components/doctors/DoctorCard";
import { Doctor } from "../models/Doctor";
import usePageTitle from "../hooks/PageTitleHook";
import CreateActionButton from "../components/shared/CreateActionButton";
import { useContext, useEffect, useState } from "react";
import DoctorFormDialog from "../components/doctors/DoctorFormDialog";
import DoctorService from "../api/services/DoctorService";
import { UserRoleContext } from "../context/UserRoleContext";
import {
  Box,
  SelectChangeEvent,
  Pagination,
  MenuItem,
  Typography,
  Select,
  Container,
} from "@mui/material";
import { Department } from "../models/Department";
import DepartmentService from "../api/services/DepartmentService";
import { AxiosError } from "axios";
import MedicalAdviceService from "../api/services/MedicalAdviceService";
import PatientService from "../api/services/PatientService";
import MedicalAdviceDialog from "../components/doctors/MedicalAdviceDialog";

function Doctors() {
  usePageTitle("Doctors");

  const userRoleContextProps = useContext(UserRoleContext);

  const medicalAdviceService: MedicalAdviceService = new MedicalAdviceService();
  const patientService: PatientService = new PatientService();
  const doctorService: DoctorService = new DoctorService();
  const departmentService: DepartmentService = new DepartmentService();

  const [doctors, setDoctors] = useState<Doctor[]>([]);
  const [createFormOpen, setCreateFormOpen] = useState(false);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalItems, setTotalItems] = useState(0);
  const [pageSize, setPageSize] = useState(10);

  const [departments, setDepartments] = useState<Department[]>([]);
  const [selectedDepartment, setSelectedDepartment] = useState("");

  const [advice, setAdvice] = useState<string | null>(null);
  const [adviceDialogOpen, setAdviceDialogOpen] = useState(false);

  useEffect(() => {
    const fetchMedicalAdvice = async () => {
      try {
        if (userRoleContextProps?.userRole === "PatientUser") {
          const patientCredentials =
            userRoleContextProps?.userCredentials.split(" ");

          const patientId =
            await patientService.searchPatientIdByNameAndSurname(
              patientCredentials[0],
              patientCredentials[1]
            );

          const advice = await medicalAdviceService.getMedicalAdviceByPatientId(
            patientId
          );

          setAdvice(advice);
          setAdviceDialogOpen(true);
        }
      } catch (error) {
        console.log(error);
      }
    };

    fetchMedicalAdvice();
  }, []);

  useEffect(() => {
    const fetchDoctors = async () => {
      try {
        let response;
        if (selectedDepartment) {
          response = await doctorService.searchDoctorsByDepartment(
            currentPage,
            pageSize,
            selectedDepartment
          );
        } else {
          response = await doctorService.getAllDoctors(currentPage, pageSize);
        }

        setDoctors(response.items);
        setTotalItems(response.totalItems);
      } catch (error) {
        const err = error as AxiosError;

        if (err.response && err.response.status === 404) {
          setDoctors([]);
        } else console.log(err.message);
      }
    };

    fetchDoctors();
  }, [currentPage, pageSize, selectedDepartment]);

  useEffect(() => {
    const fetchDepartments = async () => {
      try {
        const response = await departmentService.getAllDepartments();
        setDepartments(response);
      } catch (error) {
        console.error("Failed to fetch departments", error);
      }
    };

    fetchDepartments();
  }, []);

  const handleCreateFormOpen = () => {
    setCreateFormOpen(true);
  };

  const handleCreateFormClose = () => {
    setCreateFormOpen(false);
  };

  const handleAddDoctor = (newDoctor: Doctor) => {
    setDoctors((prevDoctors) => [newDoctor, ...prevDoctors]);
  };

  const handleDeleteDoctor = async (selectedDoctor: Doctor) => {
    try {
      if (selectedDoctor) {
        await doctorService.deleteDoctor(selectedDoctor.id);
        setDoctors((prevDoctors) =>
          prevDoctors.filter((d) => d.id !== selectedDoctor.id)
        );
      }
    } catch (error) {
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

  const handleDepartmentChange = (event: SelectChangeEvent<string>) => {
    setSelectedDepartment(event.target.value);
  };

  return (
    <Container
      sx={{
        position: "absolute",
        width: "78.15%",
        height: "auto",
        zIndex: 1,
        padding: "1.5% 1% 2% 1%",
        marginTop: "8%",
        marginLeft: "8%",
        borderRadius: "5px",
        backgroundColor: "white",
      }}
    >
      <Box
        sx={{
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
        }}
      >
        {userRoleContextProps?.userRole === "Admin" && (
          <CreateActionButton
            entityName="Doctor"
            clickAction={handleCreateFormOpen}
          />
        )}
        <Box
          sx={{
            display: "flex",
            alignItems: "center",
          }}
        >
          <Typography sx={{ mr: 1 }}>Department:</Typography>
          <Select
            value={selectedDepartment}
            onChange={handleDepartmentChange}
            displayEmpty
            sx={{
              overflow: "hidden",
              textOverflow: "ellipsis",
              whiteSpace: "nowrap",
            }}
          >
            <MenuItem value="">
              <Typography>All</Typography>
            </MenuItem>
            {departments.map((dept) => (
              <MenuItem key={dept.id} value={dept.name}>
                {dept.name}
              </MenuItem>
            ))}
          </Select>
        </Box>
      </Box>
      <Box
        sx={{
          display: "flex",
          flexWrap: "wrap",
          justifyContent: "center",
          gap: "3%",
        }}
      >
        {doctors.map((doctor, index) => (
          <DoctorCard
            key={index}
            doctor={doctor}
            onDoctorDelete={handleDeleteDoctor}
          />
        ))}
      </Box>
      <Box
        sx={{
          display: "flex",
          alignItems: "center",
          mt: 2,
          ml: 37,
        }}
      >
        <Typography sx={{ mr: 1 }}>Doctors per page:</Typography>
        <Select
          value={pageSize}
          onChange={handlePageSizeChange}
          sx={{ mr: 0.7 }}
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
      <DoctorFormDialog
        isOpened={createFormOpen}
        onClose={handleCreateFormClose}
        onDoctorAdded={handleAddDoctor}
      />
      <MedicalAdviceDialog
        open={adviceDialogOpen}
        onClose={() => setAdviceDialogOpen(false)}
        advice={advice}
      />
    </Container>
  );
}

export default Doctors;

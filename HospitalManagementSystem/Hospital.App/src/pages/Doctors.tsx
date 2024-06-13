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

function Doctors() {
  usePageTitle("Doctors");

  const userRoleContextProps = useContext(UserRoleContext);

  const doctorService: DoctorService = new DoctorService();

  const [doctors, setDoctors] = useState<Doctor[]>([]);
  const [createFormOpen, setCreateFormOpen] = useState(false);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalItems, setTotalItems] = useState(0);
  const [pageSize, setPageSize] = useState(10);

  useEffect(() => {
    const fetchDoctors = async () => {
      try {
        const response = await doctorService.getAllDoctors(
          currentPage,
          pageSize
        );
        setDoctors(response.items);
        setTotalItems(response.totalItems);
      } catch (error) {
        console.log(error);
      }
    };

    fetchDoctors();
  }, [currentPage, pageSize]);

  const handleCreateFormOpen = () => {
    setCreateFormOpen(true);
  };

  const handleCreateFormClose = () => {
    setCreateFormOpen(false);
  };

  const handleAddDoctor = (newDoctor: Doctor) => {
    setDoctors((prevDoctors) => [newDoctor, ...prevDoctors]);
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
      {userRoleContextProps?.userRole === "Admin" && (
        <CreateActionButton
          entityName="Doctor"
          clickAction={handleCreateFormOpen}
        />
      )}
      <Box
        sx={{
          display: "flex",
          flexWrap: "wrap",
          justifyContent: "center",
          gap: "3%",
        }}
      >
        {doctors.map((doctor, index) => (
          <DoctorCard key={index} doctor={doctor} />
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
          sx={{ marginRight: "20px" }}
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
    </Container>
  );
}

export default Doctors;

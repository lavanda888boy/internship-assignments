import { Doctor } from "../../models/Doctor";
import person from "../../assets/person.jpg";
import {
  Card,
  CardActions,
  CardContent,
  CardMedia,
  Divider,
  Box,
  Typography,
  useTheme,
} from "@mui/material";
import React, { useState, useContext } from "react";
import ActionMenu from "../shared/ActionMenu";
import { UserRoleContext } from "../../context/UserRoleContext";

interface DoctorCardProps {
  doctor: Doctor;
  onDoctorDelete: (doctor: Doctor) => void;
}

function DoctorCard({ doctor, onDoctorDelete }: DoctorCardProps) {
  const theme = useTheme();
  const userRoleContextProps = useContext(UserRoleContext);

  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const [selectedDoctor, setSelectedDoctor] = useState<Doctor | null>(null);

  const handleMenuClick = (
    event: React.MouseEvent<HTMLButtonElement>,
    doctor: Doctor
  ) => {
    setAnchorEl(event.currentTarget);
    setSelectedDoctor(doctor);
  };

  const handleMenuClose = () => {
    setAnchorEl(null);
  };

  const handleDeleteDoctor = () => {
    if (selectedDoctor) {
      onDoctorDelete(selectedDoctor);
    }
  };

  return (
    <Card
      sx={{
        width: "20%",
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        borderColor: theme.palette.primary.main,
        borderStyle: "solid",
        borderWidth: "5px",
        borderRadius: "20px",
        padding: "2% 3% 1% 3%",
        mt: 3,
        mb: 0.5,
      }}
    >
      <CardMedia
        component="img"
        sx={{ width: "100px", marginBottom: "5%" }}
        image={person}
        alt={`${doctor.name} ${doctor.surname}`}
      />
      <CardContent sx={{ textAlign: "center", padding: "0" }}>
        <Typography variant="h6" component="div">
          {doctor.name} {doctor.surname}
        </Typography>
        <Typography gutterBottom variant="body1" color="text.secondary">
          {doctor.department}
        </Typography>
        {userRoleContextProps?.userRole !== "PatientUser" && (
          <>
            <Typography variant="body2">
              {doctor.phoneNumber}, {doctor.address}
            </Typography>
            <Divider sx={{ mt: 1, mb: 1 }} />
            {doctor.workingHours && (
              <Box sx={{ marginBottom: "0.5rem" }}>
                <Typography variant="body1">Working Hours:</Typography>
                <Typography variant="body2">
                  {doctor.workingHours.startShift.slice(0, -3)} -{" "}
                  {doctor.workingHours.endShift.slice(0, -3)}
                </Typography>
                <Typography variant="body2">
                  {doctor.workingHours.weekDays.join(", ")}
                </Typography>
              </Box>
            )}
            <Divider sx={{ mt: 1, mb: 1 }} />
            <Typography variant="body1" sx={{ mb: 0.5 }}>
              Patients:
            </Typography>
            {doctor.patients?.map((patient) => (
              <Typography key={patient.id} variant="body2">
                {patient.fullName}
              </Typography>
            ))}
          </>
        )}
      </CardContent>
      {userRoleContextProps?.userRole === "Admin" && (
        <CardActions>
          <ActionMenu
            rowId={doctor.id}
            anchorEl={anchorEl}
            handleMenuClick={(event) => handleMenuClick(event, doctor)}
            handleMenuClose={handleMenuClose}
            onEntityDelete={handleDeleteDoctor}
            doctor={selectedDoctor}
          />
        </CardActions>
      )}
    </Card>
  );
}

export default DoctorCard;

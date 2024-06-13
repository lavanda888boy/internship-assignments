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
import React from "react";
import ActionMenu from "../shared/ActionMenu";

interface DoctorCardProps {
  doctor: Doctor;
}

function DoctorCard({ doctor }: DoctorCardProps) {
  const theme = useTheme();
  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);

  const handleMenuClick = (event: React.MouseEvent<HTMLButtonElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleMenuClose = () => {
    setAnchorEl(null);
  };

  return (
    <Card
      sx={{
        width: "20%",
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        borderColor: theme.palette.primary.light,
        borderStyle: "solid",
        borderWidth: "5px",
        borderRadius: "5px",
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
      </CardContent>
      <CardActions>
        {/* <ActionMenu
          rowId={doctor.id}
          anchorEl={anchorEl}
          handleMenuClick={handleMenuClick}
          handleMenuClose={handleMenuClose}
        /> */}
      </CardActions>
    </Card>
  );
}

export default DoctorCard;

import { Doctor } from "../../models/Doctor";
import person from "../../assets/person.jpg";
import { Card, CardContent, CardMedia, Typography } from "@mui/material";

interface DoctorCardProps {
  doctor: Doctor;
}

function DoctorCard({ doctor }: DoctorCardProps) {
  return (
    <Card
      sx={{
        width: "18%",
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        borderColor: "#B8B8FF",
        borderStyle: "solid",
        borderWidth: "5px",
        borderRadius: "5px",
        padding: "4%",
        marginBottom: "4%",
      }}
    >
      <CardMedia
        component="img"
        sx={{ width: "100px", marginBottom: "5%" }}
        image={person}
        alt={`${doctor.Name} ${doctor.Surname}`}
      />
      <CardContent sx={{ textAlign: "center", padding: "0" }}>
        <Typography gutterBottom variant="h6" component="div">
          {doctor.Name} {doctor.Surname}
        </Typography>
        <Typography variant="body2" color="text.secondary">
          {doctor.Department}
        </Typography>
      </CardContent>
    </Card>
  );
}

export default DoctorCard;

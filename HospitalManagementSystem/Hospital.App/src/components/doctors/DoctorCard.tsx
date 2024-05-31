import { Doctor } from "../../models/Doctor";
import person from "../../assets/person.jpg";
import {
  Card,
  CardActions,
  CardContent,
  CardMedia,
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
        width: "18%",
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        borderColor: theme.palette.primary.light,
        borderStyle: "solid",
        borderWidth: "5px",
        borderRadius: "5px",
        padding: "2% 3% 1% 3%",
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
      <CardActions>
        <ActionMenu
          rowId={doctor.id}
          anchorEl={anchorEl}
          handleMenuClick={handleMenuClick}
          handleMenuClose={handleMenuClose}
        />
      </CardActions>
    </Card>
  );
}

export default DoctorCard;

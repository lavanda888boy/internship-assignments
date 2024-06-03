import { Logout } from "@mui/icons-material";
import {
  Drawer,
  List,
  ListItem,
  ListItemText,
  ListItemButton,
  Box,
  IconButton,
  useTheme,
} from "@mui/material";
import { Link, useNavigate } from "react-router-dom";

interface NavbarProps {
  open: boolean;
  onClose: () => void;
}

function Navbar({ open, onClose }: NavbarProps) {
  const theme = useTheme();
  const navigate = useNavigate();

  const routes = [
    { text: "Patients", path: "/patients" },
    { text: "Doctors", path: "/doctors" },
    { text: "Records", path: "/records" },
  ];

  const handleLogout = () => {
    navigate("/");
  };

  return (
    <Drawer anchor="left" open={open} onClose={onClose}>
      <Box
        sx={{
          display: "flex",
          justifyContent: "center",
          width: 125,
          height: "100%",
          paddingTop: "10%",
          backgroundColor: theme.palette.primary.main,
          color: "white",
          textAlign: "center",
        }}
        role="presentation"
        onClick={onClose}
      >
        <List>
          {routes.map((route) => (
            <ListItem key={route.text} disablePadding sx={{ marginY: 1 }}>
              <ListItemButton component={Link} to={route.path}>
                <ListItemText primary={route.text} />
              </ListItemButton>
            </ListItem>
          ))}
        </List>
      </Box>
      <Box
        sx={{
          height: "10%",
          textAlign: "center",
          backgroundColor: theme.palette.primary.main,
          color: "white",
        }}
      >
        <IconButton color="inherit" onClick={handleLogout}>
          <Logout />
        </IconButton>
      </Box>
    </Drawer>
  );
}

export default Navbar;

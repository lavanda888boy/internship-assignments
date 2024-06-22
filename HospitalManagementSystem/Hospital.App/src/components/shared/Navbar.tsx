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
import AuthService from "../../api/services/AuthService";
import { useContext } from "react";
import { UserRoleContext } from "../../context/UserRoleContext";

interface NavbarProps {
  open: boolean;
  onClose: () => void;
}

function Navbar({ open, onClose }: NavbarProps) {
  const theme = useTheme();
  const userRoleContextProps = useContext(UserRoleContext);
  const navigate = useNavigate();

  const authService: AuthService = new AuthService();

  const handleLogout = () => {
    authService.logout();
    userRoleContextProps?.setUserRole("");
    userRoleContextProps?.setUserCredentials("");
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
          backgroundColor: theme.palette.primary.light,
          color: "white",
          textAlign: "center",
        }}
        role="presentation"
        onClick={onClose}
      >
        <List>
          {userRoleContextProps?.userRole !== "PatientUser" && (
            <ListItem key="Patients" disablePadding sx={{ marginY: 1 }}>
              <ListItemButton component={Link} to="/patients">
                <ListItemText primary="Patients" />
              </ListItemButton>
            </ListItem>
          )}

          <ListItem key="Doctors" disablePadding sx={{ marginY: 1 }}>
            <ListItemButton component={Link} to="/doctors">
              <ListItemText primary="Doctors" />
            </ListItemButton>
          </ListItem>

          <ListItem key="Records" disablePadding sx={{ marginY: 1 }}>
            <ListItemButton component={Link} to="/records">
              <ListItemText primary="Records" />
            </ListItemButton>
          </ListItem>
        </List>
      </Box>
      <Box
        sx={{
          height: "10%",
          textAlign: "center",
          backgroundColor: theme.palette.primary.light,
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

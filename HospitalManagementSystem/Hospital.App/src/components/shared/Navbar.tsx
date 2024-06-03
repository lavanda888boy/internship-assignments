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
import { Link } from "react-router-dom";

interface NavbarProps {
  open: boolean;
  onClose: () => void;
}

function Navbar({ open, onClose }: NavbarProps) {
  const theme = useTheme();

  const routes = [
    { text: "Patients", path: "/patients" },
    { text: "Doctors", path: "/doctors" },
    { text: "Records", path: "/records" },
  ];

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
        <IconButton color="inherit">
          <Logout />
        </IconButton>
      </Box>
    </Drawer>
  );
}

export default Navbar;

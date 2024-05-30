import { Logout } from "@mui/icons-material";
import {
  Drawer,
  List,
  ListItem,
  ListItemText,
  ListItemButton,
  Box,
  IconButton,
} from "@mui/material";

interface NavbarProps {
  open: boolean;
  onClose: () => void;
}

function Navbar({ open, onClose }: NavbarProps) {
  return (
    <Drawer anchor="left" open={open} onClose={onClose}>
      <Box
        sx={{
          display: "flex",
          justifyContent: "center",
          width: 125,
          height: "100%",
          paddingTop: "10%",
          backgroundColor: "#9381ff",
          color: "white",
          textAlign: "center",
        }}
        role="presentation"
        onClick={onClose}
      >
        <List>
          {["Patients", "Doctors", "Records"].map((text) => (
            <ListItem key={text} disablePadding sx={{ marginY: 1 }}>
              <ListItemButton>
                <ListItemText primary={text} />
              </ListItemButton>
            </ListItem>
          ))}
        </List>
      </Box>
      <Box
        sx={{
          height: "10%",
          textAlign: "center",
          backgroundColor: "#9381ff",
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

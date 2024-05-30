import { useContext, useState } from "react";
import {
  AppBar,
  Toolbar,
  IconButton,
  Typography,
  TextField,
  Button,
  Box,
} from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import person from "../../assets/person.jpg";
import { PageContext } from "../../context/PageContext";
import Navbar from "./Navbar";

function Statusbar() {
  const pageContextProps = useContext(PageContext);
  const [navbarOpen, setNavbarOpen] = useState(false);

  const toggleNavbar = (open: boolean) => () => {
    setNavbarOpen(open);
  };
  return (
    <>
      <AppBar>
        <Toolbar
          sx={{
            position: "fixed",
            display: "flex",
            flexDirection: "row",
            width: "82%",
            height: "5%",
            zIndex: 2,
            padding: "1%",
            marginLeft: "8%",
            borderRadius: "5px",
            backgroundColor: "white",
          }}
        >
          <IconButton
            sx={{
              backgroundColor: "#9381ff",
              color: "white",
              "&:hover": {
                backgroundColor: "#B8B8FF",
                color: "white",
              },
            }}
            edge="start"
            aria-label="menu"
            onClick={toggleNavbar(true)}
          >
            <MenuIcon />
          </IconButton>
          <Typography
            variant="h6"
            sx={{ marginLeft: "2%", marginRight: "2%", color: "black" }}
          >
            {pageContextProps?.pageName}
          </Typography>
          <form>
            <TextField
              variant="outlined"
              size="small"
              placeholder="Search smth here"
            />
          </form>
          <Button
            sx={{
              marginLeft: "auto",
              marginRight: "1%",
              padding: "0.5% 1% 0.5% 1%",
              backgroundColor: "#9381ff",
              color: "white",
              "&:hover": {
                backgroundColor: "#B8B8FF",
                color: "white",
              },
            }}
          >
            Login
          </Button>
          <Box
            component="img"
            src={person}
            alt="User"
            sx={{ width: "3%", height: "60%" }}
          />
        </Toolbar>
      </AppBar>
      <Navbar open={navbarOpen} onClose={toggleNavbar(false)} />
    </>
  );
}

export default Statusbar;

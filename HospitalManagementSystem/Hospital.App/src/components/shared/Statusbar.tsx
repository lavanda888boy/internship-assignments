import { useContext, useState } from "react";
import {
  AppBar,
  Toolbar,
  IconButton,
  Typography,
  Box,
  useTheme,
} from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import person from "../../assets/person.jpg";
import { PageContext } from "../../context/PageContext";
import Navbar from "./Navbar";
import { UserRoleContext } from "../../context/UserRoleContext";

function Statusbar() {
  const theme = useTheme();
  const userRoleContextProps = useContext(UserRoleContext);
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
            width: "75%",
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
              backgroundColor: theme.palette.primary.main,
              color: "white",
              "&:hover": {
                backgroundColor: theme.palette.primary.light,
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
          <Typography
            sx={{
              marginLeft: "auto",
              marginRight: "1%",
              backgroundColor: theme.palette.primary.main,
              padding: "0.5%",
              borderRadius: "5px",
              color: "white",
            }}
          >
            {userRoleContextProps?.userCredentials}
          </Typography>
          <Box
            component="img"
            src={person}
            alt="User"
            sx={{ width: "3.5%", height: "60%" }}
          />
        </Toolbar>
      </AppBar>
      <Navbar open={navbarOpen} onClose={toggleNavbar(false)} />
    </>
  );
}

export default Statusbar;

import { createTheme } from "@mui/material/styles";

const theme = createTheme({
  palette: {
    primary: {
      main: "#9381ff",
      light: "#B8B8FF",
    },
    secondary: {
      main: "#FFD8BE",
      light: "#FFEEDD",
    },
  },
  typography: {
    fontFamily: "Roboto",
    fontSize: 17,
  },
});

export default theme;

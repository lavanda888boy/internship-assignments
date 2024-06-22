import { createTheme } from "@mui/material/styles";

const theme = createTheme({
  palette: {
    primary: {
      main: "#7B2CBF",
      light: "#9D4EDD",
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

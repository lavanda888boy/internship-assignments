import Statusbar from "./components/shared/Statusbar";
import Doctors from "./pages/Doctors";
import Patients from "./pages/Patients";
import Records from "./pages/Records";
import { PageContextProvider } from "./context/PageContext";
import { ThemeProvider } from "@mui/material";
import theme from "./theme";
import "./reset.css";

function App() {
  return (
    <ThemeProvider theme={theme}>
      <PageContextProvider>
        <Statusbar />
        <Records />
      </PageContextProvider>
    </ThemeProvider>
  );
}

export default App;

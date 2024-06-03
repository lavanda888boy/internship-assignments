import Statusbar from "./components/shared/Statusbar";
import Doctors from "./pages/Doctors";
import Patients from "./pages/Patients";
import Records from "./pages/Records";
import { PageContextProvider } from "./context/PageContext";
import { ThemeProvider } from "@mui/material";
import theme from "./theme";
import "./reset.css";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Registration from "./pages/Registration";

function App() {
  return (
    <ThemeProvider theme={theme}>
      <PageContextProvider>
        <BrowserRouter>
          <Routes>
            <Route index element={<Registration />} />
            <Route
              element={
                <>
                  <Statusbar />
                  <Route path="/patients" element={<Patients />} />
                  <Route path="/doctors" element={<Doctors />} />
                  <Route path="/records" element={<Records />} />
                </>
              }
            />
          </Routes>
        </BrowserRouter>
      </PageContextProvider>
    </ThemeProvider>
  );
}

export default App;

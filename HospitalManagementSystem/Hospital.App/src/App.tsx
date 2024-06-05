import { PageContextProvider } from "./context/PageContext";
import { ThemeProvider } from "@mui/material";
import theme from "./theme";
import "./reset.css";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Registration from "./pages/Registration";
import Login from "./pages/Login";
import PageLayout from "./PageLayout";

function App() {
  return (
    <ThemeProvider theme={theme}>
      <PageContextProvider>
        <BrowserRouter>
          <Routes>
            <Route index element={<Login />} />
            <Route path="/registration" element={<Registration />} />
            <Route path="*" element={<PageLayout />} />
          </Routes>
        </BrowserRouter>
      </PageContextProvider>
    </ThemeProvider>
  );
}

export default App;

import Statusbar from "./components/shared/Statusbar";
import Doctors from "./pages/Doctors";
import Patients from "./pages/Patients";
import Records from "./pages/Records";
import "./reset.css";
import { Routes, Route } from "react-router-dom";

function PageLayout() {
  return (
    <>
      <Statusbar />
      <Routes>
        <Route path="/patients" element={<Patients />} />
        <Route path="/doctors" element={<Doctors />} />
        <Route path="/records" element={<Records />} />
      </Routes>
    </>
  );
}

export default PageLayout;

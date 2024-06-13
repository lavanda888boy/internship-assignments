import Statusbar from "../components/shared/Statusbar";
import Doctors from "../pages/Doctors";
import Patients from "../pages/Patients";
import Records from "../pages/Records";
import ProtectedRoute from "./ProtectedRoute";
import "../reset.css";
import { Routes, Route } from "react-router-dom";

function PageLayout() {
  return (
    <>
      <Statusbar />
      <Routes>
        <Route
          path="/patients"
          element={
            <ProtectedRoute
              element={<Patients />}
              requiredRoles={["Admin", "DoctorUser"]}
            />
          }
        />
        <Route
          path="/doctors"
          element={
            <ProtectedRoute
              element={<Doctors />}
              requiredRoles={["Admin", "PatientUser"]}
            />
          }
        />
        <Route
          path="/records"
          element={
            <ProtectedRoute
              element={<Records />}
              requiredRoles={["Admin", "DoctorUser", "PatientUser"]}
            />
          }
        />
      </Routes>
    </>
  );
}

export default PageLayout;

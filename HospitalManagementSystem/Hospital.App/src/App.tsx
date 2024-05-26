import Navbar from "./components/shared/Navbar";
import "./reset.css";
import "./App.css";
import Statusbar from "./components/shared/Statusbar";
import Doctors from "./pages/Doctors";
import Patients from "./pages/Patients";

function App() {
  return (
    <>
      <Statusbar />
      <Navbar />
      <Patients />
    </>
  );
}

export default App;

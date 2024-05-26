import Navbar from "./components/shared/Navbar";
import "./reset.css";
import "./App.css";
import Statusbar from "./components/shared/Statusbar";
import Doctors from "./pages/Doctors";
import Patients from "./pages/Patients";
import Records from "./pages/Records";

function App() {
  return (
    <>
      <Statusbar />
      <Navbar />
      <Records />
    </>
  );
}

export default App;

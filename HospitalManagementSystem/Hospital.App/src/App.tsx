import "./reset.css";
import "./App.css";
import Statusbar from "./components/shared/Statusbar";
import Doctors from "./pages/Doctors";
import Patients from "./pages/Patients";
import Records from "./pages/Records";
import { PageContextProvider } from "./context/PageContext";

function App() {
  return (
    <PageContextProvider>
      <Statusbar />
      <Patients />
    </PageContextProvider>
  );
}

export default App;

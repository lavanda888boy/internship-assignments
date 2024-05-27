import "./Statusbar.css";
import person from "../../assets/person.jpg";
import { useContext } from "react";
import { PageContext } from "../../context/PageContext";

function Statusbar() {
  const pageContextProps = useContext(PageContext);

  return (
    <>
      <header className="page-header">
        <h1 className="header-name">{pageContextProps?.pageName}</h1>
        <form>
          <input
            type="text"
            className="header-searchbar"
            name="search"
            placeholder="Search smth here"
          />
        </form>
        <p className="header-userinfo">Name Surname</p>
        <img src={person} className="header-userimage" />
      </header>
    </>
  );
}

export default Statusbar;

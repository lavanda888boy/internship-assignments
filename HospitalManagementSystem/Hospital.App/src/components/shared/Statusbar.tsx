import "./Statusbar.css";
import person from "../../assets/person.jpg";

function Statusbar() {
  return (
    <>
      <header className="page-header">
        <h1 className="header-name">Dashboard</h1>
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

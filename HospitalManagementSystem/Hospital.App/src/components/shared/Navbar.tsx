import "./Navbar.css";

function Navbar() {
  return (
    <>
      <aside className="page-sidebar">
        <a href="" className="sidebar-action">
          Patients
        </a>
        <a href="" className="sidebar-action">
          Doctors
        </a>
        <a href="" className="sidebar-action">
          Records
        </a>
        <a href="" className="sidebar-logout-action">
          Logout
        </a>
      </aside>
    </>
  );
}

export default Navbar;

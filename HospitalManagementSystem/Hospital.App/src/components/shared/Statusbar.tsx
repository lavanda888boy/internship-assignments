import "./Statusbar.css";
import person from "../../assets/person.jpg";
import React, { useContext, useState } from "react";
import { PageContext } from "../../context/PageContext";

function Statusbar() {
  const pageContextProps = useContext(PageContext);

  const [loginButton, setLoginButton] = useState(false);
  const [loginForm, setLoginForm] = useState(false);
  const [userName, setUserName] = useState("");
  const [userSurname, setUserSurname] = useState("");

  const handleLoginButtonClick = () => {
    setLoginForm(!loginForm);
  };

  const handleLoginFormSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    setLoginButton(true);
    setLoginForm(false);
  };

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
        {loginButton ? (
          <p className="header-userinfo">
            {userName} {userSurname}
          </p>
        ) : (
          <button className="header-login" onClick={handleLoginButtonClick}>
            Login
          </button>
        )}
        <img src={person} className="header-userimage" />
        {loginForm && (
          <div className="login-form-wrapper">
            <form className="login-form" onSubmit={handleLoginFormSubmit}>
              <input
                type="text"
                value={userName}
                placeholder="Name"
                onChange={(e) => setUserName(e.target.value)}
              />
              <input
                type="text"
                value={userSurname}
                onChange={(e) => setUserSurname(e.target.value)}
                placeholder="Surname"
              />
              <button type="submit" className="login-form-submit">
                Submit
              </button>
            </form>
          </div>
        )}
      </header>
    </>
  );
}

export default Statusbar;

import { createContext, useState, ReactNode } from "react";
import AuthService from "../api/services/AuthService";

interface UserRoleContextProps {
  userRole: string;
  setUserRole: (userRole: string) => void;
}

export const UserRoleContext = createContext<UserRoleContextProps | undefined>(
  undefined
);

interface UserRoleContextProviderProps {
  children: ReactNode;
}

export const UserRoleContextProvider = ({
  children,
}: UserRoleContextProviderProps) => {
  const authService: AuthService = new AuthService();

  const [userRole, setUserRole] = useState(
    authService.getUserRoleFromToken(localStorage.getItem("access-token") || "")
  );

  return (
    <UserRoleContext.Provider value={{ userRole, setUserRole }}>
      {children}
    </UserRoleContext.Provider>
  );
};

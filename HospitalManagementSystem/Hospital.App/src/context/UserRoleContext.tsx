import { createContext, useState, ReactNode } from "react";

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
  const [userRole, setUserRole] = useState("");

  return (
    <UserRoleContext.Provider value={{ userRole, setUserRole }}>
      {children}
    </UserRoleContext.Provider>
  );
};

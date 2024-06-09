import { ReactNode, useContext } from "react";
import { Navigate } from "react-router-dom";
import { UserRoleContext } from "../context/UserRoleContext";

interface ProtectedRouteProps {
  element: ReactNode;
  requiredRoles: string[];
}

function ProtectedRoute({ element, requiredRoles }: ProtectedRouteProps) {
  const userRoleContextProps = useContext(UserRoleContext);
  if (userRoleContextProps) {
    if (
      requiredRoles.length > 0 &&
      !requiredRoles.includes(userRoleContextProps.userRole)
    ) {
      return <Navigate to="/" />;
    }
  }

  return element;
}

export default ProtectedRoute;

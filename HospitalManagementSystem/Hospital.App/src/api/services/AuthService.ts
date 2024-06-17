import api from "../axios";
import { jwtDecode, JwtPayload } from "jwt-decode";

interface RegisterUserDto {
  name: string;
  surname: string;
  email: string;
  password: string;
  role: string;
}

interface LoginUserDto {
  email: string;
  password: string;
}

interface CustomJwtPayload extends JwtPayload {
  "http://schemas.microsoft.com/ws/2008/06/identity/claims/role": string;
}

class AuthService {
  public async register(user: RegisterUserDto): Promise<string> {
    const response = await api.post("/Auth/Register", user);
    const token = response.data;

    localStorage.setItem("access-token", token);
    return this.getUserRoleFromToken(token);
  }

  public async login(user: LoginUserDto): Promise<string> {
    const response = await api.post("/Auth/Login", user);
    const token = response.data;

    localStorage.setItem("access-token", token);
    return this.getUserRoleFromToken(token);
  }

  public logout() {
    localStorage.removeItem("access-token");
  }

  public getUserRoleFromToken(token: string): string {
    if (!token) return "";

    try {
      const decodedToken = jwtDecode<CustomJwtPayload>(token);
      return decodedToken[
        "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
      ];
    } catch (error) {
      console.error("Invalid token:", error);
      return "";
    }
  }
}

export default AuthService;

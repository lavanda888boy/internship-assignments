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
    try {
      const response = await api.post("/Auth/Register", user);
      const token = response.data;

      localStorage.setItem("access-token", token);
      return this.getUserRoleFromToken(token);
    } catch (error) {
      throw error;
    }
  }

  public async login(user: LoginUserDto): Promise<string> {
    try {
      const response = await api.post("/Auth/Login", user);
      const token = response.data;

      localStorage.setItem("access-token", token);
      return this.getUserRoleFromToken(token);
    } catch (error) {
      throw error;
    }
  }

  public logout() {
    localStorage.removeItem("access-token");
  }

  private getUserRoleFromToken(token: string): string {
    const decodedToken = jwtDecode(token) as CustomJwtPayload;
    const userRole =
      decodedToken[
        "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
      ];
    return userRole;
  }
}

export default new AuthService();

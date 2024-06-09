import axios from "../axios";

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

class AuthService {
  public async register(user: RegisterUserDto) {
    try {
      const response = await axios.post("/Auth/Register", user);
      const token = response.data;
      localStorage.setItem("access-token", token);
    } catch (error) {
      throw error;
    }
  }

  public async login(user: LoginUserDto) {
    try {
      const response = await axios.post("/Auth/Login", user);
      const token = response.data;
      localStorage.setItem("access-token", token);
    } catch (error) {
      throw error;
    }
  }

  public logout() {
    localStorage.removeItem("access-token");
  }
}

export default new AuthService();

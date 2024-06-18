import axios, {
  AxiosError,
  AxiosResponse,
  InternalAxiosRequestConfig,
} from "axios";
import { ReactNode, useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { UserRoleContext } from "../context/UserRoleContext";

const api = axios.create({
  baseURL: "http://localhost:5000/api",
  headers: {
    "Content-Type": "application/json",
  },
});

const AxiosInterceptorWrapper = ({ children }: { children: ReactNode }) => {
  const userRoleContextProps = useContext(UserRoleContext);
  const navigate = useNavigate();

  const [interceptorsSet, setInterceptorsSet] = useState(false);

  useEffect(() => {
    const requestInterceptor = (config: InternalAxiosRequestConfig) => {
      const token = localStorage.getItem("access-token");
      if (token) {
        config.headers.Authorization = `Bearer ${token}`;
      }
      return config;
    };

    const responseInterceptor = (response: AxiosResponse) => {
      return response;
    };

    const errorInterceptor = (error: AxiosError) => {
      if (error.response && error.response.status === 401) {
        userRoleContextProps?.setUserRole("");
        userRoleContextProps?.setUserCredentials("");
        navigate("/");
      }
      return Promise.reject(error);
    };

    const reqInterceptor = api.interceptors.request.use(
      requestInterceptor,
      (error) => Promise.reject(error)
    );

    const resInterceptor = api.interceptors.response.use(
      responseInterceptor,
      errorInterceptor
    );

    setInterceptorsSet(true);

    return () => {
      api.interceptors.request.eject(reqInterceptor);
      api.interceptors.response.eject(resInterceptor);
    };
  }, []);

  return interceptorsSet && children;
};

export default api;
export { AxiosInterceptorWrapper };

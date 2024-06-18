import { useFormik } from "formik";
import * as Yup from "yup";
import {
  TextField,
  Typography,
  Box,
  Button,
  useTheme,
  InputLabel,
} from "@mui/material";
import usePageTitle from "../hooks/PageTitleHook";
import { useNavigate, Link } from "react-router-dom";
import AuthService from "../api/services/AuthService";
import { useContext } from "react";
import { UserRoleContext } from "../context/UserRoleContext";

function Login() {
  usePageTitle("Login");

  const authService: AuthService = new AuthService();

  const theme = useTheme();
  const userRoleContextProps = useContext(UserRoleContext);
  const navigate = useNavigate();

  const formik = useFormik({
    initialValues: {
      email: "",
      password: "",
    },

    validationSchema: Yup.object({
      email: Yup.string()
        .email("Invalid email address")
        .required("Email is required"),
      password: Yup.string().required("Password is required"),
    }),

    onSubmit: async (values) => {
      try {
        const user = {
          email: values.email,
          password: values.password,
        };

        const userData = await authService.login(user);
        userRoleContextProps?.setUserRole(userData[0]);
        userRoleContextProps?.setUserCredentials(userData[1]);
        navigate("/doctors");
      } catch (error) {
        console.log(error);
      }
    },
  });

  return (
    <Box
      sx={{
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        justifyContent: "center",
        minHeight: "100vh",
        backgroundColor: theme.palette.secondary.main,
      }}
    >
      <Box
        component="form"
        sx={{
          display: "flex",
          flexDirection: "column",
          width: "100%",
          maxWidth: 400,
          padding: 3,
          backgroundColor: "white",
          borderRadius: 3,
          border: "5px solid " + theme.palette.primary.light,
          boxShadow: 1,
        }}
        onSubmit={formik.handleSubmit}
      >
        <Typography variant="h5" sx={{ mb: 2, textAlign: "center" }}>
          Login
        </Typography>
        <InputLabel htmlFor="email">Email</InputLabel>
        <TextField
          id="email"
          type="email"
          value={formik.values.email}
          placeholder="Enter your email address"
          onChange={formik.handleChange}
          onBlur={formik.handleBlur}
          error={formik.touched.email && Boolean(formik.errors.email)}
          helperText={formik.touched.email && formik.errors.email}
          fullWidth
          sx={{ mt: 0, mb: 1 }}
        />
        <InputLabel htmlFor="password">Password</InputLabel>
        <TextField
          id="password"
          type="password"
          value={formik.values.password}
          placeholder="Enter your password"
          onChange={formik.handleChange}
          onBlur={formik.handleBlur}
          error={formik.touched.password && Boolean(formik.errors.password)}
          helperText={formik.touched.password && formik.errors.password}
          fullWidth
          sx={{ mt: 0, mb: 1 }}
        />
        <Button
          type="submit"
          variant="contained"
          color="primary"
          sx={{ mt: 2, mx: 15 }}
        >
          Login
        </Button>
        <Typography sx={{ mt: 2, textAlign: "center" }} variant="body1">
          Don't have an account?{" "}
          <Link to={"/registration"}>
            <Typography variant="body1" sx={{ display: "inline" }}>
              Click here to register
            </Typography>
          </Link>
        </Typography>
      </Box>
    </Box>
  );
}

export default Login;

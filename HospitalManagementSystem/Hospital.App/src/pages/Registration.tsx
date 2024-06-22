import { useFormik } from "formik";
import * as Yup from "yup";
import { TextField, Typography, Box, Button, InputLabel } from "@mui/material";
import usePageTitle from "../hooks/PageTitleHook";
import { useNavigate } from "react-router-dom";
import AuthService from "../api/services/AuthService";
import { useContext, useState } from "react";
import { UserRoleContext } from "../context/UserRoleContext";
import { NotificationState } from "../models/utils/NotificationState";
import ActionResultNotification from "../components/shared/ActionResultNotification";

function Registration() {
  usePageTitle("Register");

  const authService: AuthService = new AuthService();

  const userRoleContextProps = useContext(UserRoleContext);
  const navigate = useNavigate();

  const [notification, setNotification] = useState<NotificationState>({
    open: false,
    message: "",
    severity: "error",
  });

  const formik = useFormik({
    initialValues: {
      name: "",
      surname: "",
      email: "",
      password: "",
      confirmPassword: "",
    },

    validationSchema: Yup.object({
      name: Yup.string()
        .max(50, "The name must be no longer than 50 characters")
        .required("The name is required"),
      surname: Yup.string()
        .max(50, "The surname must be no longer than 50 characters")
        .required("The surname is required"),
      email: Yup.string()
        .email("Invalid email address")
        .required("Email is required"),
      password: Yup.string()
        .min(10, "Password must be at least 10 characters long")
        .matches(/[0-9]/, "Password must contain at least one digit")
        .matches(/[a-z]/, "Password must contain at least one lowercase letter")
        .matches(/[A-Z]/, "Password must contain at least one uppercase letter")
        .required("Password is required"),
      confirmPassword: Yup.string()
        .oneOf([Yup.ref("password"), undefined], "Passwords must match")
        .required("Confirm password is required"),
    }),

    onSubmit: async (values) => {
      try {
        const user = {
          name: values.name,
          surname: values.surname,
          email: values.email,
          password: values.password,
          role: "PatientUser",
        };

        const userData = await authService.register(user);
        userRoleContextProps?.setUserRole(userData[0]);
        userRoleContextProps?.setUserCredentials(userData[1]);

        setNotification({
          open: true,
          message: "Registration was successful!",
          severity: "success",
        });

        setTimeout(() => {
          navigate("/");
        }, 2000);
      } catch (error) {
        setNotification({
          open: true,
          message: "Registration failed. The user might already exist.",
          severity: "error",
        });

        console.log(error);
      }
    },
  });

  const handleCloseNotification = () => {
    setNotification((prev: NotificationState) => ({ ...prev, open: false }));
  };

  return (
    <Box
      sx={{
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        justifyContent: "center",
        minHeight: "100vh",
        backgroundImage: `url("src/assets/login_image.jpg")`,
      }}
    >
      <ActionResultNotification
        state={notification}
        onClose={handleCloseNotification}
      />
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
          boxShadow: 1,
        }}
        onSubmit={formik.handleSubmit}
      >
        <Typography variant="h5" sx={{ mb: 2, textAlign: "center" }}>
          Registration
        </Typography>
        <InputLabel htmlFor="name">Name</InputLabel>
        <TextField
          id="name"
          value={formik.values.name}
          placeholder="Enter your name"
          onChange={formik.handleChange}
          onBlur={formik.handleBlur}
          error={formik.touched.name && Boolean(formik.errors.name)}
          helperText={formik.touched.name && formik.errors.name}
          fullWidth
          sx={{ mt: 0, mb: 1 }}
        />
        <InputLabel htmlFor="surname">Surname</InputLabel>
        <TextField
          id="surname"
          value={formik.values.surname}
          placeholder="Enter your surname"
          onChange={formik.handleChange}
          onBlur={formik.handleBlur}
          error={formik.touched.surname && Boolean(formik.errors.surname)}
          helperText={formik.touched.surname && formik.errors.surname}
          fullWidth
          sx={{ mt: 0, mb: 1 }}
        />
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
        <InputLabel htmlFor="confirmPassword">Confirm password</InputLabel>
        <TextField
          id="confirmPassword"
          type="password"
          value={formik.values.confirmPassword}
          placeholder="Repeat the password"
          onChange={formik.handleChange}
          onBlur={formik.handleBlur}
          error={
            formik.touched.confirmPassword &&
            Boolean(formik.errors.confirmPassword)
          }
          helperText={
            formik.touched.confirmPassword && formik.errors.confirmPassword
          }
          fullWidth
          sx={{ mt: 0, mb: 1 }}
        />
        <Button
          type="submit"
          variant="contained"
          color="primary"
          sx={{ mt: 2, mx: 15 }}
        >
          Register
        </Button>
      </Box>
    </Box>
  );
}

export default Registration;

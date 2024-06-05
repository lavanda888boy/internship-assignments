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

function Login() {
  usePageTitle("Login");

  const theme = useTheme();
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
      password: Yup.string()
        .min(10, "Password must be at least 10 characters long")
        .matches(/[0-9]/, "Password must contain at least one digit")
        .matches(/[a-z]/, "Password must contain at least one lowercase letter")
        .matches(/[A-Z]/, "Password must contain at least one uppercase letter")
        .required("Password is required"),
    }),

    onSubmit: () => {
      navigate("/doctors");
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
          name="email"
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
          name="password"
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

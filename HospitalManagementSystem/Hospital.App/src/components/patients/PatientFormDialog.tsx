import { useFormik } from "formik";
import * as Yup from "yup";
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogContentText,
  Box,
  TextField,
  Button,
  RadioGroup,
  FormControlLabel,
  Radio,
  InputLabel,
} from "@mui/material";

interface PatientFormDialogProps {
  open: boolean;
  onClose: () => void;
}

function PatientFormDialog({ open, onClose }: PatientFormDialogProps) {
  const formik = useFormik({
    initialValues: {
      name: "",
      surname: "",
      age: 1,
      gender: "",
      address: "",
      phoneNumber: "",
      insuranceNumber: "",
    },

    validationSchema: Yup.object({
      name: Yup.string()
        .min(1, "Name must be at least 1 character")
        .max(50, "Name must be no longer than 50 characters")
        .required("Name is required"),
      surname: Yup.string()
        .min(1, "Surname must be at least 1 character")
        .max(50, "Surname must be no longer than 50 characters")
        .required("Surname is required"),
      age: Yup.number()
        .min(1, "Age must be at least 1")
        .max(120, "Age must be no more than 120")
        .required("Age is required"),
      gender: Yup.string()
        .oneOf(["M", "F", "Other"], "Gender must be one of: M, F, Other")
        .required("Gender is required"),
      address: Yup.string()
        .min(5, "Address must be at least 5 characters")
        .required("Address is required"),
      phoneNumber: Yup.string().matches(
        /^\+?(\d{1,3})?[-.●]?((\d{1,4}))?[-.●]?(\d{1,4})[-.●]?(\d{1,9})$/,
        "Phone number is not valid"
      ),
      insuranceNumber: Yup.string(),
    }),

    onSubmit: () => {
      onClose();
    },
  });

  return (
    <Dialog open={open} onClose={onClose}>
      <DialogTitle>Add Patient</DialogTitle>
      <DialogContent>
        <DialogContentText>
          Please fill out the form below to add a new patient.
        </DialogContentText>
        <Box
          component="form"
          sx={{
            display: "flex",
            flexDirection: "column",
            padding: "3% 3% 0% 3%",
            backgroundColor: "white",
          }}
          onSubmit={formik.handleSubmit}
        >
          <InputLabel htmlFor="name">Name</InputLabel>
          <TextField
            name="name"
            value={formik.values.name}
            onChange={formik.handleChange}
            onBlur={formik.handleBlur}
            error={formik.touched.name && Boolean(formik.errors.name)}
            helperText={formik.touched.name && formik.errors.name}
            fullWidth
            sx={{ mt: 1, mb: 1 }}
          />
          <InputLabel htmlFor="surname">Surname</InputLabel>
          <TextField
            name="surname"
            value={formik.values.surname}
            onChange={formik.handleChange}
            onBlur={formik.handleBlur}
            error={formik.touched.surname && Boolean(formik.errors.surname)}
            helperText={formik.touched.surname && formik.errors.surname}
            fullWidth
            sx={{ mt: 1, mb: 1 }}
          />
          <InputLabel htmlFor="age">Age</InputLabel>
          <TextField
            name="age"
            type="number"
            value={formik.values.age}
            onChange={formik.handleChange}
            onBlur={formik.handleBlur}
            error={formik.touched.age && Boolean(formik.errors.age)}
            helperText={formik.touched.age && formik.errors.age}
            fullWidth
            sx={{ mt: 1, mb: 1 }}
          />
          <InputLabel htmlFor="gender">Gender</InputLabel>
          <RadioGroup
            name="gender"
            value={formik.values.gender}
            onChange={formik.handleChange}
            sx={{ mb: 1 }}
          >
            <FormControlLabel value="M" control={<Radio />} label="Male" />
            <FormControlLabel value="F" control={<Radio />} label="Female" />
            <FormControlLabel value="Other" control={<Radio />} label="Other" />
          </RadioGroup>
          {formik.touched.gender && formik.errors.gender && (
            <div style={{ color: "red", marginBottom: "10px" }}>
              {formik.errors.gender}
            </div>
          )}
          <InputLabel htmlFor="address">Address</InputLabel>
          <TextField
            name="address"
            value={formik.values.address}
            onChange={formik.handleChange}
            onBlur={formik.handleBlur}
            error={formik.touched.address && Boolean(formik.errors.address)}
            helperText={formik.touched.address && formik.errors.address}
            fullWidth
            sx={{ mt: 1, mb: 1 }}
          />
          <InputLabel htmlFor="phoneNumber">Phone number</InputLabel>
          <TextField
            name="phoneNumber"
            value={formik.values.phoneNumber}
            onChange={formik.handleChange}
            onBlur={formik.handleBlur}
            error={
              formik.touched.phoneNumber && Boolean(formik.errors.phoneNumber)
            }
            helperText={formik.touched.phoneNumber && formik.errors.phoneNumber}
            fullWidth
            sx={{ mt: 1, mb: 1 }}
          />
          <InputLabel htmlFor="insuranceNumber">Insurance number</InputLabel>
          <TextField
            name="insuranceNumber"
            value={formik.values.insuranceNumber}
            onChange={formik.handleChange}
            onBlur={formik.handleBlur}
            error={
              formik.touched.insuranceNumber &&
              Boolean(formik.errors.insuranceNumber)
            }
            helperText={
              formik.touched.insuranceNumber && formik.errors.insuranceNumber
            }
            fullWidth
            sx={{ mt: 1, mb: 1 }}
          />
          <Button
            type="submit"
            variant="contained"
            color="primary"
            sx={{ mt: 2, mx: 12 }}
          >
            Add Patient
          </Button>
          <Button onClick={onClose} color="primary" sx={{ mt: 1 }}>
            Cancel
          </Button>
        </Box>
      </DialogContent>
    </Dialog>
  );
}

export default PatientFormDialog;

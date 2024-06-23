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
  Typography,
} from "@mui/material";
import PatientService from "../../api/services/PatientService";
import { Patient } from "../../models/Patient";
import { NotificationState } from "../../models/utils/NotificationState";
import { useState } from "react";
import ActionResultNotification from "../shared/ActionResultNotification";

interface PatientFormDialogProps {
  isOpened: boolean;
  onClose: () => void;
  onPatientAdded?: (patient: Patient) => void;
  patient?: Patient;
  onPatientUpdated?: () => void;
}

type NewPatientData = Omit<Patient, "id">;

function PatientFormDialog({
  isOpened: open,
  onClose,
  onPatientAdded,
  patient,
  onPatientUpdated,
}: PatientFormDialogProps) {
  const patientService: PatientService = new PatientService();

  const [notification, setNotification] = useState<NotificationState>({
    open: false,
    message: "",
    severity: "error",
  });

  const formik = useFormik({
    initialValues: {
      name: patient?.name || "",
      surname: patient?.surname || "",
      age: patient?.age || 1,
      gender: patient?.gender || "",
      address: patient?.address || "",
      phoneNumber: patient?.phoneNumber || "",
      insuranceNumber: patient?.insuranceNumber || "",
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

    onSubmit: async (values, { resetForm }) => {
      try {
        const patientData: NewPatientData = {
          name: values.name,
          surname: values.surname,
          age: values.age,
          gender: values.gender,
          address: values.address,
          phoneNumber: values.phoneNumber || undefined,
          insuranceNumber: values.insuranceNumber || undefined,
        };

        if (patient) {
          await patientService.updatePatient(patientData, patient.id);
          onPatientUpdated && onPatientUpdated();
        } else {
          const id = await patientService.addPatient(patientData);
          const newPatient: Patient = {
            id: id,
            ...patientData,
          };
          onPatientAdded && onPatientAdded(newPatient);
        }

        resetForm();
        onClose();
      } catch (error) {
        setNotification({
          open: true,
          message: "Failed to introduce patient information.",
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
    <Dialog open={open} onClose={onClose}>
      <ActionResultNotification
        state={notification}
        onClose={handleCloseNotification}
      />
      <DialogTitle>Patient registration</DialogTitle>
      <DialogContent>
        <DialogContentText>
          Please fill out the form below to add a new patient or update an
          existing one.
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
            id="name"
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
            id="surname"
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
            id="age"
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
            id="gender"
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
            <Typography style={{ color: "red", marginBottom: "10px" }}>
              {formik.errors.gender}
            </Typography>
          )}
          <InputLabel htmlFor="address">Address</InputLabel>
          <TextField
            id="address"
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
            id="phoneNumber"
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
            id="insuranceNumber"
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
            sx={{ mt: 2, mx: 20 }}
          >
            Submit form
          </Button>
          <Button onClick={onClose} color="primary" sx={{ mt: 1, mx: 20 }}>
            Cancel
          </Button>
        </Box>
      </DialogContent>
    </Dialog>
  );
}

export default PatientFormDialog;

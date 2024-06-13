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
  InputLabel,
  FormControlLabel,
  Checkbox,
  Typography,
} from "@mui/material";
import { Doctor } from "../../models/Doctor";
import DoctorService from "../../api/services/DoctorService";
import { AxiosError } from "axios";
import { useContext } from "react";
import { UserRoleContext } from "../../context/UserRoleContext";
import { useNavigate } from "react-router-dom";

const daysOfWeek = [
  { id: 1, name: "Monday" },
  { id: 2, name: "Tuesday" },
  { id: 3, name: "Wednesday" },
  { id: 4, name: "Thursday" },
  { id: 5, name: "Friday" },
  { id: 6, name: "Saturday" },
  { id: 7, name: "Sunday" },
];

interface DoctorFormDialogProps {
  open: boolean;
  onClose: () => void;
  onDoctorAdded?: (doctor: Doctor) => void;
  doctor?: Doctor;
}

interface NewDoctorData {
  name: string;
  surname: string;
  address: string;
  phoneNumber: string;
  departmentId: number;
  startShift: string;
  endShift: string;
  weekDayIds: number[];
}

function DoctorFormDialog({
  open,
  onClose,
  onDoctorAdded,
  doctor,
}: DoctorFormDialogProps) {
  const userRoleContextProps = useContext(UserRoleContext);
  const navigate = useNavigate();

  const doctorService: DoctorService = new DoctorService();

  const formik = useFormik({
    initialValues: {
      name: "",
      surname: "",
      address: "",
      phoneNumber: "",
      department: "",
      startShift: "",
      endShift: "",
      weekDays: [] as number[],
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
      address: Yup.string()
        .min(5, "Address must be at least 5 characters")
        .required("Address is required"),
      phoneNumber: Yup.string()
        .matches(
          /^\+?(\d{1,3})?[-.●]?((\d{1,4}))?[-.●]?(\d{1,4})[-.●]?(\d{1,9})$/,
          "Phone number is not valid"
        )
        .required("Phone number is required"),
      department: Yup.string()
        .max(30, "Department name must be no longer than 50 characters")
        .required("Department name is required"),
      startShift: Yup.string().required("Start shift is required"),
      endShift: Yup.string().required("End shift is required"),
      weekDays: Yup.array().min(1, "Select at least one week day"),
    }),

    onSubmit: async (values, { resetForm }) => {
      try {
        const doctorData: NewDoctorData = {
          name: values.name,
          surname: values.surname,
          address: values.address,
          phoneNumber: values.phoneNumber,
          startShift: values.startShift,
          endShift: values.endShift,
          weekDayIds: values.weekDays,
        };

        if (doctor) {
          const id = await doctorService.updateDoctor(doctorData, doctor.id);
          //onPatientUpdated && onPatientUpdated(id);
        } else {
          const id = await doctorService.addDoctor(doctorData);

          try {
            const newDoctor = await doctorService.getDoctorById(id);
            onDoctorAdded && onDoctorAdded(newDoctor);
          } catch (error) {
            throw error;
          }
        }

        resetForm();
        onClose();
      } catch (error) {
        const err = error as AxiosError;
        if (err.response && err.response.status === 401) {
          userRoleContextProps?.setUserRole("");
          navigate("/");
        }
        console.log(err.message);
      }
    },
  });

  const handleWeekdayChange = (dayId: number) => {
    const updatedWeekDays = formik.values.weekDays.includes(dayId)
      ? formik.values.weekDays.filter((id) => id !== dayId)
      : [...formik.values.weekDays, dayId];
    formik.setFieldValue("weekDays", updatedWeekDays);
  };

  return (
    <Dialog open={open} onClose={onClose}>
      <DialogTitle>Add Doctor</DialogTitle>
      <DialogContent>
        <DialogContentText>
          Please fill out the form below to add a new doctor.
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
          <InputLabel htmlFor="departmentId">Department</InputLabel>
          <TextField
            id="department"
            value={formik.values.department}
            onChange={formik.handleChange}
            onBlur={formik.handleBlur}
            error={
              formik.touched.department && Boolean(formik.errors.department)
            }
            helperText={formik.touched.department && formik.errors.department}
            fullWidth
            sx={{ mt: 1, mb: 1 }}
          />
          <InputLabel htmlFor="startShift">Start shift</InputLabel>
          <TextField
            id="startShift"
            type="time"
            value={formik.values.startShift}
            onChange={formik.handleChange}
            onBlur={formik.handleBlur}
            error={
              formik.touched.startShift && Boolean(formik.errors.startShift)
            }
            helperText={formik.touched.startShift && formik.errors.startShift}
            fullWidth
            sx={{ mt: 1, mb: 1 }}
          />
          <InputLabel htmlFor="endShift">End shift</InputLabel>
          <TextField
            id="endShift"
            type="time"
            value={formik.values.endShift}
            onChange={formik.handleChange}
            onBlur={formik.handleBlur}
            error={formik.touched.endShift && Boolean(formik.errors.endShift)}
            helperText={formik.touched.endShift && formik.errors.endShift}
            fullWidth
            sx={{ mt: 1, mb: 1 }}
          />
          <InputLabel>Week Days</InputLabel>
          {daysOfWeek.map((day) => (
            <FormControlLabel
              key={day.id}
              control={
                <Checkbox
                  checked={formik.values.weekDays.includes(day.id)}
                  onChange={() => handleWeekdayChange(day.id)}
                />
              }
              label={day.name}
            />
          ))}
          {formik.touched.weekDays && formik.errors.weekDays && (
            <Typography style={{ color: "red", marginBottom: "10px" }}>
              {formik.errors.weekDays}
            </Typography>
          )}
          <Button
            type="submit"
            variant="contained"
            color="primary"
            sx={{ mt: 2, mx: 12 }}
          >
            Add Doctor
          </Button>
          <Button onClick={onClose} color="primary" sx={{ mt: 1, mx: 12 }}>
            Cancel
          </Button>
        </Box>
      </DialogContent>
    </Dialog>
  );
}

export default DoctorFormDialog;

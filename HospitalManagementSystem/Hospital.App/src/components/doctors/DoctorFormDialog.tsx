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
  Select,
  MenuItem,
} from "@mui/material";
import { Doctor } from "../../models/Doctor";
import DoctorService from "../../api/services/DoctorService";
import DepartmentService from "../../api/services/DepartmentService";
import { useState, useEffect } from "react";
import { Department } from "../../models/Department";

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
  isOpened: boolean;
  onClose: () => void;
  onDoctorAdded?: (doctor: Doctor) => void;
  doctor?: Doctor;
  onDoctorUpdated?: () => void;
}

interface NewDoctorData {
  name: string;
  surname: string;
  address: string;
  phoneNumber: string;
  departmentId: number;
  startShift: string;
  endShift: string;
  weekDayIds: (number | undefined)[];
}

function DoctorFormDialog({
  isOpened: open,
  onClose,
  onDoctorAdded,
  doctor,
  onDoctorUpdated,
}: DoctorFormDialogProps) {
  const doctorService: DoctorService = new DoctorService();
  const departmentService: DepartmentService = new DepartmentService();

  const [departments, setDepartments] = useState<Department[]>([]);

  useEffect(() => {
    const fetchDepartments = async () => {
      try {
        const response = await departmentService.getAllDepartments();
        setDepartments(response);
      } catch (error) {
        console.error("Failed to fetch departments", error);
      }
    };

    fetchDepartments();
  }, []);

  const formik = useFormik({
    initialValues: {
      name: doctor?.name || "",
      surname: doctor?.surname || "",
      address: doctor?.address || "",
      phoneNumber: doctor?.phoneNumber || "",
      departmentId: 0,
      startShift: doctor?.workingHours?.startShift.slice(0, -3) || "",
      endShift: doctor?.workingHours?.endShift.slice(0, -3) || "",
      weekDays:
        doctor?.workingHours?.weekDays.map(
          (day) => daysOfWeek.find((d) => d.name === day)?.id
        ) || ([] as number[]),
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
      departmentId: Yup.number().required("Department is required"),
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
          departmentId: values.departmentId,
          startShift: values.startShift,
          endShift: values.endShift,
          weekDayIds: values.weekDays,
        };
        if (doctor) {
          console.log(doctorData);
          await doctorService.updateDoctor(doctorData, doctor.id);
          onDoctorUpdated && onDoctorUpdated();
        } else {
          const id = await doctorService.addDoctor(doctorData);
          0;
          const newDoctor = await doctorService.getDoctorById(id);
          onDoctorAdded && onDoctorAdded(newDoctor);
        }
        resetForm();
        onClose();
      } catch (error) {
        console.log(error);
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
      <DialogTitle>Doctor registration</DialogTitle>
      <DialogContent>
        <DialogContentText>
          Please fill out the form below to add a new doctor or update an
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
          <Select
            id="departmentId"
            name="departmentId"
            value={formik.values.departmentId}
            onChange={formik.handleChange}
            onBlur={formik.handleBlur}
            error={
              formik.touched.departmentId && Boolean(formik.errors.departmentId)
            }
            fullWidth
            sx={{ mb: 2 }}
          >
            {departments.map((department) => (
              <MenuItem key={department.id} value={department.id}>
                {department.name}
              </MenuItem>
            ))}
          </Select>
          {formik.touched.departmentId && formik.errors.departmentId && (
            <Typography color="error" variant="body2" sx={{ mb: 2 }}>
              {formik.errors.departmentId}
            </Typography>
          )}
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

export default DoctorFormDialog;

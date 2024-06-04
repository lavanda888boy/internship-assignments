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
} from "@mui/material";

interface AddRecordFormDialogProps {
  open: boolean;
  onClose: () => void;
}

function AddRecordFormDialog({ open, onClose }: AddRecordFormDialogProps) {
  const formik = useFormik({
    initialValues: {
      examinedPatient: "",
      responsibleDoctor: "",
      examinationNotes: "",
    },

    validationSchema: Yup.object({
      examinedPatient: Yup.string()
        .max(
          80,
          "Patient's name and surname must be no longer than 80 characters"
        )
        .required("Patient's information is required"),
      responsibleDoctor: Yup.string()
        .max(
          80,
          "Doctor's name and surname must be no longer than 80 characters"
        )
        .required("Doctor's info is required"),
      examinationNotes: Yup.string()
        .max(1800, "Examination notes must be precise")
        .required("Examination notes are required"),
    }),

    onSubmit: () => {
      onClose();
    },
  });

  return (
    <>
      <Dialog open={open} onClose={onClose}>
        <DialogTitle>Add Record</DialogTitle>
        <DialogContent>
          <DialogContentText>
            Please fill out the form below to add a new record.
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
            <InputLabel htmlFor="examinedPatient">Examined patient</InputLabel>
            <TextField
              name="examinedPatient"
              value={formik.values.examinedPatient}
              placeholder="Enter examined patient info"
              onChange={formik.handleChange}
              onBlur={formik.handleBlur}
              error={
                formik.touched.examinedPatient &&
                Boolean(formik.errors.examinedPatient)
              }
              helperText={
                formik.touched.examinedPatient && formik.errors.examinedPatient
              }
              fullWidth
              sx={{ mt: 0, mb: 1 }}
            />
            <InputLabel htmlFor="responsibleDoctor">
              Responsible doctor
            </InputLabel>
            <TextField
              name="responsibleDoctor"
              value={formik.values.responsibleDoctor}
              placeholder="Enter responsible doctor info"
              onChange={formik.handleChange}
              onBlur={formik.handleBlur}
              error={
                formik.touched.responsibleDoctor &&
                Boolean(formik.errors.responsibleDoctor)
              }
              helperText={
                formik.touched.responsibleDoctor &&
                formik.errors.responsibleDoctor
              }
              fullWidth
              sx={{ mt: 0, mb: 1 }}
            />
            <InputLabel htmlFor="examinationNotes">
              Examination notes
            </InputLabel>
            <TextField
              name="examinationNotes"
              value={formik.values.examinationNotes}
              placeholder="Enter some examination notes"
              onChange={formik.handleChange}
              onBlur={formik.handleBlur}
              error={
                formik.touched.examinationNotes &&
                Boolean(formik.errors.examinationNotes)
              }
              helperText={
                formik.touched.examinationNotes &&
                formik.errors.examinationNotes
              }
              fullWidth
              sx={{ mt: 0, mb: 1 }}
            />
            <Button
              type="submit"
              variant="contained"
              color="primary"
              sx={{ mt: 2, mx: 12 }}
            >
              Add Record
            </Button>
            <Button onClick={onClose} color="primary" sx={{ mt: 1 }}>
              Cancel
            </Button>
          </Box>
        </DialogContent>
      </Dialog>
    </>
  );
}

export default AddRecordFormDialog;

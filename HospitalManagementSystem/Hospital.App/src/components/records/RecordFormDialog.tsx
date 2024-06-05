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
} from "@mui/material";

interface RecordFormDialogProps {
  open: boolean;
  onClose: () => void;
}

function RecordFormDialog({ open, onClose }: RecordFormDialogProps) {
  const formik = useFormik({
    initialValues: {
      isDiagnosis: false,
      examinedPatient: "",
      responsibleDoctor: "",
      examinationNotes: "",
      diagnosedIllness: "",
      prescribedMedicine: "",
      treatmentDuration: 1,
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
      diagnosedIllness: Yup.string().test(
        "isDiagnosis",
        "Diagnosed illness is required",
        function (value) {
          const isDiagnosis = this.parent.isDiagnosis;
          if (isDiagnosis) {
            return Yup.string()
              .max(30, "Illness name should be no longer than 30 characters")
              .required("Diagnosed illness is required")
              .isValidSync(value);
          } else return Yup.string().isValidSync(value);
        }
      ),
      prescribedMedicine: Yup.string().test(
        "isDiagnosis",
        "Prescribed medicine is required",
        function (value) {
          const isDiagnosis = this.parent.isDiagnosis;
          if (isDiagnosis) {
            return Yup.string()
              .max(
                30,
                "Prescribed medicine name should be no longer than 30 characters"
              )
              .required("Prescribed medicine is required")
              .isValidSync(value);
          } else return Yup.string().isValidSync(value);
        }
      ),
      treatmentDuration: Yup.number().test(
        "isDiagnosis",
        "Treatment duration is required",
        function (value) {
          const isDiagnosis = this.parent.isDiagnosis;
          if (isDiagnosis) {
            return Yup.number()
              .max(30, "Treatment duration should be no longer than 30 days")
              .min(1, "Treatment duration should be no shorter than 1 day")
              .required("Treatment duration is required")
              .isValidSync(value);
          } else return Yup.string().isValidSync(value);
        }
      ),
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
            <FormControlLabel
              control={
                <Checkbox
                  checked={formik.values.isDiagnosis}
                  onChange={formik.handleChange("isDiagnosis")}
                />
              }
              label="Is this a diagnosis record?"
            />
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
              multiline
              rows={3}
              sx={{ mt: 0, mb: 1 }}
            />
            {formik.values.isDiagnosis && (
              <>
                <InputLabel htmlFor="diagnosedIllness">
                  Diagnosed illness
                </InputLabel>
                <TextField
                  name="diagnosedIllness"
                  value={formik.values.diagnosedIllness}
                  placeholder="Enter diagnosed illness"
                  onChange={formik.handleChange}
                  onBlur={formik.handleBlur}
                  error={
                    formik.touched.diagnosedIllness &&
                    Boolean(formik.errors.diagnosedIllness)
                  }
                  helperText={
                    formik.touched.diagnosedIllness &&
                    formik.errors.diagnosedIllness
                  }
                  fullWidth
                  sx={{ mt: 0, mb: 1 }}
                />
                <InputLabel htmlFor="prescribedMedicine">
                  Prescribed medicine
                </InputLabel>
                <TextField
                  name="prescribedMedicine"
                  value={formik.values.prescribedMedicine}
                  placeholder="Enter prescribed medicine"
                  onChange={formik.handleChange}
                  onBlur={formik.handleBlur}
                  error={
                    formik.touched.prescribedMedicine &&
                    Boolean(formik.errors.prescribedMedicine)
                  }
                  helperText={
                    formik.touched.prescribedMedicine &&
                    formik.errors.prescribedMedicine
                  }
                  fullWidth
                  sx={{ mt: 0, mb: 1 }}
                />
                <InputLabel htmlFor="treatmentDuration">
                  Treatment duration
                </InputLabel>
                <TextField
                  name="treatmentDuration"
                  type="number"
                  value={formik.values.treatmentDuration}
                  placeholder="Enter treatment duration"
                  onChange={formik.handleChange}
                  onBlur={formik.handleBlur}
                  error={
                    formik.touched.treatmentDuration &&
                    Boolean(formik.errors.treatmentDuration)
                  }
                  helperText={
                    formik.touched.treatmentDuration &&
                    formik.errors.treatmentDuration
                  }
                  fullWidth
                  sx={{ mt: 0, mb: 1 }}
                />
              </>
            )}
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

export default RecordFormDialog;

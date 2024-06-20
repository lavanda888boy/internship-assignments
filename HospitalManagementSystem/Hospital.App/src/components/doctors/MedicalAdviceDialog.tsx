import {
  Dialog,
  DialogContent,
  DialogActions,
  Button,
  Typography,
  Box,
} from "@mui/material";

interface MedicalAdviceDialogProps {
  open: boolean;
  onClose: () => void;
  advice: string | null;
}

function MedicalAdviceDialog({
  open,
  onClose,
  advice,
}: MedicalAdviceDialogProps) {
  return (
    <Dialog open={open} onClose={onClose}>
      <Box sx={{ display: "flex", transform: "scale(0.8)", mt: -3 }}>
        <img src="src\assets\medical_advice.jpg" alt="Medical advice" />
      </Box>
      <DialogContent sx={{ mt: -7 }}>
        <Typography sx={{ textAlign: "center" }}>{advice}</Typography>
      </DialogContent>
      <DialogActions
        sx={{ display: "flex", justifyContent: "center", mt: -1, mb: 1 }}
      >
        <Button onClick={onClose} color="primary">
          Thank You
        </Button>
      </DialogActions>
    </Dialog>
  );
}

export default MedicalAdviceDialog;

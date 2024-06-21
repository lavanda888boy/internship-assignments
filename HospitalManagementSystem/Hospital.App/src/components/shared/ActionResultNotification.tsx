import { Snackbar, Alert } from "@mui/material";
import { NotificationState } from "../../models/utils/NotificationState";

interface ActionResultNotificationProps {
  state: NotificationState;
  onClose: () => void;
}

function ActionResultNotification({
  state,
  onClose,
}: ActionResultNotificationProps) {
  return (
    <Snackbar
      open={state.open}
      autoHideDuration={4000}
      onClose={onClose}
      anchorOrigin={{
        vertical: "bottom",
        horizontal: "center",
      }}
    >
      <Alert
        onClose={onClose}
        severity={state.severity}
        sx={{ width: "100%", color: "black" }}
      >
        {state.message}
      </Alert>
    </Snackbar>
  );
}

export default ActionResultNotification;

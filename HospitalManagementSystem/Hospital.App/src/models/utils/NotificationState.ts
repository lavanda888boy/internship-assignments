export interface NotificationState {
  open: boolean;
  message: string;
  severity: "success" | "error";
}

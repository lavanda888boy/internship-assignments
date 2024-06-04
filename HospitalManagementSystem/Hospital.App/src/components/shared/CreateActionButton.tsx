import { Button } from "@mui/material";

interface CreateActionButtonProps {
  entityName: string;
  clickAction: () => void;
}

function CreateActionButton({
  entityName,
  clickAction,
}: CreateActionButtonProps) {
  return (
    <Button
      variant="contained"
      color="primary"
      sx={{ mr: 86, textAlign: "left" }}
      onClick={clickAction}
    >
      Add {entityName}
    </Button>
  );
}

export default CreateActionButton;

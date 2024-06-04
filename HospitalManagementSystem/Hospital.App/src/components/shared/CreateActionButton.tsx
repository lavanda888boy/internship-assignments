import { Button } from "@mui/material";

interface CreateActionButtonProps {
  entityName: string;
}

function CreateActionButton({ entityName }: CreateActionButtonProps) {
  return (
    <Button
      variant="contained"
      color="primary"
      sx={{ mr: 86, textAlign: "left" }}
    >
      Add {entityName}
    </Button>
  );
}

export default CreateActionButton;

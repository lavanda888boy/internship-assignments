import { Record } from "../../models/Record";
import { Card, CardHeader, CardContent, Typography, Box } from "@mui/material";
import ActionMenu from "../shared/ActionMenu";
import React from "react";

interface RecordCardProps {
  record: Record;
}

function RecordCard({ record: record }: RecordCardProps) {
  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);

  const handleMenuClick = (event: React.MouseEvent<HTMLButtonElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleMenuClose = () => {
    setAnchorEl(null);
  };

  return (
    <Card
      sx={{
        borderColor: "#B8B8FF",
        borderStyle: "solid",
        borderWidth: "5px",
        borderRadius: "5px",
      }}
    >
      <CardHeader
        title={record.DateOfExamination}
        subheader={`Examined patient: ${record.PatientFullName} | Responsible doctor: ${record.DoctorFullName}`}
        subheaderTypographyProps={{ style: { color: "white" } }}
        sx={{ backgroundColor: "#9381ff", color: "white" }}
      />
      <CardContent
        sx={{
          padding: "2%",
          "&:last-child": {
            paddingBottom: "0.5%",
          },
        }}
      >
        <Typography variant="body2" color="textSecondary" component="p">
          Examination notes: {record.ExaminationNotes}
        </Typography>
        {record.DiagnosedIllness && (
          <Typography variant="body2" color="textSecondary" component="p">
            Diagnosis: {record.DiagnosedIllness}
          </Typography>
        )}
        {record.ProposedTreatment && (
          <Typography variant="body2" color="textSecondary" component="p">
            Treatment: {record.ProposedTreatment}
          </Typography>
        )}
        <Box sx={{ textAlign: "right" }}>
          <ActionMenu
            rowId={record.id}
            anchorEl={anchorEl}
            handleMenuClick={handleMenuClick}
            handleMenuClose={handleMenuClose}
          />
        </Box>
      </CardContent>
    </Card>
  );
}

export default RecordCard;

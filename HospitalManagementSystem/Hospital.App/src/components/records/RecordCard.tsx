import { DiagnosisRecord } from "../../models/DiagnosisRecord";
import {
  Card,
  CardHeader,
  CardContent,
  Typography,
  Box,
  useTheme,
} from "@mui/material";
import ActionMenu from "../shared/ActionMenu";
import React from "react";

interface RecordCardProps {
  record: DiagnosisRecord;
}

function RecordCard({ record }: RecordCardProps) {
  const theme = useTheme();
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
        borderColor: theme.palette.primary.light,
        borderStyle: "solid",
        borderWidth: "5px",
        borderRadius: "5px",
      }}
    >
      <CardHeader
        title={new Date(record.dateOfExamination).toDateString()}
        subheader={`Examined patient: ${record.examinedPatient.fullName} | Responsible doctor: ${record.responsibleDoctor.name} ${record.responsibleDoctor.surname}`}
        subheaderTypographyProps={{
          style: { color: "white" },
        }}
        sx={{
          backgroundColor: theme.palette.primary.main,
          color: "white",
        }}
      />
      <CardContent
        sx={{
          "&:last-child": {
            paddingBottom: "0.5%",
          },
        }}
      >
        <Typography variant="h6" color="textSecondary" component="p">
          Examination notes: {record.examinationNotes}
        </Typography>
        <Box sx={{ display: "flex", gap: 2 }}>
          {record.diagnosedIllness && (
            <Typography variant="h6" color="textSecondary" component="p">
              Diagnosis: {record.diagnosedIllness.name};
            </Typography>
          )}
          {record.proposedTreatment && (
            <Typography variant="h6" color="textSecondary" component="p">
              Treatment: {record.proposedTreatment.prescribedMedicine},{" "}
              {record.proposedTreatment.durationInDays} days
            </Typography>
          )}
        </Box>
        <Box sx={{ textAlign: "right" }}>
          {/* <ActionMenu
            rowId={record.id}
            anchorEl={anchorEl}
            handleMenuClick={handleMenuClick}
            handleMenuClose={handleMenuClose}
          /> */}
        </Box>
      </CardContent>
    </Card>
  );
}

export default RecordCard;

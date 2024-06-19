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
import { useContext, useState } from "react";
import { UserRoleContext } from "../../context/UserRoleContext";

interface RecordCardProps {
  record: DiagnosisRecord;
  onRecordDelete: (record: any) => void;
}

function RecordCard({ record, onRecordDelete }: RecordCardProps) {
  const theme = useTheme();
  const userRoleContextProps = useContext(UserRoleContext);

  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const [selectedRecord, setSelectedRecord] = useState<any | null>(null);

  const handleMenuClick = (
    event: React.MouseEvent<HTMLButtonElement>,
    record: any
  ) => {
    setAnchorEl(event.currentTarget);
    setSelectedRecord(record);
  };

  const handleMenuClose = () => {
    setAnchorEl(null);
  };

  const handleDeleteRecord = () => {
    if (selectedRecord) {
      onRecordDelete(selectedRecord);
    }
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
        {userRoleContextProps?.userRole === "Admin" && (
          <Box sx={{ textAlign: "right" }}>
            <ActionMenu
              rowId={record.id}
              anchorEl={anchorEl}
              handleMenuClick={(event) => handleMenuClick(event, record)}
              handleMenuClose={handleMenuClose}
              onEntityDelete={handleDeleteRecord}
            />
          </Box>
        )}
      </CardContent>
    </Card>
  );
}

export default RecordCard;

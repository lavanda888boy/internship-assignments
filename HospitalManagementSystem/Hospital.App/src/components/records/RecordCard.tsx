import { Record } from "../../models/Record";
import { Card, CardHeader, CardContent, Typography } from "@mui/material";

interface RecordCardProps {
  record: Record;
}

function RecordCard({ record: record }: RecordCardProps) {
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
      <CardContent>
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
      </CardContent>
    </Card>
  );
}

export default RecordCard;

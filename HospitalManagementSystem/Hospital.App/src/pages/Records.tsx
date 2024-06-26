import RecordCard from "../components/records/RecordCard";
import { DiagnosisRecord } from "../models/DiagnosisRecord";
import usePageTitle from "../hooks/PageTitleHook";
import CreateActionButton from "../components/shared/CreateActionButton";
import { useContext, useEffect, useState } from "react";
import RecordFormDialog from "../components/records/RecordFormDialog";
import RegularRecordService from "../api/services/RegularRecordService";
import DiagnosisRecordService from "../api/services/DiagnosisRecordService";
import {
  Box,
  Container,
  Typography,
  Select,
  MenuItem,
  Pagination,
  SelectChangeEvent,
} from "@mui/material";
import { RegularRecord } from "../models/RegularRecord";
import { UserRoleContext } from "../context/UserRoleContext";
import PatientService from "../api/services/PatientService";
import { NotificationState } from "../models/utils/NotificationState";
import ActionResultNotification from "../components/shared/ActionResultNotification";

function Records() {
  usePageTitle("Medical Records");

  const regularRecordService: RegularRecordService = new RegularRecordService();
  const diagnosisRecordsService: DiagnosisRecordService =
    new DiagnosisRecordService();
  const patientService: PatientService = new PatientService();

  const [regularRecords, setRegularRecords] = useState<RegularRecord[]>([]);
  const [diagnosisRecords, setDiagnosisRecords] = useState<DiagnosisRecord[]>(
    []
  );

  const [createFormOpen, setCreateFormOpen] = useState(false);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalRegularRecords, setTotalRegularRecords] = useState(0);
  const [totalDiagnosisRecords, setTotalDiagnosisRecords] = useState(0);
  const [pageSize, setPageSize] = useState(10);

  const [selectedRecordType, setSelectedRecordType] = useState("");

  const userRoleContextProps = useContext(UserRoleContext);

  const [notification, setNotification] = useState<NotificationState>({
    open: false,
    message: "",
    severity: "error",
  });

  useEffect(() => {
    if (selectedRecordType === "Regular" || selectedRecordType === "") {
      const fetchRegularRecords = async () => {
        try {
          let response;

          if (userRoleContextProps?.userRole === "PatientUser") {
            const patientCredentials =
              userRoleContextProps?.userCredentials.split(" ");
            const patientId =
              await patientService.searchPatientIdByNameAndSurname(
                patientCredentials[0],
                patientCredentials[1]
              );

            response =
              await regularRecordService.searchRegularRecordsByPatientId(
                patientId,
                currentPage,
                Math.floor(pageSize / 2)
              );
          } else {
            response = await regularRecordService.getAllRegularRecords(
              currentPage,
              Math.floor(pageSize / 2)
            );
          }

          setRegularRecords(response.items);
          setTotalRegularRecords(response.totalItems);
        } catch (error) {
          console.log(error);
        }
      };

      fetchRegularRecords();
    } else setRegularRecords([]);
  }, [currentPage, pageSize, selectedRecordType]);

  useEffect(() => {
    if (selectedRecordType === "Diagnosis" || selectedRecordType === "") {
      const fetchDiagnosisRecords = async () => {
        try {
          let response;

          if (userRoleContextProps?.userRole === "PatientUser") {
            const patientCredentials =
              userRoleContextProps?.userCredentials.split(" ");
            const patientId =
              await patientService.searchPatientIdByNameAndSurname(
                patientCredentials[0],
                patientCredentials[1]
              );

            response =
              await diagnosisRecordsService.searchDiagnosisRecordsByPatientId(
                patientId,
                currentPage,
                Math.floor(pageSize / 2)
              );
          } else {
            response = await diagnosisRecordsService.getAllDiagnosisRecords(
              currentPage,
              Math.floor(pageSize / 2)
            );
          }

          setDiagnosisRecords(response.items);
          setTotalDiagnosisRecords(response.totalItems);
        } catch (error) {
          console.log(error);
        }
      };

      fetchDiagnosisRecords();
    } else setDiagnosisRecords([]);
  }, [currentPage, pageSize, selectedRecordType]);

  const handleCreateFormOpen = () => {
    setCreateFormOpen(true);
  };

  const handleCreateFormClose = () => {
    setCreateFormOpen(false);
  };

  const handleAddRecord = (newRecord: any) => {
    if (newRecord.diagnosedIllness) {
      setDiagnosisRecords((prevRecords) => [newRecord, ...prevRecords]);
    } else setRegularRecords((prevRecords) => [newRecord, ...prevRecords]);
  };

  const handleDeleteRecord = async (selectedRecord: any) => {
    try {
      if (selectedRecord) {
        console.log(selectedRecord);
        if (selectedRecord.diagnosedIllness) {
          await diagnosisRecordsService.deleteDiagnosisRecord(
            selectedRecord.id
          );
          setDiagnosisRecords((prevRecords) =>
            prevRecords.filter((r) => r.id !== selectedRecord.id)
          );
        } else {
          await regularRecordService.deleteRegularRecord(selectedRecord.id);
          setRegularRecords((prevRecords) =>
            prevRecords.filter((r) => r.id !== selectedRecord.id)
          );
        }
      }
    } catch (error) {
      setNotification({
        open: true,
        message: "Failed to remove record.",
        severity: "error",
      });

      console.log(error);
    }
  };

  const handlePageChange = (
    _event: React.ChangeEvent<unknown>,
    newPage: number
  ) => {
    setCurrentPage(newPage);
  };

  const handlePageSizeChange = (event: SelectChangeEvent<number>) => {
    setPageSize(parseInt(event.target.value as string));
    setCurrentPage(1);
  };

  const handleSelectedRecordTypeChange = (event: SelectChangeEvent<string>) => {
    setSelectedRecordType(event.target.value);
  };

  const handleCloseNotification = () => {
    setNotification((prev: NotificationState) => ({ ...prev, open: false }));
  };

  return (
    <Container
      sx={{
        position: "absolute",
        width: "78.15%",
        height: "auto",
        zIndex: 1,
        padding: "1.5% 2% 3% 2%",
        marginTop: "8%",
        marginLeft: "8%",
        borderRadius: "5px",
        backgroundColor: "white",
      }}
    >
      <ActionResultNotification
        state={notification}
        onClose={handleCloseNotification}
      />
      <Box
        sx={{
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
          mb: 2,
        }}
      >
        {userRoleContextProps?.userRole !== "PatientUser" && (
          <CreateActionButton
            entityName="Record"
            clickAction={handleCreateFormOpen}
          />
        )}
        <Box
          sx={{
            display: "flex",
            alignItems: "center",
          }}
        >
          <Typography sx={{ mr: 1 }}>Record type:</Typography>
          <Select
            value={selectedRecordType}
            onChange={handleSelectedRecordTypeChange}
            displayEmpty
            sx={{
              overflow: "hidden",
              textOverflow: "ellipsis",
              whiteSpace: "nowrap",
            }}
          >
            <MenuItem value="">
              <Typography>All</Typography>
            </MenuItem>
            <MenuItem value="Regular">Regular records</MenuItem>
            <MenuItem value="Diagnosis">Diagnosis records</MenuItem>
          </Select>
        </Box>
      </Box>
      <Box sx={{ display: "flex", flexDirection: "column", gap: "30px" }}>
        {regularRecords.map((record, index) => (
          <RecordCard
            key={index}
            record={record}
            onRecordDelete={handleDeleteRecord}
          />
        ))}
        {diagnosisRecords.map((record, index) => (
          <RecordCard
            key={index}
            record={record}
            onRecordDelete={handleDeleteRecord}
          />
        ))}
      </Box>
      <Box
        sx={{
          display: "flex",
          alignItems: "center",
          mt: 4,
          ml: 37,
        }}
      >
        <Typography sx={{ mr: 1 }}>Records per page:</Typography>
        <Select
          value={pageSize}
          onChange={handlePageSizeChange}
          sx={{ mr: 0.65 }}
        >
          <MenuItem value={4}>4</MenuItem>
          <MenuItem value={10}>10</MenuItem>
          <MenuItem value={14}>14</MenuItem>
        </Select>
        <Pagination
          count={Math.ceil(
            (totalRegularRecords + totalDiagnosisRecords) / pageSize
          )}
          page={currentPage}
          onChange={handlePageChange}
          color="primary"
        />
      </Box>
      {userRoleContextProps?.userRole !== "PatientUser" && (
        <RecordFormDialog
          isOpened={createFormOpen}
          onClose={handleCreateFormClose}
          onRecordAdded={handleAddRecord}
        />
      )}
    </Container>
  );
}

export default Records;

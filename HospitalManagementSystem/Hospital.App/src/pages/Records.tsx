import RecordCard from "../components/records/RecordCard";
import { DiagnosisRecord } from "../models/DiagnosisRecord";
import usePageTitle from "../hooks/PageTitleHook";
import CreateActionButton from "../components/shared/CreateActionButton";
import { useEffect, useState } from "react";
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

function Records() {
  usePageTitle("Medical Records");

  const regularRecordService: RegularRecordService = new RegularRecordService();
  const diagnosisRecordsService: DiagnosisRecordService =
    new DiagnosisRecordService();

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

  useEffect(() => {
    if (selectedRecordType === "Regular" || selectedRecordType === "") {
      const fetchRegularRecords = async () => {
        try {
          const response = await regularRecordService.getAllRegularRecords(
            currentPage,
            Math.floor(pageSize / 2)
          );
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
          const response = await diagnosisRecordsService.getAllDiagnosisRecords(
            currentPage,
            Math.floor(pageSize / 2)
          );
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
      <Box
        sx={{
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
          mb: 2,
        }}
      >
        <CreateActionButton
          entityName="Record"
          clickAction={handleCreateFormOpen}
        />
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
          <RecordCard key={index} record={record} />
        ))}
        {diagnosisRecords.map((record, index) => (
          <RecordCard key={index} record={record} />
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
      <RecordFormDialog
        isOpened={createFormOpen}
        onClose={handleCreateFormClose}
      />
    </Container>
  );
}

export default Records;

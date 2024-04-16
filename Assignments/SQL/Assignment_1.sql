--CREATE DATABASE HospitalManagement;
USE HospitalManagement;

CREATE TABLE Genders (
	Id INT PRIMARY KEY,
	Type NVARCHAR(5) NOT NULL 
);

CREATE TABLE Departments (
	Id INT PRIMARY KEY,
	Name NVARCHAR(20) NOT NULL UNIQUE
);

CREATE TABLE DoctorWorkingHours (
	Id INT PRIMARY KEY,
	StartShift DATETIMEOFFSET NOT NULL, 
	EndShift DATETIMEOFFSET NOT NULL 
);

CREATE TABLE DoctorWorkingHoursWeekDays (
	Id INT PRIMARY KEY,
	DoctorWorkingHoursId INT NOT NULL,
	WeekDay TINYINT NOT NULL CHECK (WeekDay > 0 AND WeekDay < 8),

	FOREIGN KEY (DoctorWorkingHoursId) REFERENCES DoctorWorkingHours(Id)
);

CREATE TABLE Patients (
    Id INT PRIMARY KEY,
    Name NVARCHAR(20) NOT NULL,
    Surname NVARCHAR(20) NOT NULL,
    Age INT NOT NULL CHECK (Age > 0 AND Age < 110),
    GenderId INT NOT NULL,
    Address NVARCHAR(100) NOT NULL,
    PhoneNumber NVARCHAR(20),
    InsuranceNumber NVARCHAR(20),

	FOREIGN KEY (GenderId) REFERENCES Genders(Id)
);

CREATE TABLE Doctors (
    Id INT PRIMARY KEY,
    Name NVARCHAR(20) NOT NULL,
    Surname NVARCHAR(20) NOT NULL,
    Address NVARCHAR(100) NOT NULL,
    PhoneNumber NVARCHAR(20) NOT NULL,
    DepartmentId INT NOT NULL,
    WorkingHoursId INT NOT NULL

	FOREIGN KEY (DepartmentId) REFERENCES Departments(Id),
	FOREIGN KEY (WorkingHoursId) REFERENCES DoctorWorkingHours(Id)
);

CREATE TABLE DoctorPatient (
    DoctorId INT,
    PatientId INT,
    PRIMARY KEY (DoctorId, PatientId),
    FOREIGN KEY (DoctorId) REFERENCES Doctors(Id),
    FOREIGN KEY (PatientId) REFERENCES Patients(Id)
);

CREATE TABLE IllnessSeverities (
	Id INT PRIMARY KEY,
	Severity NVARCHAR(10) NOT NULL
);

CREATE TABLE Illnesses (
	Id INT PRIMARY KEY,
	Name NVARCHAR(20) NOT NULL UNIQUE,
	SeverityId INT NOT NULL,

	FOREIGN KEY (SeverityId) REFERENCES IllnessSeverities(Id)
);

CREATE TABLE RegularMedicalRecords (
	Id INT PRIMARY KEY,
	ExaminedPatientId INT NOT NULL,
	ResponsibleDoctorId INT NOT NULL,
	DateOfExamination DATETIMEOFFSET NOT NULL,
	ExaminationNotes NVARCHAR NOT NULL,

	FOREIGN KEY (ExaminedPatientId) REFERENCES Patients(Id),
	FOREIGN KEY (ResponsibleDoctorId) REFERENCES Doctors(Id)
);

CREATE TABLE Treatments (
	Id INT PRIMARY KEY,
	PrescribedMedicine NVARCHAR(15) NOT NULL,
	TreatmentDuration INT NOT NULL CHECK(TreatmentDuration > 0 AND TreatmentDuration < 90)
);

CREATE TABLE DiagnosisMedicalRecords (
	Id INT PRIMARY KEY,
	ExaminedPatientId INT NOT NULL,
	ResponsibleDoctorId INT NOT NULL,
	DateOfExamination DATETIMEOFFSET NOT NULL,
	ExaminationNotes NVARCHAR NOT NULL,
	DiagnosedIllnessId INT NOT NULL,
	ProposedTreatmentId INT NOT NULL,

	FOREIGN KEY (ExaminedPatientId) REFERENCES Patients(Id),
	FOREIGN KEY (ResponsibleDoctorId) REFERENCES Doctors(Id),
	FOREIGN KEY (DiagnosedIllnessId) REFERENCES Illnesses(Id),
	FOREIGN KEY (ProposedTreatmentId) REFERENCES Treatments(Id)
);
CREATE TABLE Station
(
    StatNr INTEGER PRIMARY KEY,
    Name   CHARACTER VARYING(30)
);

CREATE TABLE Room
(
    RoomNr  INTEGER PRIMARY KEY,
    BedsNum INTEGER,
    StatNr  INTEGER REFERENCES Station (StatNr)
);

CREATE TABLE StationPersonnel
(
    PersNr  INTEGER PRIMARY KEY,
    NameNum INTEGER,
    StatNr  INTEGER REFERENCES Station (StatNr)
);

CREATE TABLE Caregiver
(
    Qualification CHARACTER VARYING(30),
    PersNr        INTEGER REFERENCES StationPersonnel (PersNr),
    CaregiverNr   INTEGER PRIMARY KEY
);

CREATE TABLE Doctor
(
    Area     CHARACTER VARYING(30),
    Rank     CHARACTER VARYING(30),
    PersNr   INTEGER REFERENCES StationPersonnel (PersNr),
    DoctorNr INTEGER PRIMARY KEY
);

CREATE TABLE Patient
(
    PatientNr     INTEGER PRIMARY KEY,
    Name          CHARACTER VARYING(30),
    Disease       CHARACTER VARYING(30),
    RoomNr        INTEGER REFERENCES Room (RoomNr),
    AdmissionFrom TIMESTAMP,
    AdmissionTo   TIMESTAMP,
    DoctorNr      INTEGER REFERENCES Doctor (DoctorNr)
);
CREATE TABLE City
(
    Name   CHARACTER VARYING(30),
    Region CHARACTER VARYING(30),
    PRIMARY KEY (Name, Region)
);

CREATE TABLE Station
(
    Name      CHARACTER VARYING(30) PRIMARY KEY,
    TracksNum INTEGER,
    CityName  CHARACTER VARYING(30),
    Region    CHARACTER VARYING(30),
    FOREIGN KEY (CityName, Region) REFERENCES City (Name, Region)
);

CREATE TABLE Train
(
    TrainNr          INTEGER PRIMARY KEY,
    Length           INTEGER,
    StartStationName CHARACTER VARYING(30) REFERENCES Station (Name),
    EndStationName   CHARACTER VARYING(30) REFERENCES Station (Name)
);

CREATE TABLE Connection
(
    FromStation CHARACTER VARYING(30) REFERENCES Station (Name),
    ToStation   CHARACTER VARYING(30) REFERENCES Station (Name),
    TrainNr     INTEGER REFERENCES Train (TrainNr),
    Arrival     TIMESTAMP,
    Departure   TIMESTAMP,
    PRIMARY KEY (FromStation, ToStation, TrainNr, Arrival, Departure)
)
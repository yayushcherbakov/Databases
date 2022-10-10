CREATE TABLE Reader
(
    ID        INTEGER PRIMARY KEY,
    LastName  CHARACTER VARYING(30),
    FirstName CHARACTER VARYING(30),
    Address   CHARACTER VARYING(30),
    BirthDate TIMESTAMP
);

CREATE TABLE Publisher
(
    PubName    CHARACTER VARYING(30) PRIMARY KEY,
    PubAddress CHARACTER VARYING(30)
);

CREATE TABLE Book
(
    ISBN     INTEGER PRIMARY KEY,
    Title    CHARACTER VARYING(30),
    Author   CHARACTER VARYING(30),
    PagesNum INTEGER,
    PubYear  INTEGER,
    PubName  CHARACTER VARYING(30) REFERENCES Publisher (PubName)
);

CREATE TABLE Copy
(
    ISBN          INTEGER REFERENCES Book (ISBN),
    CopyNumber    INTEGER,
    ShelfPosition CHARACTER VARYING(30),
    PRIMARY KEY (CopyNumber, ISBN)
);

CREATE TABLE Category
(
    CategoryName CHARACTER VARYING(30) PRIMARY KEY,
    ParentCat    CHARACTER VARYING(30) REFERENCES Category (CategoryName) NULL
);

CREATE TABLE BookCat
(
    ISBN         INTEGER REFERENCES Book (ISBN),
    CategoryName CHARACTER VARYING(30) REFERENCES Category (CategoryName),
    PRIMARY KEY (ISBN, CategoryName)
);

CREATE TABLE Borrowing
(
    ReaderNr   INTEGER REFERENCES Reader (ID),
    ISBN       INTEGER,
    CopyNumber INTEGER,
    ReturnDate TIMESTAMP null,
    PRIMARY KEY (ReaderNr, ISBN, CopyNumber, ReturnDate),
    FOREIGN KEY (ISBN, CopyNumber) REFERENCES Copy (ISBN, CopyNumber)
);
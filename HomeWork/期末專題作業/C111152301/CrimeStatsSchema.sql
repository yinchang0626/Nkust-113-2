-- SQL Script to Create CrimeStats and UserAccounts Tables for ASP.NET Core MVC Project

CREATE TABLE CrimeStats (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Year INTEGER NOT NULL,
    Month INTEGER NOT NULL,
    TotalCases INTEGER NOT NULL,
    RapeCases INTEGER NOT NULL,
    RobberyCases INTEGER NOT NULL,
    OtherCases INTEGER NOT NULL,
    CrimeRate REAL,
    Suspects INTEGER,
    SuspectRate REAL,
    SolvedCases INTEGER,
    SolvedCurrent INTEGER,
    SolvedOld INTEGER,
    SolvedOther INTEGER,
    SolveRate REAL
);

CREATE TABLE UserAccounts (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Username TEXT NOT NULL,
    Password TEXT NOT NULL,
    Role TEXT NOT NULL
);
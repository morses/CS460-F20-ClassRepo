-- UP script for SQLite

CREATE TABLE [Peak] (
  [ID]              INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
  [Name]            NVARCHAR(30) NOT NULL,
  [Height]          INT NOT NULL,
  [ClimbingStatus]  BOOLEAN NOT NULL,
  [FirstAscentYear] INT
);

CREATE TABLE [Expedition] (
  [ID]                INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
  [Season]            NVARCHAR(10),
  [Year]              INT,
  [StartDate]         DATE,
  [TerminationReason] NVARCHAR(80),
  [OxygenUsed]        BOOLEAN,
  [PeakID]            INT,
  [TrekkingAgencyID]  INT,
  FOREIGN KEY ([PeakID]) REFERENCES [Peak] ([ID]),
  FOREIGN KEY ([TrekkingAgencyID]) REFERENCES [TrekkingAgency] ([ID])
);

CREATE TABLE [TrekkingAgency] (
  [ID]    INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
  [Name]  NVARCHAR(100)
);

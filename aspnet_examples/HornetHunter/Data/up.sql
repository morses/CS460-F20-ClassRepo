-- script to create all tables and setup our complete data model

CREATE TABLE Sightings (
	ID				INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
	FullName		NVARCHAR(64),
	Phone			NVARCHAR(15)	NOT NULL,
	Latitude		FLOAT(7)		NOT NULL,
	Longitude		FLOAT(7)		NOT NULL,
	SightingTime	DATETIME		NOT NULL,
	Description		NVARCHAR(512),
	ReportTime		DATETIME		NOT NULL,
	IPAddress		NVARCHAR(25)
);
GO
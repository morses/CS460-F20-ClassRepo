
-- sqlite version (I haven't run the scaffolding on this so pay attention to types generated in the model class)
-- also remember to modify the line in the dbcontext file to:
-- entity.Property(e => e.Id).ValueGeneratedOnAdd();

CREATE TABLE Sightings 
(
	ID				INTEGER			NOT NULL PRIMARY KEY AUTOINCREMENT,
	FullName		NVARCHAR(64)	NULL,
	Phone			NVARCHAR(15)	NOT NULL,
	Latitude		FLOAT(7)		NOT NULL,
	Longitude		FLOAT(7)		NOT NULL,
	SightingTime	DATETIME		NOT NULL,
	Description		NVARCHAR(512)	NULL,
	ReportTime		DATETIME		NOT NULL,
	IPAddress		NVARCHAR(25)
);
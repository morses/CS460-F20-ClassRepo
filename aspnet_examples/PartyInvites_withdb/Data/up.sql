CREATE TABLE GuestResponses
(
	ID		        INTEGER             NOT NULL PRIMARY KEY AUTOINCREMENT,
	FullName		NVARCHAR(50)		NOT NULL,
    Email           NVARCHAR(50)        NOT NULL,
    Phone           NVARCHAR(50)        NOT NULL,
    WillAttend      BOOLEAN,
	TimeSubmitted	DATETIME			NOT NULL
);
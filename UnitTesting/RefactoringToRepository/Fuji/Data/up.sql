CREATE TABLE [FujiUser] (
  [ID] int PRIMARY KEY IDENTITY(1, 1),
  [ASPNetIdentityId] nvarchar(450),
  [FirstName] nvarchar(50),
  [LastName] nvarchar(50)
);

CREATE TABLE [Apple] (
  [ID] int PRIMARY KEY IDENTITY(1, 1),
  [VarietyName] nvarchar(50),
  [ScientificName] nvarchar(100)
);

CREATE TABLE [ApplesConsumed] (
  [ID] int PRIMARY KEY IDENTITY(1, 1),
  [FujiUserID] int NOT NULL,
  [AppleID] int NOT NULL,
  [Count] int NOT NULL,
  [ConsumedAt] datetime NOT NULL
);

ALTER TABLE [ApplesConsumed] ADD CONSTRAINT [ApplesConsumed_fk_FujiUser] FOREIGN KEY ([FujiUserID]) REFERENCES [FujiUser] ([ID]);

ALTER TABLE [ApplesConsumed] ADD CONSTRAINT [ApplesConsumed_fk_Apple] FOREIGN KEY ([AppleID]) REFERENCES [Apple] ([ID]);

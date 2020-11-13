CREATE TABLE [Buyer] (
  [ID] int PRIMARY KEY IDENTITY(1, 1),
  [FirstName] nvarchar(50) NOT NULL,
  [LastName] nvarchar(50) NOT NULL,
  [Email] nvarchar(50) NOT NULL
)
GO

CREATE TABLE [Seller] (
  [ID] int PRIMARY KEY IDENTITY(1, 1),
  [FirstName] nvarchar(50) NOT NULL,
  [LastName] nvarchar(50) NOT NULL,
  [Email] nvarchar(50) NOT NULL,
  [Phone] nvarchar(15)
)
GO

CREATE TABLE [Item] (
  [ID] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(50) NOT NULL,
  [Description] nvarchar(256) NOT NULL,
  [SellerID] int
)
GO

CREATE TABLE [Bid] (
  [ID] int PRIMARY KEY IDENTITY(1, 1),
  [Price] decimal(17,2),
  [TimeSubmitted] datetime,
  [BuyerID] int,
  [ItemID] int
)
GO

ALTER TABLE [Item] ADD CONSTRAINT [Item_Fk_Seller] FOREIGN KEY ([SellerID]) REFERENCES [Seller] ([ID])
GO

ALTER TABLE [Bid] ADD CONSTRAINT [Bid_Fk_Buyer] FOREIGN KEY ([BuyerID]) REFERENCES [Buyer] ([ID])
GO

ALTER TABLE [Bid] ADD CONSTRAINT [Bid_Fk_Item] FOREIGN KEY ([ItemID]) REFERENCES [Item] ([ID])
GO

ALTER TABLE [ApplesConsumed] DROP CONSTRAINT [ApplesConsumed_fk_FujiUser]
ALTER TABLE [ApplesConsumed] DROP CONSTRAINT [ApplesConsumed_fk_Apple]

DROP TABLE ApplesConsumed
DROP TABLE Apple
DROP TABLE FujiUser
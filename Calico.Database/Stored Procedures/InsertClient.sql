﻿CREATE PROCEDURE [dbo].[InsertClient]
	@Name NVARCHAR(MAX) 
AS
INSERT INTO [dbo].[Clients] ([Name])
VALUES (@Name)
SELECT CAST(SCOPE_IDENTITY() AS INT)

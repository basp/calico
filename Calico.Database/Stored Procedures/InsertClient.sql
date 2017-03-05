CREATE PROCEDURE [dbo].[InsertClient]
	@Name nvarchar(MAX) 
AS
INSERT INTO [dbo].[Clients] ([Name])
VALUES (@Name)
SELECT CAST(SCOPE_IDENTITY() AS int)

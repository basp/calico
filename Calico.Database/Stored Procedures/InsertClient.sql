CREATE PROCEDURE [dbo].[InsertTenant]
	@Name nvarchar(MAX) 
AS
INSERT INTO [dbo].[Tenants] ([Name])
VALUES (@Name)
SELECT CAST(SCOPE_IDENTITY() AS int)

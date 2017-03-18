CREATE PROCEDURE [dbo].[GetTenant]
	@Id int
AS
SELECT TOP 1 
	[Id], 
	[Name]
FROM [dbo].[Tenants]
WHERE [Id] = @Id
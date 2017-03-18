CREATE PROCEDURE [dbo].[GetTenants]
	@Top int
AS
SELECT TOP(@Top) 
	[Id], 
	[Name] 
FROM [dbo].[Tenants]
ORDER BY [Id] DESC
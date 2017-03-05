CREATE PROCEDURE [dbo].[GetClients]
	@Top int
AS
SELECT TOP(@Top) [Id], [Name] 
FROM [dbo].[Clients]
ORDER BY [Id] DESC
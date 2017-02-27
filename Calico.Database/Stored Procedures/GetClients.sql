CREATE PROCEDURE [dbo].[GetClients]
	@Top INT = 50
AS
SELECT TOP(@Top) [Id], [Name] 
FROM [dbo].[Clients]
ORDER BY [Id] DESC
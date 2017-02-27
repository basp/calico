CREATE PROCEDURE [dbo].[GetClients]
	@Top INT
AS
SELECT TOP(@Top) [Id], [Name] 
FROM [dbo].[Clients]
ORDER BY [Id] DESC
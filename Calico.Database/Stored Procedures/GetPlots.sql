CREATE PROCEDURE [dbo].[GetPlots]
	@ClientId INT,
	@Top INT
AS
SELECT TOP(@Top) [Id], [ClientId], [Name], [Geometry]
FROM [dbo].[Plots]
ORDER BY [Id] DESC
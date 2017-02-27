CREATE PROCEDURE [dbo].[GetPlots]
	@ClientId INT,
	@Top INT = 50
AS
SELECT TOP(@Top) [Id], [Name], [Geometry]
FROM [dbo].[Plots]
ORDER BY [Id] DESC
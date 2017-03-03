CREATE PROCEDURE [dbo].[GetPlots]
	@ClientId INT,
	@Top INT
AS
SELECT TOP(@Top) 
	[Id], 
	[ClientId], 
	[Name], 
	[Geometry].ToString() AS Wkt
FROM [dbo].[Plots]
ORDER BY [Id]
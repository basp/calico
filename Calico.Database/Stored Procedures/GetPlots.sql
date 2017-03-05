CREATE PROCEDURE [dbo].[GetPlots]
	@ClientId int,
	@Top int
AS
SELECT TOP(@Top) 
	[Id], 
	[ClientId], 
	[Name], 
	[Geometry].ToString() AS Wkt,
	[SRID]
FROM [dbo].[Plots]
ORDER BY [Id]
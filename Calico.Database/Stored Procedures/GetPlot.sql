CREATE PROCEDURE [dbo].[GetPlot]
	@PlotId int
AS
SELECT 
	[Id], 
	[ClientId], 
	[Name], 
	[Geometry].ToString() AS [Wkt],
	[SRID]
FROM [dbo].[Plots]
WHERE [Id] = @PlotId
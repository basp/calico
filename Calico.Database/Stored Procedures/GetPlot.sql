CREATE PROCEDURE [dbo].[GetPlot]
	@PlotId INT
AS
SELECT 
	[Id], 
	[ClientId], 
	[Name], 
	[Geometry].ToString() AS [Wkt],
	[SRID]
FROM [dbo].[Plots]
WHERE [Id] = @PlotId
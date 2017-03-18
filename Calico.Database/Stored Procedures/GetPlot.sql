CREATE PROCEDURE [dbo].[GetPlot]
	@PlotId int
AS
SELECT 
	[Id], 
	[TenantId], 
	[Name], 
	[Geometry].ToString() AS [Wkt],
	[SRID]
FROM [dbo].[Plots]
WHERE [Id] = @PlotId
CREATE PROCEDURE [dbo].[GetPlot]
	@PlotId INT
AS
SELECT 
	[Id], 
	[ClientId], 
	[Name], 
	[Geometry].ToString() AS [Wkt]
FROM [dbo].[Plots]
WHERE [Id] = @PlotId
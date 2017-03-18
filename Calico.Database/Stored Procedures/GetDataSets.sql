CREATE PROCEDURE [dbo].[GetDataSets]
	@PlotId int,
	@Top int
AS
SELECT TOP(@Top) 
	[Id], 
	[PlotId], 
	[FeatureTypeId], 
	[Name], 
	[DateCreated] 
FROM [dbo].[DataSets]
WHERE [PlotId] = @PlotId
ORDER BY [DateCreated] DESC
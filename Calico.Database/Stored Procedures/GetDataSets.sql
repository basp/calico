CREATE PROCEDURE [dbo].[GetDataSets]
	@PlotId INT,
	@Top INT
AS
SELECT TOP(@Top) [Id], [PlotId], [FeatureTypeId], [Name], [DateCreated] 
FROM [dbo].[DataSets]
WHERE [PlotId] = @PlotId
ORDER BY [Id] DESC
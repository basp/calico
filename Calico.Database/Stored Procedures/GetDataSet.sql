CREATE PROCEDURE [dbo].[GetDataSet]
	@Id int
AS
SELECT [Id], [PlotId], [FeatureTypeId], [Name], [DateCreated]
FROM [dbo].[DataSets]
WHERE [Id] = @Id
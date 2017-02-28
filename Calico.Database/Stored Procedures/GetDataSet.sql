CREATE PROCEDURE [dbo].[GetDataSet]
	@Id INT
AS
SELECT [Id], [PlotId], [FeatureTypeId], [Name], [DateCreated]
FROM [dbo].[DataSets]
WHERE [Id] = @Id
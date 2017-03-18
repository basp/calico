CREATE PROCEDURE [dbo].[GetDataSet]
	@Id int,
	@Top int = 50
AS
SELECT TOP(@Top)
	[Id], 
	[PlotId], 
	[FeatureTypeId], 
	[Name], 
	[DateCreated]
FROM [dbo].[DataSets]
WHERE [Id] = @Id
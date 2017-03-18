CREATE PROCEDURE [dbo].[InsertDataSet]
	@PlotId int,
	@FeatureTypeId int,
	@Name nvarchar(MAX),
	@DateCreated datetime2(0)
AS
INSERT INTO [dbo].[DataSets] (
	[PlotId], 
	[FeatureTypeId], 
	[Name], 
	[DateCreated])
VALUES (
	@PlotId, 
	@FeatureTypeId, 
	@Name, 
	@DateCreated)
SELECT CAST(SCOPE_IDENTITY() AS int)
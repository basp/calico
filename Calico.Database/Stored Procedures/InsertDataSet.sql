CREATE PROCEDURE [dbo].[InsertDataSet]
	@PlotId INT,
	@FeatureTypeId INT,
	@Name NVARCHAR(MAX),
	@DateCreated DATETIME2
AS
INSERT INTO [dbo].[DataSets] ([PlotId], [FeatureTypeId], [Name], [DateCreated])
VALUES (@PlotId, @FeatureTypeId, @Name, @DateCreated)
SELECT CAST(SCOPE_IDENTITY() AS INT)
CREATE PROCEDURE [dbo].[InsertFeature]
	@DataSetId INT,
	@Index INT,
	@Geometry GEOMETRY
AS
INSERT INTO [dbo].[Features] ([DataSetId], [Index], [Geometry])
VALUES (@DataSetId, @Index, @Geometry)
SELECT CAST(SCOPE_IDENTITY() AS INT)
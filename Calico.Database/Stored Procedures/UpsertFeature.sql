CREATE PROCEDURE [dbo].[UpsertFeature]
	@DataSetId INT,
	@Index INT,
	@Geometry GEOMETRY
AS
INSERT INTO [dbo].[Features] ([DataSetId], [Index], [Geometry])
VALUES (@DataSetId, @Index, @Geometry)
SELECT @@ROWCOUNT
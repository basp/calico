CREATE PROCEDURE [dbo].[GetFeatures]
	@DataSetId INT
AS
SELECT 
	[DataSetId],
	[Index], 
	[Geometry].ToString() AS [Wkt],
	[SRID]
FROM [dbo].[Features]
WHERE [DataSetId] = @DataSetId
ORDER BY [Index]
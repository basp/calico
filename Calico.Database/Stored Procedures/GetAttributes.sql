CREATE PROCEDURE [dbo].[GetAttributes]
	@FeatureTypeId int
AS
SELECT 
	[FeatureTypeId], 
	[Index], 
	[DataTypeId], 
	[Name]
FROM [dbo].[Attributes]
WHERE FeatureTypeId = @FeatureTypeId
ORDER BY [Index]
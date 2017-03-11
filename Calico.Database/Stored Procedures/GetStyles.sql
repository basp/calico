CREATE PROCEDURE [dbo].[GetStyles]
	@FeatureTypeId int
AS
SELECT 
	[Id], 
	[FeatureTypeId], 
	[AttributeIndex], 
	[StyleTypeId],
	[Name]
FROM [dbo].[Styles]
WHERE [FeatureTypeId] = @FeatureTypeId
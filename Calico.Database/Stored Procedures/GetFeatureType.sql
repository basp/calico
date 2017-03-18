CREATE PROCEDURE [dbo].[GetFeatureType]
	@Id int
AS
SELECT 
	[Id], 
	[TenantId], 
	[Name]
FROM [dbo].[FeatureTypes]
WHERE [Id] = @Id
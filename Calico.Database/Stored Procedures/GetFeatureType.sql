CREATE PROCEDURE [dbo].[GetFeatureType]
	@Id int
AS
SELECT [Id], [ClientId], [Name]
FROM [dbo].[FeatureTypes]
WHERE [Id] = @Id
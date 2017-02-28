CREATE PROCEDURE [dbo].[GetFeatureType]
	@Id INT
AS
SELECT [Id], [ClientId], [Name]
FROM [dbo].[FeatureTypes]
WHERE [Id] = @Id
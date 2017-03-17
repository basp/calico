CREATE PROCEDURE [dbo].[GetFeatureTypes]
	@ClientId int,
	@Top int
AS
SELECT TOP(@Top) [Id], [ClientId], [Name]
FROM [dbo].[FeatureTypes]
WHERE [ClientId] = @ClientId
ORDER BY [Id]
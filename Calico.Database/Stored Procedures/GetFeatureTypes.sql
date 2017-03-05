CREATE PROCEDURE [dbo].[GetFeatureTypes]
	@ClientId int,
	@Top int
AS
SELECT TOP(@Top) [Id], [ClientId], [Name]
FROM [dbo].[FeatureTypes]
ORDER BY [Id]
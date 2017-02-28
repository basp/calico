CREATE PROCEDURE [dbo].[GetFeatureTypes]
	@ClientId INT,
	@Top INT
AS
SELECT TOP(@Top) [Id], [ClientId], [Name]
FROM [dbo].[FeatureTypes]
ORDER BY [Id]
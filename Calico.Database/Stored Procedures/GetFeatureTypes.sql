CREATE PROCEDURE [dbo].[GetFeatureTypes]
	@TenantId int,
	@Top int
AS
SELECT TOP(@Top) 
	[Id], 
	[TenantId], 
	[Name]
FROM [dbo].[FeatureTypes]
WHERE [TenantId] = @TenantId
ORDER BY [Id]
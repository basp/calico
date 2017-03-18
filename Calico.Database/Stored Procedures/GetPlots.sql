CREATE PROCEDURE [dbo].[GetPlots]
	@TenantId int,
	@Top int
AS
SELECT TOP(@Top) 
	[Id], 
	[TenantId], 
	[Name], 
	[Geometry].ToString() AS Wkt,
	[SRID]
FROM [dbo].[Plots]
WHERE [TenantId] = @TenantId
ORDER BY [Id]
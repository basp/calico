CREATE PROCEDURE [dbo].[GetPlotsContainingGeometry]
	@TenantId int,
	@Geometry geometry
AS
SELECT 
	[Id], 
	[TenantId], 
	[Name], 
	[Geometry].ToString() AS Wkt,
	[SRID]
FROM [dbo].[Plots]
WHERE [TenantId ] = @TenantId
AND [Geometry].STContains(@Geometry) = 1
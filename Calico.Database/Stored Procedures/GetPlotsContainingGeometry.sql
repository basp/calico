CREATE PROCEDURE [dbo].[GetPlotsContainingGeometry]
	@ClientId int,
	@Geometry geometry
AS
SELECT 
	[Id], 
	[ClientId], 
	[Name], 
	[Geometry].ToString() AS Wkt,
	[SRID]
FROM [dbo].[Plots]
WHERE [ClientId ] = @ClientId
AND [Geometry].STContains(@Geometry) = 1
CREATE PROCEDURE [dbo].[InsertPlot]
	@TenantId int,
	@Name nvarchar(MAX),
	@Geometry geometry,
	@Geography geography,
	@SRID int
AS
INSERT INTO [dbo].[Plots] (
	[TenantId], 
	[Name], 
	[Geometry],
	[Geography],
	[SRID])
VALUES (
	@TenantId, 
	@Name, 
	@Geometry, 
	@Geography, 
	@SRID)
SELECT CAST(SCOPE_IDENTITY() AS int)

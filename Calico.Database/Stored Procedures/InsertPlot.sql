CREATE PROCEDURE [dbo].[InsertPlot]
	@ClientId int,
	@Name nvarchar(MAX),
	@Geometry geometry,
	@Geography geography,
	@SRID int
AS
INSERT INTO [dbo].[Plots] (
	[ClientId], 
	[Name], 
	[Geometry],
	[Geography],
	[SRID])
VALUES (@ClientId, @Name, @Geometry, @Geography, @SRID)
SELECT CAST(SCOPE_IDENTITY() AS int)

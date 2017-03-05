CREATE PROCEDURE [dbo].[InsertPlot]
	@ClientId INT,
	@Name NVARCHAR(MAX),
	@Geometry GEOMETRY,
	@Geography GEOGRAPHY,
	@SRID INT
AS
INSERT INTO [dbo].[Plots] (
	[ClientId], 
	[Name], 
	[Geometry],
	[Geography],
	[SRID])
VALUES (@ClientId, @Name, @Geometry, @Geography, @SRID)
SELECT CAST(SCOPE_IDENTITY() AS INT)

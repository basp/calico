CREATE PROCEDURE [dbo].[InsertPlot]
	@ClientId INT,
	@Name NVARCHAR(MAX),
	@Geometry GEOMETRY,
	@SRID INT
AS
INSERT INTO [dbo].[Plots] (
	[ClientId], 
	[Name], 
	[Geometry],
	[SRID])
VALUES (@ClientId, @Name, @Geometry, @SRID)
SELECT CAST(SCOPE_IDENTITY() AS INT)

CREATE PROCEDURE [dbo].[InsertPlot]
	@ClientId INT,
	@Name NVARCHAR(MAX),
	@Geometry GEOMETRY
AS
INSERT INTO [dbo].[Plots] ([ClientId], [Name], [Geometry])
VALUES (@ClientId, @Name, @Geometry)
SELECT CAST(SCOPE_IDENTITY() AS INT)

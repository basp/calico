CREATE FUNCTION [dbo].[NearestPlots]
(
	@Target geography,
	@Top int
)
RETURNS @returntable TABLE
(
	[Id] int,
	[ClientId] int,
	[Name] nvarchar(max),
	[Wkt] nvarchar(max),
	[Distance] float(53)
)
AS
BEGIN
	INSERT @returntable
	SELECT TOP(@Top)
		[Id],
		[ClientId],
		[Name],
		[Geometry].ToString() AS Wkt,
		[Geography].STDistance(@Target)
	FROM [dbo].[Plots]
	ORDER BY [Geography].STDistance(@Target)
	RETURN
END

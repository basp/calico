CREATE PROCEDURE [dbo].[GetStyleClasses]
	@StyleId int
AS
SELECT 
	[Id], 
	[StyleId],
	[Legend],
	[Category],
	[MinValue],
	[MaxValue]
FROM [dbo].[StyleClasses]
WHERE [StyleId] = @StyleId
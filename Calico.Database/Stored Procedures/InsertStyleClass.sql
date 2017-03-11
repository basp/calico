CREATE PROCEDURE [dbo].[InsertStyleClass]
	@StyleId int,
	@Legend nvarchar(MAX),
	@Category nvarchar(MAX) = NULL,
	@MinValue float(53) = NULL,
	@MaxValue float(53) = NULL
AS
INSERT INTO [dbo].[StyleClasses] (
	[StyleId],
	[Legend],
	[Category],
	[MinValue],
	[MaxValue])
VALUES (
	@StyleId,
	@Legend,
	@Category,
	@MinValue,
	@MaxValue)
SELECT CAST(SCOPE_IDENTITY() AS INT)
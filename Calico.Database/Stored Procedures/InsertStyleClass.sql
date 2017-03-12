CREATE PROCEDURE [dbo].[InsertStyleClass]
	@StyleId int,
	@Symbol nvarchar(MAX),
	@Legend nvarchar(MAX),
	@Category nvarchar(MAX) = NULL,
	@MinValue float(53) = NULL,
	@MaxValue float(53) = NULL
AS
INSERT INTO [dbo].[StyleClasses] (
	[StyleId],
	[Legend],
	[Symbol],
	[Category],
	[MinValue],
	[MaxValue])
VALUES (
	@StyleId,
	@Symbol,
	@Legend,
	@Category,
	@MinValue,
	@MaxValue)
SELECT CAST(SCOPE_IDENTITY() AS INT)
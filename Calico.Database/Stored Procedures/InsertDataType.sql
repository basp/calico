CREATE PROCEDURE [dbo].[InsertDataType]
	@Name nvarchar(MAX),
	@SqlType nvarchar(MAX),
	@BclType nvarchar(MAX)
AS
INSERT INTO [dbo].[DataTypes] (
	[Name], 
	[SqlType], 
	[BclType])
VALUES (
	@Name, 
	@SqlType, 
	@BclType)
SELECT CAST(SCOPE_IDENTITY() AS int)
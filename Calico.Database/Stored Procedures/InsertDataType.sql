CREATE PROCEDURE [dbo].[InsertDataType]
	@Name NVARCHAR(MAX),
	@SqlType NVARCHAR(MAX),
	@BclType NVARCHAR(MAX)
AS
INSERT INTO [dbo].[DataTypes] ([Name], [SqlType], [BclType])
VALUES (@Name, @SqlType, @BclType)
SELECT CAST(SCOPE_IDENTITY() AS INT)
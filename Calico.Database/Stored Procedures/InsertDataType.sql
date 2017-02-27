CREATE PROCEDURE [dbo].[InsertDataType]
	@Id INT ,
	@Name NVARCHAR(MAX),
	@SqlType NVARCHAR(MAX),
	@BclType NVARCHAR(MAX)
AS
INSERT INTO [dbo].[DataTypes] ([Id], [Name], [SqlType], [BclType])
VALUES (@Id, @Name, @SqlType, @BclType)
SELECT @Id
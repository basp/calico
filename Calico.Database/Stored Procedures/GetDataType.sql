CREATE PROCEDURE [dbo].[GetDataType]
	@Id INT
AS
SELECT 
	[Id], 
	[Name], 
	[SqlType], 
	[BclType]
FROM [dbo].[DataTypes] 
WHERE [Id] = @Id
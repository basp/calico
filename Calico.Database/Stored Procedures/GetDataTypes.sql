CREATE PROCEDURE [dbo].[GetDataTypes]
	@Top int = 100
AS
SELECT TOP(@Top)
	[Id], 
	[Name], 
	[SqlType], 
	[BclType]
FROM [dbo].[DataTypes]
ORDER BY [Id]
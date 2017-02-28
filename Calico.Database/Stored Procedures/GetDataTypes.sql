CREATE PROCEDURE [dbo].[GetDataTypes]
AS
SELECT [Id], [Name], [SqlType], [BclType]
FROM [dbo].[DataTypes]
ORDER BY [Id]
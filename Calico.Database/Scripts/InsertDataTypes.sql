IF NOT EXISTS(SELECT [Id] FROM [dbo].[DataTypes] WHERE [Name] = N'Double')
EXEC InsertDataType 
	@Name = N'Double', 
	@SqlType = N'FLOAT', 
	@BclType = N'System.Double'

IF NOT EXISTS(SELECT [Id] FROM [dbo].[DataTypes] WHERE [Name] = N'Long')
EXEC InsertDataType
	@Name = N'Long',
	@SqlType = N'BIGINT',
	@BclType = N'System.Int64'


IF NOT EXISTS(SELECT [Id] FROM [dbo].[DataTypes] WHERE [Name] = N'String')
EXEC InsertDataType 
	@Name = N'String', 
	@SqlType = N'VARCHAR', 
	@BclType = N'System.String'

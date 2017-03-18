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

IF NOT EXISTS(SELECT [Id] FROM [dbo].StyleTypes WHERE [Name] = N'Categorized')
INSERT INTO [dbo].[StyleTypes] ([Name]) VALUES (N'Categorized')

IF NOT EXISTS(SELECT [Id] FROM [dbo].StyleTypes WHERE [Name] = N'Graduated')
INSERT INTO [dbo].[StyleTypes] ([Name]) VALUES (N'Graduated')

IF NOT EXISTS(SELECT [Id] FROM [dbo].Tenants WHERE [Id] = 1)
INSERT INTO [dbo].[Tenants] ([Name]) VALUES (N'System')
CREATE TABLE [dbo].[Styles]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [FeatureTypeId] NVARCHAR(MAX) NOT NULL, 
	[AttributeIndex] INT NOT NULL,
    [StyleTypeId] INT NOT NULL, 
	[Name] NVARCHAR(MAX) NOT NULL
)

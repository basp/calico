CREATE TABLE [dbo].[Styles]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [FeatureTypeId] INT NOT NULL, 
	[AttributeIndex] INT NOT NULL,
    [StyleTypeId] INT NOT NULL, 
	[Name] NVARCHAR(MAX) NOT NULL,
	CONSTRAINT FK_Styles_FeatureTypes FOREIGN KEY ([FeatureTypeId])
		REFERENCES [FeatureTypes]([Id]),
	CONSTRAINT FK_Styles_StyleTypes FOREIGN KEY ([StyleTypeId])
		REFERENCes [StyleTypes]([Id])
)

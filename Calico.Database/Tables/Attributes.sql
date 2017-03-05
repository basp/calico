CREATE TABLE [dbo].[Attributes]
(
	[FeatureTypeId] int NOT NULL,
	[Index] int NOT NULL,
	[DataTypeId] int NOT NULL,
	[Name] nvarchar(MAX) NOT NULL, 
    CONSTRAINT [FK_Attributes_FeatureTypes] FOREIGN KEY ([FeatureTypeId]) 
		REFERENCES [FeatureTypes]([Id]) 
		ON DELETE CASCADE,
	CONSTRAINT [FK_Attributes_DataTypes] 
		FOREIGN KEY ([DataTypeId]) 
		REFERENCES [DataTypes]([Id]), 
    CONSTRAINT [PK_Attributes] PRIMARY KEY ([FeatureTypeId], [Index]) 
)

GO
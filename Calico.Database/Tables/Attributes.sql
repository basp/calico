CREATE TABLE [dbo].[Attributes]
(
	[FeatureTypeId] INT NOT NULL,
	[Index] INT NOT NULL,
	[DataTypeId] INT NOT NULL,
	[Name] NVARCHAR(MAX) NOT NULL, 
    CONSTRAINT [FK_Attributes_FeatureTypes] FOREIGN KEY ([FeatureTypeId]) REFERENCES [FeatureTypes]([Id]),
	CONSTRAINT [FK_Attributes_DataTypes] FOREIGN KEY ([DataTypeId]) REFERENCES [DataTypes]([Id]), 
    CONSTRAINT [PK_Attributes] PRIMARY KEY ([FeatureTypeId], [Index]) 
)

GO
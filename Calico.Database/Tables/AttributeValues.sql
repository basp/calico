CREATE TABLE [dbo].[AttributeValues]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[DataSetId] INT NOT NULL,
	[AttributeId] INT NOT NULL,
	[FeatureIndex] INT NOT NULL,
	[DoubleValue] FLOAT(53) NULL,
	[LongValue] BIGINT NULL,
	[StringValue] NVARCHAR(MAX) NULL, 
    CONSTRAINT [FK_AttributeValues_Attributes] FOREIGN KEY ([AttributeId]) REFERENCES [Attributes]([Id])
		ON DELETE CASCADE,
	CONSTRAINT [FK_AttributeValues_DataSets] FOREIGN KEY ([DataSetId]) REFERENCES [DataSets]([Id])
		ON DELETE CASCADE, 
    CONSTRAINT [PK_AttributeValues] PRIMARY KEY ([Id])
)

GO

CREATE UNIQUE INDEX [IX_AttributeValues_Column] ON [dbo].[AttributeValues] ([DataSetId], [AttributeId], [FeatureIndex])

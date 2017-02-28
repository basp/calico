CREATE TABLE [dbo].[AttributeValues]
(
	[DataSetId] INT NOT NULL,
	[FeatureIndex] INT NOT NULL,
	[AttributeIndex] INT NOT NULL,
	[DoubleValue] FLOAT(53) NULL,
	[LongValue] BIGINT NULL,
	[StringValue] NVARCHAR(MAX) NULL, 
	CONSTRAINT [FK_AttributeValues_DataSets] FOREIGN KEY ([DataSetId]) REFERENCES [DataSets]([Id])
		ON DELETE CASCADE, 
    CONSTRAINT [PK_AttributeValues] PRIMARY KEY ([DataSetId], [FeatureIndex], [AttributeIndex])
)

GO
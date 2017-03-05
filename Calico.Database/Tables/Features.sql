CREATE TABLE [dbo].[Features]
(
	[DataSetId] INT NOT NULL,
    [Index] INT NOT NULL, 
	[Geometry] GEOMETRY NOT NULL, 
    [Geography] [sys].[geography] NOT NULL, 
    [SRID] INT NOT NULL, 
    CONSTRAINT [FK_Features_DataSets] FOREIGN KEY ([DataSetId]) 
		REFERENCES [DataSets]([Id]) 
		ON DELETE CASCADE, 
    CONSTRAINT [PK_Features] PRIMARY KEY ([DataSetId], [Index]), 
)

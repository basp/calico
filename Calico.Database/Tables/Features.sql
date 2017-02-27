CREATE TABLE [dbo].[Features]
(
    [Id] INT NOT NULL IDENTITY(1,1),  
	[DataSetId] INT NOT NULL,
    [Index] INT NOT NULL, 
	[Geometry] GEOMETRY NOT NULL, 
    CONSTRAINT [FK_Features_DataSets] FOREIGN KEY ([DataSetId]) REFERENCES [DataSets]([Id]), 
    CONSTRAINT [PK_Features] PRIMARY KEY ([Id]), 
)

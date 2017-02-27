CREATE TABLE [dbo].[DataSets]
(
	[Id] INT NOT NULL IDENTITY(1,1), 
	[PlotId] INT NOT NULL,
	[FeatureTypeId] INT NOT NULL,
    [Name] NVARCHAR(MAX) NOT NULL, 
    [DateCreated] DATETIME2 NOT NULL, 
    CONSTRAINT [FK_DataSets_Plots] FOREIGN KEY ([PlotId]) REFERENCES [Plots]([Id])
		ON DELETE CASCADE, 
    CONSTRAINT [FK_DataSets_FeatureTypes] FOREIGN KEY ([FeatureTypeId]) REFERENCES [FeatureTypes]([Id])
		ON DELETE CASCADE, 
    CONSTRAINT [PK_DataSets] PRIMARY KEY ([Id])
)

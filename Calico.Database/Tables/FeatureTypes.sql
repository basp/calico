CREATE TABLE [dbo].[FeatureTypes]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[ClientId] INT NOT NULL,
	[Name] NVARCHAR(MAX) NOT NULL, 
    CONSTRAINT [FK_FeatureTypes_Clients] FOREIGN KEY ([ClientId]) REFERENCES [Clients]([Id])
		ON DELETE CASCADE, 
    CONSTRAINT [PK_FeatureTypes] PRIMARY KEY ([Id])
)

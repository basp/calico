CREATE TABLE [dbo].[Plots]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[ClientId] INT NOT NULL,
	[Name] NVARCHAR(MAX) NOT NULL,
	[Geometry] GEOMETRY NOT NULL, 
    [SRID] INT NOT NULL, 
    CONSTRAINT [FK_Plots_Clients] FOREIGN KEY ([ClientId]) REFERENCES [Clients]([Id]), 
    CONSTRAINT [PK_Plots] PRIMARY KEY ([Id]) 
)

GO

CREATE TABLE [dbo].[Plots]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[TenantId] INT NOT NULL,
	[Name] NVARCHAR(MAX) NOT NULL,
	[Geometry] GEOMETRY NOT NULL, 
    [Geography] [sys].[geography] NOT NULL, 
    [SRID] INT NOT NULL, 
    CONSTRAINT [FK_Plots_Tenants] FOREIGN KEY ([TenantId]) 
		REFERENCES [Tenants]([Id]), 
    CONSTRAINT [PK_Plots] PRIMARY KEY ([Id]) 
)

GO

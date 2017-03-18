CREATE TABLE [dbo].[FeatureTypes]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[TenantId] INT NOT NULL,
	[Name] NVARCHAR(MAX) NOT NULL, 
    CONSTRAINT [FK_FeatureTypes_Tenants] FOREIGN KEY ([TenantId]) 
		REFERENCES [Tenants]([Id])
		ON DELETE CASCADE, 
    CONSTRAINT [PK_FeatureTypes] PRIMARY KEY ([Id])
)

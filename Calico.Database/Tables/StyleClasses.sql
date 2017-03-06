CREATE TABLE [dbo].[StyleClasses]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [StringValue] NVARCHAR(MAX) NULL, 
    [MinValue] FLOAT(53) NULL, 
    [MaxValue] FLOAT(53) NULL
)

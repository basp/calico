CREATE TABLE [dbo].[StyleClasses]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [StyleId] INT NOT NULL,
    [Symbol] NVARCHAR(MAX) NOT NULL, 
	[Legend] NVARCHAR(MAX) NOT NULL, 
    [Category] NVARCHAR(MAX) NULL, 
    [MinValue] FLOAT(53) NULL, 
    [MaxValue] FLOAT(53) NULL,
    CONSTRAINT FK_StyleClasses_Styles FOREIGN KEY ([StyleId]) 
		REFERENCES [Styles]([Id])
)

CREATE PROCEDURE [dbo].[GetClient]
	@Id int
AS
SELECT TOP 1 [Id], [Name]
FROM [dbo].[Clients]
WHERE [Id] = @Id
CREATE PROCEDURE [dbo].[GetClient]
	@Id INT
AS
SELECT TOP 1 [Id], [Name]
FROM [dbo].[Clients]
WHERE [Id] = @Id
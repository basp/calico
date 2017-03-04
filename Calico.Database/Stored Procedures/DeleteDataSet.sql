CREATE PROCEDURE [dbo].[DeleteDataSet]
	@Id INT
AS
DELETE FROM [dbo].[DataSets]
WHERE [Id] = @Id
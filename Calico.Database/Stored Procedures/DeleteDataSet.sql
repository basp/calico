CREATE PROCEDURE [dbo].[DeleteDataSet]
	@Id int
AS
DELETE FROM [dbo].[DataSets]
WHERE [Id] = @Id
CREATE PROCEDURE [dbo].[DeletePlot]
	@Id INT
AS
DELETE FROM [dbo].[Plots]
WHERE [Id] = @Id
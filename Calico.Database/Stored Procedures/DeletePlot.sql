CREATE PROCEDURE [dbo].[DeletePlot]
	@Id int
AS
DELETE FROM [dbo].[Plots]
WHERE [Id] = @Id
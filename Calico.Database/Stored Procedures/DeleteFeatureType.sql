CREATE PROCEDURE [dbo].[DeleteFeatureType]
	@Id INT
AS
DELETE FROM [dbo].[FeatureTypes] 
WHERE [Id] = @Id
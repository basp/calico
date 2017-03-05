CREATE PROCEDURE [dbo].[DeleteFeatureType]
	@Id int
AS
DELETE FROM [dbo].[FeatureTypes] 
WHERE [Id] = @Id
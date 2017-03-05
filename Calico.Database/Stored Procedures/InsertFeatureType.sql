CREATE PROCEDURE [dbo].[InsertFeatureType]
	@ClientId int,
	@Name nvarchar(MAX)
AS
INSERT INTO [dbo].[FeatureTypes] ([ClientId], [Name])
VALUES (@ClientId, @Name)
SELECT CAST(SCOPE_IDENTITY() AS int)
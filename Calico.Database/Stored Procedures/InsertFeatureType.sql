CREATE PROCEDURE [dbo].[InsertFeatureType]
	@ClientId INT,
	@Name NVARCHAR(MAX)
AS
INSERT INTO [dbo].[FeatureTypes] ([ClientId], [Name])
VALUES (@ClientId, @Name)
SELECT CAST(SCOPE_IDENTITY() AS INT)
CREATE PROCEDURE [dbo].[InsertAttribute]
	@FeatureTypeId INT,
	@DataTypeId INT,
	@Index INT,
	@Name NVARCHAR(MAX)
AS
INSERT INTO [dbo].[Attributes] ([DataTypeId], [FeatureTypeId], [Index], [Name])
VALUES (@DataTypeId, @FeatureTypeId, @Index, @Name)
SELECT CAST(SCOPE_IDENTITY() AS INT)
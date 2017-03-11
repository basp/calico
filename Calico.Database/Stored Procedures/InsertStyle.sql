CREATE PROCEDURE [dbo].[InsertStyle]
	@FeatureTypeId INT,
	@AttributeIndex INT,
	@StyleTypeId INT,
	@Name NVARCHAR(MAX)
AS
INSERT INTO [dbo].[Styles] (
	FeatureTypeId, 
	AttributeIndex, 
	StyleTypeId, 
	[Name])
VALUES (
	@FeatureTypeId, 
	@AttributeIndex, 
	@StyleTypeId, 
	@Name)
SELECT CAST(SCOPE_IDENTITY() AS INT)
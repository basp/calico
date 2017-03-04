CREATE VIEW [Info].[Attributes]
AS 
SELECT 
	[FeatureTypes].Id AS FeatureTypeId,
	[FeatureTypes].[Name] AS FeatureTypeName,
	[Attributes].[Index] AS AttributeId,
	[Attributes].[Name] AS AttributeName,
	[DataTypes].[Id] AS DataTypeId,
	[DataTypes].[Name] AS DataTypeName
FROM [dbo].[Attributes] AS Attributes
INNER JOIN [dbo].[FeatureTypes] AS FeatureTypes
	ON Attributes.FeatureTypeId = FeatureTypes.Id
INNER JOIN [dbo].[DataTypes] AS DataTypes
	ON Attributes.DataTypeId = DataTypes.Id
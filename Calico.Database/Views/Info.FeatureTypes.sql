CREATE VIEW [Info].[FeatureTypes]
AS 
SELECT
	Clients.Id AS ClientId,
	Clients.[Name] AS ClientName,
	FeatureTypes.Id AS FeatureTypeId,
	FeatureTypes.[Name] AS FeatureTypeName
FROM [dbo].[FeatureTypes] AS FeatureTypes
INNER JOIN [dbo].[Clients] AS Clients
	ON FeatureTypes.ClientId = Clients.Id

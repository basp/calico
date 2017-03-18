CREATE VIEW [Info].[FeatureTypes]
AS 
SELECT
	Tenants.Id AS TenantId,
	Tenants.[Name] AS TenantName,
	FeatureTypes.Id AS FeatureTypeId,
	FeatureTypes.[Name] AS FeatureTypeName
FROM [dbo].[FeatureTypes] AS FeatureTypes
INNER JOIN [dbo].Tenants AS Tenants
	ON FeatureTypes.TenantId = Tenants.Id

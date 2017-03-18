CREATE VIEW [Info].[DataSets]
AS
SELECT
	Tenants.Id AS TenantId,
	Tenants.[Name] AS TenantName,
	Plots.Id AS PlotId,
	Plots.[Name] AS PlotName,
	FeatureTypes.Id AS FeatureTypeId,
	FeatureTypes.[Name] AS FeatureTypeName,
	DataSets.Id AS DataSetId,
	DataSets.[Name] AS DataSetName
FROM [dbo].DataSets AS DataSets
INNER JOIN [dbo].Plots AS Plots
	ON DataSets.PlotId = Plots.Id
INNER JOIN [dbo].FeatureTypes AS FeatureTypes
	ON DataSets.FeatureTypeId = FeatureTypes.Id
INNER JOIN [dbo].Tenants AS Tenants
	ON Plots.TenantId = Tenants.Id
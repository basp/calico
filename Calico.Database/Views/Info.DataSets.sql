CREATE VIEW [Info].[DataSets]
AS
SELECT
	Clients.Id AS ClientId,
	Clients.[Name] AS ClientName,
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
INNER JOIN [dbo].Clients AS Clients
	ON Plots.ClientId = Clients.Id
CREATE PROCEDURE [dbo].[GetAttributeValues]
	@DataSetId INT
AS
SELECT
	[DataSetId],
	[FeatureIndex],
	[AttributeIndex],
	[DoubleValue],
	[LongValue],
	[StringValue]
FROM [dbo].[AttributeValues]
WHERE [DataSetId] = @DataSetId
ORDER BY [FeatureIndex], [AttributeIndex]
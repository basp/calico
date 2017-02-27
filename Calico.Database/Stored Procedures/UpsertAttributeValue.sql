CREATE PROCEDURE [dbo].[UpsertAttributeValue]
	@DataSetId INT,
	@AttributeId INT,
	@FeatureIndex INT,
	@DoubleValue FLOAT(53) = NULL,
	@LongValue BIGINT = NULL,
	@StringValue NVARCHAR(MAX) = NULL
AS
INSERT INTO [dbo].[AttributeValues] (
	[DataSetId],
	[AttributeId],
	[FeatureIndex],
	[DoubleValue],
	[LongValue],
	[StringValue])
VALUES (
	@DataSetId,
	@AttributeId,
	@FeatureIndex,
	@DoubleValue,
	@LongValue,
	@StringValue)
SELECT @@ROWCOUNT
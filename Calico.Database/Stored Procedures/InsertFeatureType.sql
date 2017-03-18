CREATE PROCEDURE [dbo].[InsertFeatureType]
	@TenantId int,
	@Name nvarchar(MAX)
AS
INSERT INTO [dbo].[FeatureTypes] (
	[TenantId], 
	[Name])
VALUES (@TenantId, @Name)
SELECT CAST(SCOPE_IDENTITY() AS int)
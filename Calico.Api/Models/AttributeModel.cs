namespace Calico.Api.Models
{
	public class AttributeModel
	{
		public int FeatureTypeId { get; set; }

		public int Index { get; set; }

		public int DataTypeId { get; set; }

		public string Name { get; set; }

        public DataTypeModel DataType { get; set; }
	}
}
using Microsoft.SqlServer.Types;

namespace Calico
{
    public class PlotRecord
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public string Name { get; set; }

        public SqlGeometry Geometry { get; set; }
    }
}

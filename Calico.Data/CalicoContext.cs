namespace Calico.Data
{
    using System.Data.Entity;

    public class CalicoContext : DbContext
    {
        public CalicoContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public virtual DbSet<Client> Clients { get; set; }

        public virtual DbSet<DataType> DataTypes { get; set; }

        public virtual DbSet<DataSet> DataSets { get; set; }

        public virtual DbSet<Feature> Features { get; set; }

        public virtual DbSet<FeatureType> FeatureTypes { get; set; }
    }
}

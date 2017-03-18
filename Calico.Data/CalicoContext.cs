namespace Calico.Data
{
    using System;
    using System.Data.Entity;

    public class CalicoContext : DbContext
    {
        public CalicoContext()
            : this("Name=dev")
        {
        }

        public CalicoContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public virtual DbSet<Attribute> Attributes { get; set; }

        public virtual DbSet<AttributeValue> AttributeValues { get; set; }

        public virtual DbSet<Tenant> Tenants { get; set; }

        public virtual DbSet<DataType> DataTypes { get; set; }

        public virtual DbSet<DataSet> DataSets { get; set; }

        public virtual DbSet<Feature> Features { get; set; }

        public virtual DbSet<FeatureType> FeatureTypes { get; set; }

        public virtual DbSet<Plot> Plots { get; set; }
    }
}

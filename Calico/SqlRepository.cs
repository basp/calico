namespace Calico
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Dapper;

    public class SqlRepository : IRepository
    {
        private readonly SqlConnection connection;
        private readonly SqlTransaction transaction;

        public SqlRepository(
            SqlConnection connection,
            SqlTransaction transaction = null)
        {
            this.connection = connection;
            this.transaction = transaction;
        }

        public int InsertClient(ClientRecord rec)
        {
            var @param = new { rec.Name };
            return this.Insert(nameof(this.InsertClient), @param);
        }

        public int InsertDataSet(DataSetRecord rec)
        {
            var @param = new { rec.PlotId, rec.FeatureTypeId, rec.Name, rec.DateCreated };
            return this.Insert(nameof(this.InsertDataSet), @param);
        }

        public int InsertDataType(DataTypeRecord rec)
        {
            var @param = new { rec.Name, rec.SqlType, rec.BclType };
            return this.Insert(nameof(this.InsertDataType), @param);
        }

        public int InsertFeatureType(FeatureTypeRecord rec)
        {
            var @param = new { rec.ClientId, rec.Name };
            return this.Insert(nameof(this.InsertFeatureType), @param);
        }

        public int InsertPlot(PlotRecord rec)
        {
            var @param = new { rec.ClientId, rec.Name, rec.Geometry };
            return this.Insert(nameof(this.InsertPlot), @param);
        }

        private int Insert(string sproc, object @param)
        {
            return this.connection.QueryFirst<int>(
                sproc,
                @param,
                commandType: CommandType.StoredProcedure,
                transaction: this.transaction);
        }
    }
}

// <copyright file="SqlSession.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Dapper;

    public class SqlSession : ISession, IDisposable
    {
        private readonly SqlConnection connection;
        private readonly SqlTransaction transaction;
        private bool disposed = false;

        private SqlSession(SqlConnection connection, SqlTransaction transaction)
        {
            this.connection = connection;
            this.transaction = transaction;
        }

        public SqlConnection Connection => this.connection;

        public SqlTransaction Transaction => this.transaction;

        public static SqlSession Open(SqlConnection connection)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            return new SqlSession(connection, connection.BeginTransaction());
        }

        public int BulkCopy(string tableName, DataTable table)
        {
            var opts = SqlBulkCopyOptions.Default;
            using (var copy = new SqlBulkCopy(this.connection, opts, this.transaction))
            {
                copy.DestinationTableName = tableName;
                copy.WriteToServer(table);
            }

            return table.Rows.Count;
        }

        public void Commit()
        {
            this.transaction.Commit();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int Execute(string sproc, object @param = null)
        {
            return this.connection.Execute(
                sproc,
                @param,
                commandType: CommandType.StoredProcedure,
                transaction: this.transaction);
        }

        public int Insert(string sproc, object @param = null)
        {
            return this.connection.QueryFirst<int>(
                sproc,
                @param,
                commandType: CommandType.StoredProcedure,
                transaction: this.transaction);
        }

        public IEnumerable<T> Query<T>(string sproc, object @param = null)
        {
            return this.connection.Query<T>(
                sproc,
                @param,
                commandType: CommandType.StoredProcedure,
                transaction: this.transaction);
        }

        public T QuerySingle<T>(string sproc, object @param = null)
        {
            return this.connection.QuerySingle<T>(
                sproc,
                @param,
                commandType: CommandType.StoredProcedure,
                transaction: this.transaction);
        }

        public void Rollback()
        {
            this.transaction.Rollback();
        }

        protected void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.transaction.Dispose();
                this.connection.Dispose();
            }

            this.disposed = true;
        }
    }
}

// <copyright file="ISession.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System.Collections.Generic;
    using System.Data;

    public interface ISession
    {
        void Commit();

        void Rollback();

        int BulkCopy(string tableName, DataTable table);

        int Execute(string sproc, object @param = null);

        int Insert(string sproc, object @param);

        T QuerySingle<T>(string sproc, object @param = null);

        IEnumerable<T> Query<T>(string sproc, object @param = null);
    }
}

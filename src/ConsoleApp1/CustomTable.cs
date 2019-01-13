using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqToDB;
using LinqToDB.Data;

namespace ConsoleApp1
{
    public class CustomTable<T> : IQueryable<T>
        where T : class
    {
        private readonly DataConnection _dataConnection;
        private readonly ITable<T> _table;

        internal CustomTable(DataConnection dataConnection, ITable<T> table)
        {
            _dataConnection = dataConnection ?? throw new ArgumentNullException(nameof(dataConnection));
            _table = table ?? throw new ArgumentNullException(nameof(table));
        }

        public void Insert(T entity)
        {
            _dataConnection.Insert(entity);
        }

        public void Update(T entity)
        {
            _dataConnection.Update(entity);
        }

        public void Delete(T entity)
        {
            _dataConnection.Delete(entity);
        }

        #region IQueryable Members

        public Type ElementType => _table.ElementType;

        public Expression Expression => _table.Expression;

        public IQueryProvider Provider => _table.Provider;

        public IEnumerator<T> GetEnumerator() => _table.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _table.GetEnumerator();

        #endregion
    }
}

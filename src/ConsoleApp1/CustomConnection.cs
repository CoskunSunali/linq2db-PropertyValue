using System;
using System.Reflection;
using LinqToDB;
using LinqToDB.Data;

namespace ConsoleApp1
{
    public class CustomConnection
    {
        private readonly static MethodInfo _s_getTableMethodInfo = typeof(DataConnection).GetMethod(nameof(DataConnection.GetTable));
        private readonly static MethodInfo _s_createTableMethodInfo = typeof(DataConnection).GetMethod(nameof(DataExtensions.CreateTable));

        private readonly DataConnection _dataConnection;

        public CustomConnection()
        {
            _dataConnection = new DataConnection(
                @"Server=.\sqlexpress2016;Database=TestDatabase;User Id=TestUser;Password=P@ssw0rd;MultipleActiveResultSets=True;Enlist=False;");
        }

        public CustomTable<T> GetTable<T>()
            where T : class
        {
            var table = _dataConnection.GetTable<T>();

            return new CustomTable<T>(_dataConnection, table);
        }

        public CustomTable<BaseEntity> GetTable(string entityTypeName)
        {
            var entityType = GetDynamicType(entityTypeName);
            var getTableGenericMethod = _s_getTableMethodInfo.MakeGenericMethod(entityType);

            var table = getTableGenericMethod.Invoke(_dataConnection, new object[] { }) as ITable<BaseEntity>;

            return new CustomTable<BaseEntity>(_dataConnection, table);
        }

        public CustomTable<T> CreateTable<T>()
            where T : class
        {
            var table = _dataConnection.CreateTable<T>();

            return new CustomTable<T>(_dataConnection, table);
        }

        public CustomTable<BaseEntity> CreateTable(string entityTypeName)
        {
            var entityType = GetDynamicType(entityTypeName);
            var createTableGenericMethod = _s_createTableMethodInfo.MakeGenericMethod(entityType);

            var table = createTableGenericMethod.Invoke(
                _dataConnection,
                new object[] {
                    Type.Missing,
                    Type.Missing,
                    Type.Missing,
                    Type.Missing,
                    Type.Missing,
                    Type.Missing })
                as ITable<BaseEntity>;

            return new CustomTable<BaseEntity>(_dataConnection, table);
        }

        private Type GetDynamicType(string entityTypeName)
        {
            // Doesn't generate assembly or emit. For now.
            return Type.GetType(entityTypeName);
        }
    }
}

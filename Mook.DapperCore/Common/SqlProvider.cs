using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Concurrent;
using System.Data.SqlClient;

namespace Mook.DapperCore
{
    public static class SqlProvider
    {
        /// <summary>
        /// 数据库提供器连接对象类型集合
        /// </summary>
        internal static readonly ConcurrentDictionary<DatabaseType, Type> SqlProviderDbConnectionTypeCollection;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static SqlProvider()
        {
            SqlProviderDbConnectionTypeCollection = new ConcurrentDictionary<DatabaseType, Type>();
        }

        /// <summary>
        /// 获取数据库连接对象类型
        /// </summary>
        /// <param name="sqlProvider"></param>
        /// <returns></returns>
        internal static Type GetDbConnectionType(DatabaseType sqlProvider)
        {
            return SqlProviderDbConnectionTypeCollection.GetOrAdd(sqlProvider, Function);
            static Type Function(DatabaseType sqlProvider)
            {
                dynamic databaseDbConnectionType = sqlProvider switch
                {
                    DatabaseType.SqlServer => new SqlConnection(),
                    DatabaseType.Oracle => new OracleConnection(),
                    DatabaseType.MySql => new MySqlConnection(),
                    DatabaseType.SQLite => new SqliteConnection(),
                    DatabaseType.PostgreSql => new NpgsqlConnection(),
                    _ => throw new ArgumentException($"Invalid database type `{sqlProvider}`."),
                };
                return databaseDbConnectionType.GetType();
            }
        }
    }
}

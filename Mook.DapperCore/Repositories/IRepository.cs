using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Mook.DapperCore
{
    public partial interface IRepository
    {
        /// <summary>
        /// 设置数据库
        /// </summary>
        void UseDatabase(string dbConfigName);

        /// <summary>
        /// 数据库连接对象
        /// </summary>
        IDbConnection DbConn { get; }

        /// <summary>
        /// 开启事务，单例使用
        /// </summary>
        /// <returns></returns>
        IDbTransaction BeginTransaction();

        /// <summary>
        /// 同步执行命令返回影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// 异步执行命令返回影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// 同步执行查询返回DataTable类型
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        DataTable ExecuteDataTable(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// 异步执行查询返回DataTable类型
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        Task<DataTable> ExecuteDataTableAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// 同步执行命令返回结果集中第一行的第一列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        object ExecuteScalar(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// 异步执行命令返回结果集中第一行的第一列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        Task<object> ExecuteScalarAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// 同步查询返回多条动态类型记录
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        IEnumerable<dynamic> Query(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// 异步查询返回多条动态类型记录
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> QueryAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// 同步查询返回多条泛型记录
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// 异步查询返回多条泛型记录
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// 同步查询返回一条动态类型记录
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        dynamic QueryFirstOrDefault(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// 异步查询返回一条动态类型记录
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        Task<dynamic> QueryFirstOrDefaultAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// 同步查询返回一条泛型记录
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        T QueryFirstOrDefault<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// 异步查询返回一条泛型记录
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// 同步分页查询返回泛型集合
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        Tuple<IEnumerable<T>, int> QueryPage<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// 异步分页查询返回泛型集合
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        Task<Tuple<IEnumerable<T>, int>> QueryPageAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
    }

    /// <summary>
    /// 泛型仓储接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial interface IRepository<T> : IRepository where T : class, new()
    {
        /// <summary>
        /// 同步获取一条
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        T Get(object id, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 异步获取一条
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        Task<T> GetAsync(object id, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 同步根据条件获取集合
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <param name="orCondition"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        IEnumerable<T> GetList(object filterCondition, bool orCondition = false, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 异步根据条件获取集合
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <param name="orCondition"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListAsync(object filterCondition, bool orCondition = false, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 同步获取所有
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        IEnumerable<T> GetAll(IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 异步获取所有
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync(IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 同步新增一条
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        int Insert(T entity, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 异步新增一条
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        Task<int> InsertAsync(T entity, IDbTransaction transaction = null, int? commandTimeout = null, ISqlAdapter sqlAdapter = null);

        /// <summary>
        /// 同步新增多条
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        int Insert(IEnumerable<T> entities, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 异步新增多条
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="sqlAdapter"></param>
        /// <returns></returns>
        Task<int> InsertAsync(IEnumerable<T> entities, IDbTransaction transaction = null, int? commandTimeout = null, ISqlAdapter sqlAdapter = null);

        /// <summary>
        /// 同步更新一条
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        bool Update(T entity, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 异步更新一条
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(T entity, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 同步更新多条
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        bool Update(IEnumerable<T> entities, IDbTransaction transaction = null, int? commandTimeout = null);
        
        /// <summary>
        /// 异步更新多条
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(IEnumerable<T> entities, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 同步更新部分字段
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filterCondition"></param>
        /// <param name="updateSelf"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        bool Update(object data, object filterCondition = null, bool updateSelf = false, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 异步更新部分字段
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filterCondition"></param>
        /// <param name="updateSelf"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(object data, object filterCondition = null, bool updateSelf = false, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 同步删除一条
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        bool Delete(T entity, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 异步删除一条
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(T entity, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 同步删除多条
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        bool Delete(IEnumerable<T> entities, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 异步删除多条
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(IEnumerable<T> entities, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 同步指定条件删除
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        bool Delete(object filterCondition, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 异步指定条件删除
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(object filterCondition, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 同步查询分页数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fields"></param>
        /// <param name="strWhere"></param>
        /// <param name="pageInfo"></param>
        /// <param name="trans"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        List<T> GetPage(string tableName, string fields, string strWhere, PageParam pageInfo, IDbTransaction trans = null, int? commandTimeout = null);

        /// <summary>
        /// 异步查询分页数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fields"></param>
        /// <param name="strWhere"></param>
        /// <param name="pageInfo"></param>
        /// <param name="trans"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        Task<List<T>> GetPageAsync(string tableName, string fields, string strWhere, PageParam pageInfo, IDbTransaction trans = null, int? commandTimeout = null);
    }
}

using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Mook.DapperCore
{
    public partial class Repository : IRepository
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        public DatabaseType _dbType;

        /// <summary>
        /// 数据库连接对象
        /// </summary>
        protected IDbConnection _dbConn;

        /// <summary>
        /// 数据库配置信息
        /// </summary>
        private readonly IOptions<DatabaseConfig> _option;

        /// <summary>
        /// 依赖注入数据库配置
        /// </summary>
        /// <param name="option"></param>
        public Repository(IOptions<DatabaseConfig> option)
        {
            _option = option;
            if (_option.Value.ConnectionConfigs == null)
            {
                CreateConnection(_option.Value.DatabaseType, _option.Value.ConnectionString);
            }
        }

        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="databaseType"></param>
        /// <param name="connectionString"></param>
        private void CreateConnection(DatabaseType databaseType, string connectionString)
        {
            try
            {
                _dbConn = Activator.CreateInstance(SqlProvider.GetDbConnectionType(databaseType), new[] { connectionString }) as IDbConnection;
                _dbType = _option.Value.DatabaseType;
            }
            catch { throw; }
        }

        /// <summary>
        /// 设置数据库
        /// </summary>
        /// <param name="dbConfigName">数据库配置名称</param>
        public void UseDatabase(string dbConfigName)
        {
            if (_option.Value.ConnectionConfigs != null)
            {
                for (int i = 0; i < _option.Value.ConnectionConfigs.Count; i++)
                {
                    if (_option.Value.ConnectionConfigs[i].DatabaseConfigName == dbConfigName)
                    {
                        CreateConnection(_option.Value.ConnectionConfigs[i].DatabaseType, _option.Value.ConnectionConfigs[i].ConnectionString);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 数据库连接对象
        /// </summary>
        public virtual IDbConnection DbConn { get { return _dbConn; } }

        /// <summary>
        /// 开启事务，单例使用
        /// </summary>
        /// <returns></returns>
        public virtual IDbTransaction BeginTransaction()
        {
            try
            {
                _dbConn.Open();
                return _dbConn.BeginTransaction();
            }
            catch { throw; }
        }

        /// <summary>
        /// 同步执行命令返回影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _dbConn.Execute(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// 异步执行命令返回影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _dbConn.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// 同步执行查询返回DataTable类型
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual DataTable ExecuteDataTable(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var reader = _dbConn.ExecuteReader(sql, param, transaction, commandTimeout, commandType);
            DataTable dt = new();
            dt.Load(reader);
            return dt;
        }

        /// <summary>
        /// 异步执行查询返回DataTable类型
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual async Task<DataTable> ExecuteDataTableAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var reader = await _dbConn.ExecuteReaderAsync(sql, param, transaction, commandTimeout, commandType);
            DataTable dt = new();
            dt.Load(reader);
            return dt;
        }

        /// <summary>
        /// 同步执行命令返回结果集中第一行的第一列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual object ExecuteScalar(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _dbConn.ExecuteScalar(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// 异步执行命令返回结果集中第一行的第一列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual async Task<object> ExecuteScalarAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _dbConn.ExecuteScalarAsync(sql, param, transaction, commandTimeout, commandType);
        }

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
        public virtual IEnumerable<dynamic> Query(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _dbConn.Query(sql, param, transaction, buffered, commandTimeout, commandType);
        }

        /// <summary>
        /// 异步查询返回多条动态类型记录
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<dynamic>> QueryAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _dbConn.QueryAsync(sql, param, transaction, commandTimeout, commandType);
        }

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
        public virtual IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _dbConn.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
        }

        /// <summary>
        /// 异步查询返回多条泛型记录
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _dbConn.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// 同步查询返回一条动态类型记录
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual dynamic QueryFirstOrDefault(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _dbConn.QueryFirstOrDefault(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// 异步查询返回一条动态类型记录
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual async Task<dynamic> QueryFirstOrDefaultAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _dbConn.QueryFirstOrDefaultAsync(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// 同步查询返回一条泛型记录
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual T QueryFirstOrDefault<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _dbConn.QueryFirstOrDefault<T>(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// 异步查询返回一条泛型记录
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _dbConn.QueryFirstOrDefaultAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// 同步分页查询返回泛型集合
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual Tuple<IEnumerable<T>, int> QueryPage<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var multi = _dbConn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);
            int totalCount = int.Parse(multi.Read<long>().Single().ToString());
            return Tuple.Create(multi.Read<T>(), totalCount);
        }

        /// <summary>
        /// 异步分页查询返回泛型集合
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual async Task<Tuple<IEnumerable<T>, int>> QueryPageAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var multi = await _dbConn.QueryMultipleAsync(sql, param, transaction, commandTimeout, commandType);
            int totalCount = int.Parse(multi.Read<long>().Single().ToString());
            return Tuple.Create(multi.Read<T>(), totalCount);
        }        
    }

    /// <summary>
    /// 泛型仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class Repository<T> : Repository, IRepository<T> where T : class, new()
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="option"></param>
        public Repository(IOptions<DatabaseConfig> option) : base(option)
        {
        }

        /// <summary>
        /// 同步获取一条
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public virtual T Get(object id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return _dbConn.Get<T>(id, transaction, commandTimeout);
        }

        /// <summary>
        /// 异步获取一条
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public virtual async Task<T> GetAsync(object id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await _dbConn.GetAsync<T>(id, transaction, commandTimeout);
        }

        /// <summary>
        /// 同步根据条件获取集合
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <param name="orCondition"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetList(object filterCondition, bool orCondition = false, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return _dbConn.GetList<T>(filterCondition, orCondition, transaction, commandTimeout);
        }

        /// <summary>
        /// 异步根据条件获取集合
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <param name="orCondition"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetListAsync(object filterCondition, bool orCondition = false, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await _dbConn.GetListAsync<T>(filterCondition, orCondition, transaction, commandTimeout);
        }

        /// <summary>
        /// 同步获取所有
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll(IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return _dbConn.GetAll<T>(transaction, commandTimeout);
        }

        /// <summary>
        /// 异步获取所有
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync(IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await _dbConn.GetAllAsync<T>(transaction, commandTimeout);
        }

        /// <summary>
        /// 同步新增一条
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public virtual int Insert(T entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return (int)_dbConn.Insert(entity, transaction, commandTimeout);
        }

        /// <summary>
        /// 异步新增一条
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="sqlAdapter"></param>
        /// <returns></returns>
        public virtual async Task<int> InsertAsync(T entity, IDbTransaction transaction = null, int? commandTimeout = null, ISqlAdapter sqlAdapter = null)
        {
            return await _dbConn.InsertAsync(entity, transaction, commandTimeout, sqlAdapter);
        }

        /// <summary>
        /// 同步新增多条
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public virtual int Insert(IEnumerable<T> entities, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return (int)_dbConn.Insert(entities, transaction, commandTimeout);
        }

        /// <summary>
        /// 异步新增多条
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="sqlAdapter"></param>
        /// <returns></returns>
        public virtual async Task<int> InsertAsync(IEnumerable<T> entities, IDbTransaction transaction = null, int? commandTimeout = null, ISqlAdapter sqlAdapter = null)
        {
            return await _dbConn.InsertAsync(entities, transaction, commandTimeout, sqlAdapter);
        }

        /// <summary>
        /// 同步更新一条
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public virtual bool Update(T entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return _dbConn.Update(entity, transaction, commandTimeout);
        }

        /// <summary>
        /// 异步更新一条
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public virtual async Task<bool> UpdateAsync(T entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await _dbConn.UpdateAsync(entity, transaction, commandTimeout);
        }

        /// <summary>
        /// 同步更新多条
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public virtual bool Update(IEnumerable<T> entities, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return _dbConn.Update(entities, transaction, commandTimeout);
        }

        /// <summary>
        /// 异步更新多条
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public virtual async Task<bool> UpdateAsync(IEnumerable<T> entities, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await _dbConn.UpdateAsync(entities, transaction, commandTimeout);
        }

        /// <summary>
        /// 同步更新部分字段
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filterCondition"></param>
        /// <param name="updateSelf"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public virtual bool Update(object data, object filterCondition = null, bool updateSelf = false, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return _dbConn.Update<T>(data, filterCondition, updateSelf, transaction, commandTimeout);
        }

        /// <summary>
        /// 异步更新部分字段
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filterCondition"></param>
        /// <param name="updateSelf"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public virtual async Task<bool> UpdateAsync(object data, object filterCondition = null, bool updateSelf = false, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await _dbConn.UpdateAsync<T>(data, filterCondition, updateSelf, transaction, commandTimeout);
        }

        /// <summary>
        /// 同步删除一条
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public virtual bool Delete(T entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return _dbConn.Delete(entity, transaction, commandTimeout);
        }

        /// <summary>
        /// 异步删除一条
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAsync(T entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await _dbConn.DeleteAsync(entity, transaction, commandTimeout);
        }

        /// <summary>
        /// 同步删除多条
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public virtual bool Delete(IEnumerable<T> entities, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return _dbConn.Delete(entities, transaction, commandTimeout);
        }

        /// <summary>
        /// 异步删除多条
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAsync(IEnumerable<T> entities, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await _dbConn.DeleteAsync(entities, transaction, commandTimeout);
        }

        /// <summary>
        /// 同步指定条件删除
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public virtual bool Delete(object filterCondition, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return _dbConn.Delete<T>(filterCondition, transaction, commandTimeout);
        }

        /// <summary>
        /// 异步指定条件删除
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAsync(object filterCondition, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await _dbConn.DeleteAsync<T>(filterCondition, transaction, commandTimeout);
        }

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
        public virtual List<T> GetPage(string tableName, string fields, string strWhere, PageParam pageInfo, IDbTransaction trans = null, int? commandTimeout = null)
        {
            var pagerHelper = new Pager(tableName, fields, strWhere, pageInfo.SortCondition, pageInfo.PageIndex, pageInfo.PageSize);
            string pagerSql = pagerHelper.GetPagerSql(true, _dbType);
            var recordCount = _dbConn.ExecuteScalar(pagerSql, null, trans, commandTimeout);
            pageInfo.RecordCount = (int)recordCount;
            pagerSql = pagerHelper.GetPagerSql(false, _dbType);
            var result = _dbConn.Query<T>(pagerSql, null, trans, true, commandTimeout);
            return result.ToList();
        }

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
        public virtual async Task<List<T>> GetPageAsync(string tableName, string fields, string strWhere, PageParam pageInfo, IDbTransaction trans = null, int? commandTimeout = null)
        {
            var pagerHelper = new Pager(tableName, fields, strWhere, pageInfo.SortCondition, pageInfo.PageIndex, pageInfo.PageSize);
            string pagerSql = pagerHelper.GetPagerSql(true, _dbType);
            var recordCount = await _dbConn.ExecuteScalarAsync(pagerSql, null, trans, commandTimeout);
            pageInfo.RecordCount = (int)recordCount;
            pagerSql = pagerHelper.GetPagerSql(false, _dbType);
            var result = await _dbConn.QueryAsync<T>(pagerSql, null, trans, commandTimeout);
            return result.ToList();
        }
    }
}

namespace Mook.DapperCore
{
    /// <summary> 
    /// 获取不同数据库分页语句类
    /// </summary> 
    public class Pager
    {
        private string _tableName;
        private string _fields;
        private string _strWhere;
        private string _sortCondition;
        private int _pageSize;
        private int _pageIndex;
        private string _pagerSql;

        /// <summary>
        /// 表或Sql语句
        /// </summary>
        internal string TableOrSql
        {
            get
            {
                bool isSql = _tableName.ToLower().Contains("from");
                if (isSql)
                {
                    return string.Format("({0}) AA ", _tableName);
                }
                else
                {
                    return _tableName;
                }
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tableName">表名或Sql语句</param>
        /// <param name="fields">返回的列</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="sortCondition">排序条件</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页显示数量</param>
        public Pager(string tableName, string fields, string strWhere, string sortCondition, int pageIndex, int pageSize)
        {
            _tableName = tableName;
            _fields = fields;
            _strWhere = strWhere;
            _sortCondition = sortCondition;
            _pageIndex = pageIndex;
            _pageSize = pageSize;
            if (string.IsNullOrWhiteSpace(_strWhere))
            {
                _strWhere = " (1=1) ";
            }
        }

        /// <summary>
        /// Oracle分页语句
        /// </summary>
        /// <param name="isCount"></param>
        /// <returns></returns>
        private string GetOraclePagerSql(bool isCount)
        {
            if (isCount)
            {
                _pagerSql = string.Format("select count(*) as total from {0} where {1} ", TableOrSql, _strWhere);
            }
            else
            {
                string strOrder = string.Format(" order by {0}", _sortCondition);
                int minRow = _pageSize * (_pageIndex - 1);
                int maxRow = _pageSize * _pageIndex;
                string selectSql = string.Format("select {0} from {1} where {2} {3}", _fields, TableOrSql, _strWhere, strOrder);
                _pagerSql = string.Format(@"select b.* from (select a.*, rownum as rowIndex from({2}) a) b
                           where b.rowIndex > {0} and b.rowIndex <= {1}", minRow, maxRow, selectSql);
            }
            return _pagerSql;
        }

        /// <summary>
        /// SqlServer分页语句
        /// </summary>
        /// <param name="isCount"></param>
        /// <returns></returns>
        private string GetSqlServerPagerSql(bool isCount)
        {
            if (isCount)
            {
                _pagerSql = string.Format("select count(*) as total from {0} where {1} ", TableOrSql, _strWhere);
            }
            else
            {
                string strOrder = string.Format(" order by {0}", _sortCondition);
                int minRow = _pageSize * (_pageIndex - 1) + 1;
                int maxRow = _pageSize * _pageIndex;
                _pagerSql = string.Format(@"with paging as (select row_number() over ({0}) as rownumber, {1} from {2} where {3})
                    select * from paging where rownumber between {4} and {5}", strOrder, _fields, TableOrSql, _strWhere, minRow, maxRow);
                //_pagerSql = string.Format("select * from {0} where {1} order by {2} offset (({3} - 1) * {4}) rows fetch next {4} rows only", TableOrSqlWrapper, _strWhere,
                //        _sortCondition, _pageIndex, _pageSize);
            }
            return _pagerSql;
        }

        /// <summary>
        /// MySql分页语句
        /// </summary>
        /// <param name="isCount"></param>
        /// <returns></returns>
        private string GetMySqlPagerSql(bool isCount)
        {
            if (isCount)
            {
                _pagerSql = string.Format("select count(*) as total from {0} where {1}", TableOrSql, _strWhere);
            }
            else
            {
                string strOrder = string.Format(" order by {0}", _sortCondition);
                int minRow = _pageSize * (_pageIndex - 1);
                _pagerSql = string.Format("select {0} from {1} where {2} {3} limit {4},{5}", _fields, TableOrSql, _strWhere, strOrder, minRow, _pageSize);
            }
            return _pagerSql;
        }

        /// <summary>
        /// SQLite分页语句
        /// </summary>
        /// <param name="isCount"></param>
        /// <returns></returns>
        private string GetSQLitePagerSql(bool isCount)
        {
            if (isCount)
            {
                _pagerSql = string.Format("select count(*) as total from {0} where {1} ", TableOrSql, _strWhere);
            }
            else
            {
                string strOrder = string.Format(" order by {0}", _sortCondition);
                int minRow = _pageSize * (_pageIndex - 1);
                _pagerSql = string.Format("select {0} from {1} where {2} {3} limit {4},{5}",
                    _fields, TableOrSql, _strWhere, strOrder, minRow, _pageSize);
            }
            return _pagerSql;
        }

        /// <summary>
        /// 获取指定数据库的分页语句
        /// </summary>
        /// <param name="isCount">是否统计总数</param>
        /// <param name="dbType">数据库类型</param>
        public string GetPagerSql(bool isCount, DatabaseType dbType)
        {
            string sql = "";
            switch (dbType)
            {
                case DatabaseType.SqlServer:
                    sql = GetSqlServerPagerSql(isCount);
                    break;
                case DatabaseType.Oracle:
                    sql = GetOraclePagerSql(isCount);
                    break;
                case DatabaseType.MySql:
                    sql = GetMySqlPagerSql(isCount);
                    break;
                case DatabaseType.SQLite:
                    sql = GetSQLitePagerSql(isCount);
                    break;
            }
            return sql;
        }
    }
}

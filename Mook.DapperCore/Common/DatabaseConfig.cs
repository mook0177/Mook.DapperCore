using System.Collections.Generic;

namespace Mook.DapperCore
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    public class DatabaseConfig
    {
        /// <summary>
        /// 数据库配置名称
        /// </summary>
        public string DatabaseConfigName { get; set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public DatabaseType DatabaseType { get; set; }

        /// <summary>
        /// 数据库连接字符
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 数据库配置集合
        /// </summary>
        public List<DatabaseConfig> ConnectionConfigs { get; set; }
    }
}

using Microsoft.Extensions.DependencyInjection;
using System;

namespace Mook.DapperCore
{
    public static class ServiceCollectionExtensions
    {
        // <summary>
        /// 添加 Dapper 扩展
        /// </summary>
        /// <param name="services"></param>
        /// <param name="option">数据库配置</param>
        /// <returns></returns>
        public static IServiceCollection AddDapper(this IServiceCollection services, Action<DatabaseConfig> option)
        {
            services.Configure(option);
            services.AddScoped<IRepository, Repository>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }
    }
}

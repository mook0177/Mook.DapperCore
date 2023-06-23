# Mook.DapperCore - 支持Oracle同时支持多数据库实例
##### Example usage:
``` C#
var connectionConfigs = builder.Configuration.GetSection("ConnectionStringList").Get<List<DatabaseConfig>>();
//Dapper多数据库实例｛使用仓储时需指定数据库，repository.UseDatabase(dbConfigName)｝
services.AddDapper(option =>
{
    option.ConnectionConfigs = connectionConfigs;
});

//Dapper单数据库实例
services.AddDapper(option =>
{
    //option.ConnectionString = Configuration["ConnectionString"];
    option.ConnectionString = "Data Source=.;DataBase=DapperDemo;User ID=Test;PWD=Test2021";
    option.DatabaseType = DatabaseType.SqlServer;
});
``` 
``` json
"ConnectionStringList": [
    {
      "DatabaseConfigName": "A",       
      "DatabaseType": "SqlServer",
      "ConnectionString": "Data Source=...;User ID=A;Password=A"
    },
    {
      "DatabaseConfigName": "B",
      "DatabaseType": "Mysql",
      "connectionString": "Data Source=...;User ID=B;Password=B"
    },
    {
      "DatabaseConfigName": "C",
      "DatabaseType": "Oracle",
      "ConnectionString": "Data Source=...;User ID=C;Password=C"
    },
    {
      "DatabaseConfigName": "D",
      "DatabaseType": "Oracle",
      "ConnectionString": "Data Source=...;User ID=D;Password=D"
    }, 
    {
      "DatabaseConfigName": "E",
      "DatabaseType": "SqlServer",
      "ConnectionString": "Data Source=...;User ID=E;Password=E"
    }
]
```

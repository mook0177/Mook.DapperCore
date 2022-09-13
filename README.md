# Mook.DapperCore - 支持Oracle同时支持多数据库实例
##### Example usage:
``` C#
//加载数据库配置
List<DatabaseConfig> list = new();
var configs = Configuration.GetSection("ConnectionStrings");
for (int i = 1; i < 100; i++)
{
   if (!configs.GetSection(i.ToString()).Exists())
   {
      break;
   }
   DatabaseConfig config = new()
   {      
      ConnectionConfigs = new List<DatabaseConfig>(),
      DatabaseConfigName = configs.GetSection(i.ToString()).GetSection("name").Value,
      ConnectionString = configs.GetSection(i.ToString()).GetSection("connectionString").Value
   };
   string dbType = configs.GetSection(i.ToString()).GetSection("providerName").Value;
   switch (dbType)
   {
      case "SqlServer":
           config.DatabaseType = DatabaseType.SqlServer;
           break;
      case "Oracle":
           config.DatabaseType = DatabaseType.Oracle;
           break;
      case "MySql":
           config.DatabaseType = DatabaseType.MySql;
           break;
      case "SQLite":
           config.DatabaseType = DatabaseType.SQLite;
           break;
      case "PostgreSql":
           config.DatabaseType = DatabaseType.PostgreSql;
           break;
   }
   list.Add(config);
}
//Dapper多数据库实例｛使用仓储时需指定数据库，repository.UseDatabase(dbConfigName)｝
services.AddDapper(option =>
{
    option.ConnectionConfigs = list;
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
"ConnectionStrings": {
    "1": {
      "name": "A",
      "connectionString": "Data Source=...;User ID=A;Password=A",
      "providerName": "SqlServer"
    },
    "2": {
      "name": "B",
      "connectionString": "Data Source=...;User ID=B;Password=B",
      "providerName": "Mysql"
    },
    "3": {
      "name": "C",
      "connectionString": "Data Source=...;User ID=C;Password=C",
      "providerName": "Oracle"
    },
    "4": {
      "name": "D",
      "connectionString": "Data Source=...;User ID=D;Password=D",
      "providerName": "Oracle"
    },
    "5": {
      "name": "E",
      "connectionString": "Data Source=...;User ID=E;Password=E",
      "providerName": "SqlServer"
    }
  }
```

var builder = WebApplication.CreateBuilder(args);

var httpReportsBuilder = builder.Services.AddHttpReportsDashboard();
var storageDBType = builder.Configuration.GetValue<StorageDBType>("HttpReportsDashboard:Storage:DbType");
switch (storageDBType)
{
    case StorageDBType.SQLServer:
        httpReportsBuilder.AddSQLServerStorage();
        break;
    case StorageDBType.MySQL:
        httpReportsBuilder.AddMySqlStorage();
        break;
    case StorageDBType.PostgreSQL:
        httpReportsBuilder.AddPostgreSQLStorage();
        break;
    case StorageDBType.Sqlite:
    default:
        System.IO.Directory.CreateDirectory("./App_Data");
        httpReportsBuilder.AddSqliteStorage();
        break;
}

var app = builder.Build();

app.UseHttpReportsDashboard();

app.Run();


// ƒ¨»œ’À∫≈ admin  123456

using HttpReportsDashboardServer;
using LogDashboard;
using LogDashboard.Extensions;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("App_Data/appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"App_Data/appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    ;

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

builder.Services.AddHttpClient();
builder.Services.AddHealthChecks()
//.AddSqlServer(builder.Configuration["ConnectionStrings:connectionString"]);
;

builder.Services.AddLogDashboard(opt =>
{
    var username = builder.Configuration.GetValue<string>("LogDashboard:UserName");
    var password = builder.Configuration.GetValue<string>("LogDashboard:PassWord") ?? "";
    var pathMatch = builder.Configuration.GetValue<string>("LogDashboard:PathMatch");
    var brand = builder.Configuration.GetValue<string>("LogDashboard:Brand");

    if (!string.IsNullOrWhiteSpace(username))
        opt.AddAuthorizationFilter(new LogDashboardBasicAuthorizationFilter(username, password));
    if (!string.IsNullOrWhiteSpace(pathMatch)) opt.PathMatch = pathMatch;
    if (!string.IsNullOrWhiteSpace(brand)) opt.Brand = brand;

    opt.SetRootPath(System.IO.Path.Combine(AppContext.BaseDirectory + "App_Data", "logs"));
    opt.CustomLogModel<LogDashboard.Models.RequestTraceLogModel>();
});
builder.Host.UseNLog();

var app = builder.Build();

app.UseHttpReportsDashboard();
app.UseLogDashboard();
app.UseHealthChecks("/health");

app.Run();


// ƒ¨»œ’À∫≈ admin  123456

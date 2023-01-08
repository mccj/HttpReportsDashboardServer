using HttpReports;
using HttpReports.Storage.Abstractions;
using HttpReports.Storage.Sqlite;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceBuildExtensions
    {
        public static IHttpReportsBuilder AddSqliteStorage(this IHttpReportsBuilder builder)
        {
            builder.Services.AddOptions();
            builder.Services.Configure<SqliteStorageOptions>(builder.Configuration.GetSection("Storage"));

            return builder.AddSqliteStorageService();
        }

        public static IHttpReportsBuilder AddSqliteStorage(this IHttpReportsBuilder builder, Action<SqliteStorageOptions> options)
        {
            builder.Services.AddOptions();
            builder.Services.Configure<SqliteStorageOptions>(options);

            return builder.AddSqliteStorageService();
        }

        private static IHttpReportsBuilder AddSqliteStorageService(this IHttpReportsBuilder builder)
        {
            builder.Services.AddSingleton<IHttpReportsStorage, SqliteStorage>();

            return builder;
        }


        //[Obsolete("Use AddSqliteStorage instead")]
        //public static IHttpReportsBuilder UseSqliteStorage(this IHttpReportsBuilder builder)
        //{
        //    builder.Services.AddOptions();
        //    builder.Services.Configure<SqliteStorageOptions>(builder.Configuration.GetSection("Storage"));

        //    return builder.AddSqliteStorageService();
        //}


        //[Obsolete("Use AddSqliteStorage instead")]
        //public static IHttpReportsBuilder UseSqliteStorage(this IHttpReportsBuilder builder, Action<SqliteStorageOptions> options)
        //{
        //    builder.Services.AddOptions();
        //    builder.Services.Configure<SqliteStorageOptions>(options);

        //    return builder.AddSqliteStorageService();
        //}
    }
}
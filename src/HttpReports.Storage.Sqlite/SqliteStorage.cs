using HttpReports.Storage.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HttpReports.Storage.Sqlite
{
    public class SqliteStorage : BaseStorage
    {
        new public SqliteStorageOptions _options;

        public ILogger<SqliteStorage> Logger { get; }

        private string Prefix { get; set; } = string.Empty;

        public SqliteStorage(IOptions<SqliteStorageOptions> options, ILogger<SqliteStorage> logger)

            : base(new BaseStorageOptions
            {
                DeferSecond = options.Value.DeferSecond,
                DeferThreshold = options.Value.DeferThreshold,
                ConnectionString = options.Value.ConnectionString,
                DataType = FreeSql.DataType.Sqlite
            })

        {
            _options = options.Value;

            Logger = logger;

        }
    }
}
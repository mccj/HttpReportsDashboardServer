using Microsoft.Extensions.Options;

namespace HttpReports.Storage.Sqlite
{
    public class SqliteStorageOptions : IOptions<SqliteStorageOptions>
    {
        public string ConnectionString { get; set; }


        public bool EnableDefer { get; set; }


        public int DeferSecond { get; set; }


        public int DeferThreshold { get; set; }

        public SqliteStorageOptions Value => this;
    }
}
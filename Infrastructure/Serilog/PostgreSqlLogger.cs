using Infrastructure.Tokens;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NpgsqlTypes;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Infrastructure.Serilog.ClientIpColumnWriter;

namespace Infrastructure.Serilog;
public class PostgreSqlLogger : LoggerServiceBase
{
    private readonly PostgreLogSettings _settings;
    public PostgreSqlLogger(IOptions<PostgreLogSettings> options)
    {
        _settings = options.Value;

        var columnWriters = new Dictionary<string, ColumnWriterBase>
        {
            { "message", new RenderedMessageColumnWriter(NpgsqlDbType.Text) },
            { "method_name", new MethodNameColumnWriter() },
            { "message_template", new MessageTemplateColumnWriter(NpgsqlDbType.Text) },
            { "level", new LevelColumnWriter(true, NpgsqlTypes.NpgsqlDbType.Varchar) },
            { "time_stamp", new TimestampColumnWriter(NpgsqlTypes.NpgsqlDbType.Timestamp) },
            { "exception", new ExceptionColumnWriter() },
            { "log_event", new LogEventSerializedColumnWriter(NpgsqlDbType.Json) },
            { "user_name", new UsernameColumnWriter() },
            { "ip_adress", new ClientIpColumnWriter() }          
        };

        ColumnOptions columnOptions = new();

        Logger seriLogConfig = new LoggerConfiguration().WriteTo
            .PostgreSQL(
                connectionString: _settings.ConnectionString,
                tableName: _settings.TableName,
                columnOptions: columnWriters,
                needAutoCreateTable: _settings.AutoCreateSqlTable)
            .Enrich.FromLogContext()
            .CreateLogger();

        Logger = seriLogConfig;
    }
}
public class UsernameColumnWriter : ColumnWriterBase
{
    public UsernameColumnWriter() : base(NpgsqlDbType.Varchar)
    {
    }

    public override object GetValue(LogEvent logEvent, IFormatProvider formatProvider = null)
    {
        logEvent.Properties.TryGetValue("user_name",out var value);
        return value?.ToString() ?? null;
    }
}
public class ClientIpColumnWriter : ColumnWriterBase
{
    public ClientIpColumnWriter() : base(NpgsqlDbType.Varchar)
    {
    }

    public override object GetValue(LogEvent logEvent, IFormatProvider formatProvider = null)
    {
        logEvent.Properties.TryGetValue("ip_adress", out var value);
        return value?.ToString() ?? null;
    }
    public class MethodNameColumnWriter : ColumnWriterBase
    {
        public MethodNameColumnWriter() : base(NpgsqlDbType.Varchar) { }

        public override object GetValue(LogEvent logEvent, IFormatProvider formatProvider = null)
        {
            logEvent.Properties.TryGetValue("method_name", out var value);
            return value?.ToString() ?? null;
        }
    }
}

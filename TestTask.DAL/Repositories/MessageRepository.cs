using TestTask.DAL.Entities;
using TestTask.DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Microsoft.Extensions.Logging;

namespace TestTask.DAL.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly NpgsqlDataSourceBuilder _dataSourceBuilder;
    private readonly string? _connectionString;

    public MessageRepository(IConfiguration configuration, ILoggerFactory loggerFactory)
    {
        _connectionString = configuration.GetValue<string>("DB_CONNECTION");
        _dataSourceBuilder = new NpgsqlDataSourceBuilder(_connectionString);
        _dataSourceBuilder.UseLoggerFactory(loggerFactory);
    }

    public async Task<MessageEntity> CreateMessage(MessageEntity messageEntity, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(_dataSourceBuilder);

        await EnsureCreated();

        using var dataSource = _dataSourceBuilder.Build();

        await using var command = dataSource.CreateCommand(
            "INSERT INTO messages (Text, CreatedAt, Number) VALUES (@text, @date, @number);");
        command.Parameters.AddWithValue("@text", messageEntity.Text);
        command.Parameters.AddWithValue("@date", messageEntity.CreatedAt);
        command.Parameters.AddWithValue("@number", messageEntity.Number);
        await command.ExecuteNonQueryAsync(ct);
        return messageEntity;
    }

    public async Task<IReadOnlyCollection<MessageEntity>> GetMessages(DateTime start, DateTime end, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(_dataSourceBuilder);

        using var dataSource = _dataSourceBuilder.Build();

        await using var command = dataSource.CreateCommand(
            "SELECT Text, CreatedAt, Number FROM messages WHERE CreatedAt > @start AND CreatedAt < @end;");
        command.Parameters.AddWithValue("@start", start);
        command.Parameters.AddWithValue("@end", end);

        await using var reader = await command.ExecuteReaderAsync();

        var messages = new List<MessageEntity>();

        if(reader.HasRows){
            while (await reader.ReadAsync(ct))
            {
                messages.Add(new MessageEntity()
                {
                    Text = (string)reader[0],
                    CreatedAt = (DateTime)reader[1],
                    Number = Decimal.ToInt32((decimal)reader[2])
                });
            }
        }

        return messages;
    }

    private async Task EnsureCreated()
    {
        bool dbExists;

        using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
        {
            await conn.OpenAsync();
            string cmdText = "SELECT 1 FROM messages";
            using (NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn))
            {
                dbExists = await cmd.ExecuteScalarAsync() != null;
            }

            if (!dbExists)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS public.messages " +
                    "(text text COLLATE pg_catalog.\"default\" NOT NULL, " +
                    "createdat timestamp without time zone NOT NULL, " +
                    "\"number\" numeric NOT NULL )", conn))
                {
                    await cmd.ExecuteScalarAsync();
                }
            }
        }
    }
}

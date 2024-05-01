using Microsoft.Data.SqlClient;

namespace AsyncAndTransactions.Repositories.SqlClient;

internal abstract class BaseRepository
{
    protected abstract string TableName { get; }
    
    protected async Task<bool> ExistsAsync(SqlCommand command, string whereSql, Action<SqlParameterCollection> onParametrizationAction, CancellationToken cancellationToken)
    {
        var query = $"SELECT COUNT(1) FROM {TableName} WHERE {whereSql}";
        var result = await ExecuteScalarAsync(command, query, onParametrizationAction, cancellationToken);
        return (int)result! > 0;
    }
    
    protected async Task<T?> FirstOrDefaultAsync<T>(SqlCommand command, string whereSql, Action<SqlParameterCollection> onParametrizationAction, Func<SqlReadRowModel, T> onReadAction, 
        T? initialValue = default, CancellationToken cancellationToken = default)
    {
        var result = initialValue;

        await ExecuteReaderAsync(command,$"SELECT TOP 1 * FROM {TableName} WHERE {whereSql}", onParametrizationAction, row =>
        {
            result = onReadAction(row);
        }, cancellationToken);

        return result;
    }
    
    protected async Task<int> ExecuteNonQueryAsync(SqlCommand command, string query, Action<SqlParameterCollection> onParametrizationAction, CancellationToken cancellationToken)
    {
        command.CommandText = query;
        command.Parameters.Clear();
        onParametrizationAction(command.Parameters);

        return await command.ExecuteNonQueryAsync(cancellationToken);
    }
    
    public static async Task<object> ExecuteScalarAsync(SqlCommand command, string query, Action<SqlParameterCollection> onParametrizationAction, CancellationToken cancellationToken)
    {
        command.CommandText = query;
        command.Parameters.Clear();
        onParametrizationAction(command.Parameters);

        return await command.ExecuteScalarAsync(cancellationToken);
    }

    private async Task ExecuteReaderAsync(SqlCommand command, string query, Action<SqlParameterCollection> onParametrizationAction, Action<SqlReadRowModel> onReadAction, CancellationToken cancellationToken)
    {
        command.CommandText = query;
        command.Parameters.Clear();
        onParametrizationAction(command.Parameters);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        var ordinalsDict = RetrieveOrdinals(reader);
        
        while (await reader.ReadAsync(cancellationToken))
        {
            onReadAction(new(reader, ordinalsDict));
        }
    }

    private Dictionary<string, int> RetrieveOrdinals(SqlDataReader reader)
    {
        var ordinalsDict = new Dictionary<string, int>();
        for (var i = 0; i < reader.FieldCount; i++)
        {
            ordinalsDict.Add(reader.GetName(i).ToLower(), i);
        }

        return ordinalsDict;
    }   
}
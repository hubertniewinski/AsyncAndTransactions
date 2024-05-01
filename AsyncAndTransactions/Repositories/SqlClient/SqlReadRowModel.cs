using Microsoft.Data.SqlClient;

namespace AsyncAndTransactions.Repositories.SqlClient;

internal record SqlReadRowModel(SqlDataReader Reader, Dictionary<string, int> Columns);
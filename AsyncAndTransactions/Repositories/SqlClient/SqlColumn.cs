namespace AsyncAndTransactions.Repositories.SqlClient;

internal record SqlColumn<TDomainModel>(string Name, string Parameter, Func<TDomainModel, object?> GetDomainModelValue)
{
    public readonly string Name = Name.ToLower();
    public readonly string Parameter = Parameter.ToLower();
}
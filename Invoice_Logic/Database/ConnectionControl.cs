using Dapper;
using Invoice_Logic.API;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Invoice_Logic.Database;

public interface IConnectionControl
{
    Task<List<T>> LoadDataAsync<T>(string storedProc);
    Task<List<T>> LoadDataAsync<T, P>(string storedProc, P parameters);
    Task<int> SaveDataAsync<P>(string storedProc, P parameters);
}

public class ConnectionControl : IConnectionControl
{
    private string _connectionString;

    public ConnectionControl(string connectionString)
    {
        _connectionString = connectionString;
    }

    public ConnectionControl(ICustomSettings settings)
    {
        _connectionString = settings.ConnectionString;
    }

    public ConnectionControl(string serverName, string databaseName, string userName, string passwrod)
    {
        _connectionString = string.Format(@"Server={0};Database={1};User Id={2};Password={3};",
            serverName,
            databaseName,
            userName,
            passwrod);
    }

    public ConnectionControl(string serverName, string databaseName)
    {
        _connectionString = string.Format(@"Server={0};Database={1};Trusted_Connection=True;",
            serverName,
            databaseName);
    }

    public async Task<List<T>> LoadDataAsync<T, P>(string storedProc, P parameters)
    {
        using IDbConnection db = new SqlConnection(_connectionString);
        var rows = (await db.QueryAsync<T>(
            storedProc,
            parameters,
            commandType: CommandType.StoredProcedure)).ToList();
        return rows;
    }

    public async Task<List<T>> LoadDataAsync<T>(string storedProc)
    {
        using IDbConnection db = new SqlConnection(_connectionString);
        var rows = (await db.QueryAsync<T>(
            storedProc,
            commandType: CommandType.StoredProcedure)).ToList();
        return rows;
    }

    public async Task<int> SaveDataAsync<P>(string storedProc, P parameters)
    {
        using IDbConnection db = new SqlConnection(_connectionString);
        var result = await db.ExecuteAsync(storedProc, parameters, commandType: CommandType.StoredProcedure);
        return result;
    }
}

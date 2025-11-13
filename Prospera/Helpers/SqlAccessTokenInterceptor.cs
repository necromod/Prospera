using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Data.Common;

public class SqlAccessTokenInterceptor : DbConnectionInterceptor
{
    private readonly TokenAcquisitionService _tokenService;

    public SqlAccessTokenInterceptor(TokenAcquisitionService tokenService)
    {
        _tokenService = tokenService;
    }

    public override async Task ConnectionOpeningAsync(DbConnection connection, ConnectionEventData eventData, CancellationToken cancellationToken = default)
    {
        if (connection is SqlConnection sqlConnection)
        {
            var token = await _tokenService.GetAccessTokenAsync();
            sqlConnection.AccessToken = token;
        }

        await base.ConnectionOpeningAsync(connection, eventData, cancellationToken);
    }
}

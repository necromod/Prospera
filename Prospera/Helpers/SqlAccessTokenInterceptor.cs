using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Prospera.Helpers
{
    public class SqlAccessTokenInterceptor : DbConnectionInterceptor
    {
        private readonly TokenAcquisitionService _tokenService;

        public SqlAccessTokenInterceptor(TokenAcquisitionService tokenService)
        {
            _tokenService = tokenService;
        }

        public override InterceptionResult ConnectionOpening(DbConnection connection, ConnectionEventData eventData, InterceptionResult result)
        {
            if (connection is SqlConnection sqlConnection)
            {
                // Acquire token synchronously for the interception point
                var token = _tokenService.GetAccessTokenAsync().GetAwaiter().GetResult();
                sqlConnection.AccessToken = token;
            }

            return base.ConnectionOpening(connection, eventData, result);
        }
    }
}

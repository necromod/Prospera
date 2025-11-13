using Azure.Identity;
using Azure.Core;
using System.Threading.Tasks;

public class TokenAcquisitionService
{
    private readonly DefaultAzureCredential _credential;

    public TokenAcquisitionService()
    {
        _credential = new DefaultAzureCredential();
    }

    public async Task<string> GetAccessTokenAsync()
    {
        var tokenRequestContext = new TokenRequestContext(new[] { "https://database.windows.net/.default" });
        var token = await _credential.GetTokenAsync(tokenRequestContext);
        return token.Token;
    }
}

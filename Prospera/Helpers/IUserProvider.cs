using Prospera.Models;

namespace Prospera.Helpers
{
    public interface IUserProvider
    {
        Usuario? GetUserById(int id);
    }
}

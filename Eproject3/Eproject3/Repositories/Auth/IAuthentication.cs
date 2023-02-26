using Eproject3.Models;

namespace Eproject3.Repositories.Auth
{
    public interface IAuthentication
    {
        public Admin? GetUserFromToken(string token);
    }
}

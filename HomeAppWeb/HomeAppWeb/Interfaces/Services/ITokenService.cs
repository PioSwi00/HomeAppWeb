using HomeAppWeb.Models;
using System.Threading.Tasks;

namespace HomeAppWeb.Interface.Services
{
    public interface ITokenService
    {
        Task<string> GenerateToken(User user);
    }
}

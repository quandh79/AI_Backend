using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface ITokenRepository
    {
        Task DeactivateAsync(string token);
        Task DeactivateCurrentAsync();
        Task<bool> IsActiveAsync(string token);
        Task<bool> IsCurrentActiveToken();
    }
}
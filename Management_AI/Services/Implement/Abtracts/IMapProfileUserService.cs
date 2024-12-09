using Common.Commons;
using Management_AI.Models.Main;
using System.Threading.Tasks;

namespace Management_AI.Services.Implement.Abtracts
{
    public interface IMapProfileUserService
    {
        Task<ResponseService<bool>> Create(AddUserToProfile obj);
    }
}

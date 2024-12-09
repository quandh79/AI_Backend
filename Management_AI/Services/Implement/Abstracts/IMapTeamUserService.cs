using Common.Commons;
using Common.Params.Base;
using Management_AI.Models.Main;
using Repository.BCC01_EF;
using Repository.CustomModel;
using System.Threading.Tasks;

namespace Management_AI.Services.Implement.Abstracts
{
    public interface IMapTeamUserService
    {
        Task<ResponseService<ListResult<BCC01_MapTeamUser>>> GetAll(PagingParam param);
        Task<ResponseService<BCC01_MapTeamUser>> Add(BCC01_MapTeamUser obj);
        Task<ResponseService<bool>> Delete(string id);

    }
}

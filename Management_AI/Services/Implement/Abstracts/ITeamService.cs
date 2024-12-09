using Common.Commons;
using Common.Params.Base;
using Management_AI.Models.Main;
using Repository.BCC01_EF;
using Repository.CustomModel;
using Repository.EF;
using System;
using System.Threading.Tasks;

namespace Management_AI.Services.Implement.Abstracts
{
    public interface ITeamService
    {
        Task<ResponseService<ListResult<BCC01_Teams>>> GetAll(PagingParam param);
        Task<ResponseService<TeamModel>> Create(TeamModel obj);
        Task<ResponseService<TeamModel>> Update(TeamModel obj);
        Task<ResponseService<bool>> Delete(Guid id);
        Task<ResponseService<bool>> AddUserNameInTeam(AddMapTeamUserModel model);
        Task<ResponseService<bool>> DeleteUserNameInTeam(DeleteMapTeamUserModel model);
        Task<ResponseService<ListResult<string>>> GetAllUserNotInTeam(Guid teamId);
        Task<ResponseService<ListResult<UserInTeamModel>>> GetAllUserInTeam(Guid teamId);
    }
}

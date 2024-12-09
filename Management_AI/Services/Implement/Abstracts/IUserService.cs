using Common.Commons;
using Common.Params.Base;
using Repository.EF;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Management_AI.Common.ResponAPI3rd;
using Management_AI.Models;
using Management_AI.Services.Models;
using Repository.CustomModel;
using Repository.BCC01_EF;
using Management_AI.Models.Main;
using Management_AI.Models.Common;

namespace Management_AI.Services.Implement.Abstracts
{
    public interface IUserService
    {
        Task<ResponseService<ListResult<UserCustomResponse>>> GetAll(PagingParam param);
        Task<IEnumerable<BCC01_User>> GetAllUser();
        Task<ResponseService<UserCustomResponse>> Create(UserRequest obj, bool isLdap = false);
        Task<ResponseService<UserCustomResponse>> Update(UserUpdateRequest obj);
        Task<ResponseService<bool>> Delete(string id);
        Task<ResponseService<UserCustomResponse>> GetById(string username);
        Task<ResponseService<UserState>> GetUserState(UsernameRequest request);
        Task<ResponseService<ListResult<UserCustomResponse>>> GetListUserRole(UsernameRequest request);
        Task<BCC01_User> GetUserByExtesnion(string ext);
        Task<ResponseService<UserModel>> CheckExistByEmail(ItemModel<string> items);
    }
}
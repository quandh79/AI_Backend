using AutoMapper;
using Common.Commons;
using Common.Params.Base;
using Management_AI.Services.Implement.Abstracts;
using Repository.BCC01_EF;
using Repository.CustomModel;
using Repository.Repositories.Abtracts;
using ILogger = Common.Commons.ILogger;


namespace Management_AI.Services.Implement
{
    public class MapTeamUserService : BaseService, IMapTeamUserService
    {
        public IMapTeamUserRepository _mapTeamUserRepository { get; set; }
        public MapTeamUserService(IMapTeamUserRepository mapTeamUserRepository, ILogger logger, IMapper mapper) : base(logger, mapper)
        {
            _mapTeamUserRepository = mapTeamUserRepository;
        }

        public async Task<ResponseService<BCC01_MapTeamUser>> Add(BCC01_MapTeamUser obj)
        {
            var res = await _mapTeamUserRepository.Create(obj);
            if (res != null)
            {
                return new ResponseService<BCC01_MapTeamUser>(res);
            }
            return new ResponseService<BCC01_MapTeamUser>(false, "Add Error", 0);
        }

        public async Task<ResponseService<bool>> Delete(string id)
        {
            var res = await _mapTeamUserRepository.Delete(id);
            return new ResponseService<bool>(res);

        }

        public async Task<ResponseService<ListResult<BCC01_MapTeamUser>>> GetAll(PagingParam param)
        {
            var res = await _mapTeamUserRepository.GetAll(param);
            return new ResponseService<ListResult<BCC01_MapTeamUser>>(res);
        }
    }
}

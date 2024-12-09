
using AutoMapper;
using Common;
using Common.Commons;
using Common.Params.Base;
using Management_AI.Common;
using Management_AI.Models.Main;
using Management_AI.Services.Implement.Abstracts;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repository.BCC01_EF;
using Repository.CustomModel;
using Repository.Repositories.Abtracts;
using ILogger = Common.Commons.ILogger;


namespace Management_AI.Services.Implement
{
    public class TeamService : BaseService, ITeamService
    {
        public ITeamRepository _teamRepository { get; set; }
        public IMapTeamUserRepository _mapTeamUserRepository { get; set; }
        public TeamService(ITeamRepository teamRepository,
            IMapTeamUserRepository mapTeamUserRepository, ILogger logger, IMapper mapper) : base(logger, mapper)
        {
            _mapTeamUserRepository = mapTeamUserRepository;
            _teamRepository = teamRepository;
        }
        public async Task<ResponseService<TeamModel>> Create(TeamModel obj)
        {
            obj.AddInfo();
            _logger.LogError($"{nameof(Create)} obj: {JsonConvert.SerializeObject(obj)}");

            var bcc01_teams = _mapper.Map<TeamModel, BCC01_Teams>(obj);
            var data = await _teamRepository.Create(bcc01_teams);
            if (data != null)
            {
                var team = _mapper.Map<BCC01_Teams, TeamModel>(data);
                return new ResponseService<TeamModel>(team);
            }
            return new ResponseService<TeamModel>("Error!!");
        }

        public async Task<ResponseService<bool>> Delete(Guid id)
        {
            // delete Tenant_id
            var res = await _teamRepository.Delete(id);
            // delete All username in MapTeamUser
            using (var _db = new BCC01_DbContextSql())
            {
                var lstMapTeamUser = _db.BCC01_MapTeamUser.Where(x => x.team_id == id);
                if (lstMapTeamUser != null && lstMapTeamUser.Any())
                {
                    _db.BCC01_MapTeamUser.RemoveRange(lstMapTeamUser);
                    await _db.SaveChangesAsync();
                }

            }

            return new ResponseService<bool>(res);
        }

        public async Task<ResponseService<ListResult<BCC01_Teams>>> GetAll(PagingParam param)
        {

            param.tenant_id = Guid.Parse(SessionStore.Get<string>(Constants.KEY_SESSION_TENANT_ID));
            _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

            var res = await _teamRepository.GetAll(param);
            return new ResponseService<ListResult<BCC01_Teams>>(res);
        }

        public async Task<ResponseService<TeamModel>> Update(TeamModel obj)
        {
            obj.UpdateInfo();
            var bcc01_teams = _mapper.Map<TeamModel, BCC01_Teams>(obj);
            var data = await _teamRepository.Update(bcc01_teams, obj.id);
            if (data != null)
            {
                var team = _mapper.Map<BCC01_Teams, TeamModel>(data);
                return new ResponseService<TeamModel>(team);
            }
            return new ResponseService<TeamModel>("Error!!");
        }


        public async Task<ResponseService<bool>> AddUserNameInTeam(AddMapTeamUserModel model)
        {
            try
            {
                //var tenant_id = Guid.Parse(SessionStore.Get<string>(Constants.KEY_SESSION_TENANT_ID));
                model.tenantId = model.tenantId ?? Guid.Parse(SessionStore.Get<string>(Constants.KEY_SESSION_TENANT_ID));

                using (var db = new BCC01_DbContextSql())
                {
                    var datetime = DateTime.Now;
                    var lstmapTeamUser = new List<BCC01_MapTeamUser>();
                    foreach (var item in model.usernames)
                    {
                        var bcc01_mapteamuser = new BCC01_MapTeamUser()
                        {
                            create_by = SessionStore.Get<string>(Constants.KEY_SESSION_USER_ID),
                            create_time = datetime,
                            id = Guid.NewGuid(),
                            team_id = model.teamId,
                            tenant_id = model.tenantId.Value,
                            update_by = string.Empty,
                            update_time = datetime,
                            username = item

                        };
                        lstmapTeamUser.Add(bcc01_mapteamuser);
                    }
                    await db.BCC01_MapTeamUser.AddRangeAsync(lstmapTeamUser);
                    await db.SaveChangesAsync();
                }
                return new ResponseService<bool>(true);
            }
            catch (Exception ex)
            {
                return new ResponseService<bool>(false);
            }
        }

        public async Task<ResponseService<bool>> DeleteUserNameInTeam(DeleteMapTeamUserModel model)
        {
            //var tenant_id = Guid.Parse(SessionStore.Get<string>(Constants.KEY_SESSION_TENANT_ID));
            model.tenantId = model.tenantId ?? Guid.Parse(SessionStore.Get<string>(Constants.KEY_SESSION_TENANT_ID));
            using (var db = new BCC01_DbContextSql())
            {
                var mapTeamUser = await db.BCC01_MapTeamUser.FirstOrDefaultAsync(x => x.tenant_id == model.tenantId
                                                                        && x.username == model.username
                                                                        && x.team_id == model.teamId);
                var res = db.BCC01_MapTeamUser.Remove(mapTeamUser);
                await db.SaveChangesAsync();
                return new ResponseService<bool>(true);
            }
        }

        public async Task<ResponseService<ListResult<string>>> GetAllUserNotInTeam(Guid teamId)
        {
            var tenant_id = Guid.Parse(SessionStore.Get<string>(Constants.KEY_SESSION_TENANT_ID));

            using (var db = new BCC01_DbContextSql())
            {
                var lstUser = await db.BCC01_User.Where(x => x.tenant_id == tenant_id && x.is_active).Select(x => x.username).ToListAsync();
                var lstMapTeamUser = await db.BCC01_MapTeamUser.Where(ptr => ptr.tenant_id == tenant_id
                                                                            && ptr.team_id == teamId)
                                                            .Select(x => x.username).ToListAsync();
                if (lstMapTeamUser == null || lstMapTeamUser.Count() <= 0)
                {
                    return new ResponseService<ListResult<string>>(new ListResult<string>(lstUser, lstUser.Count()));

                }
                //join All User not in MapTeamUser

                var data = (from u in lstUser
                            join m in lstMapTeamUser on u equals m into m
                            from j in m.DefaultIfEmpty()
                            where j == null
                            select u).ToList();

                return new ResponseService<ListResult<string>>(new ListResult<string>(data.ToList(), data.Count()));
            }
        }

        public async Task<ResponseService<ListResult<UserInTeamModel>>> GetAllUserInTeam(Guid teamId)
        {
            var tenant_id = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);

            using (var db = new BCC01_DbContextSql())
            {
                var lstMapTeamUser = await db.BCC01_MapTeamUser.Where(ptr => ptr.tenant_id == tenant_id && ptr.team_id == teamId).ToListAsync();
                List<UserInTeamModel> lstUserInTeam = new List<UserInTeamModel>();
                foreach (var data in lstMapTeamUser)
                {
                    var user = await db.BCC01_User.FirstOrDefaultAsync(x => x.tenant_id == tenant_id && x.username == data.username);

                    var res = new UserInTeamModel
                    {
                        tenant_id = tenant_id,
                        username = data.username,
                        email = user.email,
                        fullname = user.fullname,
                        id = data.id,
                        role_id = user.role_id,
                        teamid = data.team_id
                    };
                    lstUserInTeam.Add(res);
                }
                return new ResponseService<ListResult<UserInTeamModel>>(new ListResult<UserInTeamModel>(lstUserInTeam, lstUserInTeam.Count()));

            }
        }
    }

}

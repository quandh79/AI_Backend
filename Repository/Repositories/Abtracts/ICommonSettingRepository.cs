using Repository.BCC01_EF;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface ICommonSettingRepository : IBaseRepositorySql<BCC01_CommonSetting>
    {
        Task<List<BCC01_CommonSetting>> GetAllCommonSetting();
    }
}

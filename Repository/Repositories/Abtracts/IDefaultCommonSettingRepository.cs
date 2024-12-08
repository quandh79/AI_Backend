using Repository.BCC01_EF;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface IDefaultCommonSettingRepository : IBaseRepositorySql<BCC01_DefaultCommonSetting>
    {
        Task<BCC01_DefaultCommonSetting> CreateDefaultCommonSettingWithSyncData(BCC01_DefaultCommonSetting setting);
        Task<BCC01_DefaultCommonSetting> UpdateDefaultCommonSettingWithSyncData(BCC01_DefaultCommonSetting setting);
    }
}

using Microsoft.EntityFrameworkCore;
using Repository.BCC01_EF;
using Repository.EF;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CommonSettingRepository : BaseRepositorySql<BCC01_CommonSetting>, ICommonSettingRepository
    {
        public CommonSettingRepository() : base() { }


        public async Task<List<BCC01_CommonSetting>> GetAllCommonSetting()
        {
            return await _db.BCC01_CommonSetting.ToListAsync();
        }
    }
}

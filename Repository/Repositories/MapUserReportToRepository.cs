using Microsoft.EntityFrameworkCore;
using Repository.BCC01_EF;
using Repository.Repositories.Abtracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class MapUserReportToRepository : BaseRepositorySql<BCC01_MapUserReportTo>, IMapUserReportToRepository
    {
        public async Task<bool> RemoveAll(string username)
        {
            var lstUserReport = await _db.BCC01_MapUserReportTo.Where(x => x.username == username).ToListAsync();
            if(lstUserReport != null && lstUserReport.Any())
            {
                await RemoveRange(lstUserReport);
                return true;
            }
            return false;
        }
    }
}

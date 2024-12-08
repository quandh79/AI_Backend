using Repository.BCC01_EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Abtracts
{
    public interface IMapUserReportToRepository : IBaseRepositorySql<BCC01_MapUserReportTo>
    {
        public Task<bool> RemoveAll(string username);
    }
}

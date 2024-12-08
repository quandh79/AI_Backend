using Microsoft.EntityFrameworkCore;
using Repository.BCC01_EF;
using Repository.Repositories.Abtracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class MapAgentGroupRepository : IMapAgentGroupRepository
    {
       public async Task<List<BCC01_MapAgentGroup>> GetAllAgentGroupIdByUserName(Guid tenant_id, string username)
        {
            using (var _dbContextSql = new BCC01_DbContextSql())
            {
                return await _dbContextSql.BCC01_MapAgentGroup.Where(x => x.tenant_id.Equals(tenant_id) && x.username.Equals(username)).ToListAsync();
            }
        }
    }
}

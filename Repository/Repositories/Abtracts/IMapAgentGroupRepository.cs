using Common.Params.Base;
using Repository.BCC01_EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Abtracts
{
    public interface IMapAgentGroupRepository
    {
        public Task<List<BCC01_MapAgentGroup>> GetAllAgentGroupIdByUserName(Guid tenant_id, string username);

    }
}

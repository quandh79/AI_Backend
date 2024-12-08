using Common.Params.Base;
using Repository.BCC03_EF;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface ICallLogRepository : IBaseRepositoryBCC03<BCC03_CallLog>
    {
        BCC03_CallLog GetCallTalkingByExtension(string extension);
        Task<BCC03_CallLog> GetCallIdNotOverYet(Guid tenant_id, string extension_number);
      //  Task<CallLogResponse> GetQueueNameByUniqueId(string unique_id);
    }
}
using Common;
using Common.Commons;
using log4net;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509;
using Repository.BCC01_EF;
using Repository.BCC03_EF;
using Repository.EF;
using Repository.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CallLogRepository : BaseRepositoryBCC03<BCC03_CallLog>, ICallLogRepository
    {
        //private static ILogger _logger = ConfigContainerDJ.CreateInstance<ILogger>();

        public CallLogRepository()
        {
        }
        public BCC03_CallLog GetCallTalkingByExtension(string extension)
        {
            using (var _dbContextSql = new BCC03_DbContextSql())
            {
                return _dbContextSql.Set<BCC03_CallLog>().Where(x => x.extension_number == extension
                        && (x.call_status == Constants.ANSWER || x.call_status == Constants.HOLD || x.call_status == Constants.UNHOLD)
                        && (x.call_direct == Constants.INBOUND || x.call_direct == Constants.OUTBOUND)).OrderByDescending(x => x.create_time).FirstOrDefault();
            }
        }

        public async Task<BCC03_CallLog> GetCallIdNotOverYet(Guid tenant_id, string extension_number)
        {
            using (var _dbContextSql = new BCC03_DbContextSql())
            {
                return await _dbContextSql.Set<BCC03_CallLog>().FirstOrDefaultAsync(x => x.tenant_id == tenant_id && x.extension_number == extension_number && x.end_time == null);
            }

        }

        //public async Task<CallLogResponse> GetQueueNameByUniqueId(string unique_id)
        //{
        //    using(var _db = new DbContextSql())
        //    {
        //        var callLog = await _db.Set<BCC03_CallLog>().FirstOrDefaultAsync(x => x.is_cucm_waiting_call == true && x.phone_number == unique_id);
        //        if(callLog != null && callLog.queue_id != null)
        //        {
        //            var queue = await _db.Set<BCC02_Queue>().FirstOrDefaultAsync(x => x.id == callLog.queue_id);
        //            if(queue != null)
        //            {
        //                return new CallLogResponse
        //                {
        //                    hotline = callLog.hotline_number,
        //                    queue_name = queue.queue_name,
        //                };
        //            }
        //        }
        //        return new CallLogResponse();
        //    }
        //}

        //public async Task<string> GetQueueNameByUniqueId(string unique_id)
        //{

        //    using (var _dbContextSql = new DbContextSql())
        //    {
        //        var callLog = await _dbContextSql.Set<BCC02_CallLog>().FirstOrDefaultAsync(x =>  x.is_cucm_waiting_call == true && x.phone_number == unique_id);// x.is_cucm_waiting_call == true && x.phone_number == unique_id
        //        if (callLog != null && callLog.queue_id != null)
        //        { 
        //            var queue = await _dbContextSql.Set<BCC02_Queue>().FirstOrDefaultAsync(x => x.id == callLog.queue_id);
        //            if (queue != null)
        //            {
        //                return queue.queue_name;
        //            }
        //        }
        //        return "";

        //    }
        //}

    }
}

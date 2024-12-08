using Common.Params.Base;
using Repository.BCC03_EF;
using Repository.EF;
using Repository.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface IRecordingRepository : IBaseRepositoryBCC03<BCC03_RecordingFile>
    {
    }
}
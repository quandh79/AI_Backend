using Microsoft.EntityFrameworkCore;
using Repository.BCC03_EF;
using Repository.EF;
using Repository.Model;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class RecordingRepository : BaseRepositoryBCC03<BCC03_RecordingFile>, IRecordingRepository
    {
        public RecordingRepository() 
        {
        }
    }
}

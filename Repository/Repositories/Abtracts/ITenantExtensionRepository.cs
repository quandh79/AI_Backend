using Repository.BCC01_EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Abtracts
{
    public interface ITenantExtensionRepository : IBaseRepositorySql<BCC01_TenantExtension>
    {
        public BCC01_TenantExtension GetTenantExtension(Guid tenantid);
        public int? GetMaxPrefixExtension();
        public bool ExtensionIsValid(string extension);
       // public bool UpdateTenantExtension(BCC01_TenantExtension); chưa cần thiết
    }
}


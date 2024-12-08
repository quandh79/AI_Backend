using Repository.BCC01_EF;
using Repository.Repositories.Abtracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class TenantExtensionRespository : BaseRepositorySql<BCC01_TenantExtension>, ITenantExtensionRepository
    {
        public TenantExtensionRespository() : base()
        {
        }

        public bool ExtensionIsValid(string extension)
        {
            using (var db = new BCC01_DbContextSql())
            {
                var result = db.BCC01_User.Where(x => x.extension_number == extension).FirstOrDefault();
                if (result != null)
                {
                    return true;
                }    
            }
            //throw new NotImplementedException();
            // check trùng trong db chưa => xong
            // check đúng prefix hay chưa
            // check tồn tại tên cucm chưa
            // create phone trên cucm
            // update vào db, (table user, tenantextension)
            //
            return false;
        }

        public int? GetMaxPrefixExtension()
        {
            using (var db = new BCC01_DbContextSql())
            {
                if (db.BCC01_TenantExtension.Count() > 0)
                {
                    var result = db.BCC01_TenantExtension.ToList().Max(x => x.prefix_extension);
                    /*  nếu prefix lớn nhất = 0, thì table ko có data => mặc định prefix_extension bắt đầu = 10
                     */
                    if (result == null || result == 0)
                    {
                        return 100;
                    }
                    return result;// == null? return;
                }
                return 100;
            }
        }

        public BCC01_TenantExtension GetTenantExtension(Guid tenantid)
        {
            using (var db = new BCC01_DbContextSql())
            {
                try
                {
                    //var x = db.BCC01_TenantExtension.ToList();
                    var result =  db.BCC01_TenantExtension.Where(x => x.tenant_id.CompareTo(tenantid) == 0).FirstOrDefault();
                    return result;

                }catch(Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
                
            }
        }
    }
}

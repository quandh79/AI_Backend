using Dapper;
using Repository.BCC01_EF;
using Repository.Utility;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class DefaultCommonSettingRepository : BaseRepositorySql<BCC01_DefaultCommonSetting>, IDefaultCommonSettingRepository
    {
        private readonly IStoreProcedureExcute _storeProcedureExcute;

        public DefaultCommonSettingRepository(
            IStoreProcedureExcute storeProcedureExcute
            ) : base() 
        {
            _storeProcedureExcute = storeProcedureExcute;
        }

        public virtual async Task<BCC01_DefaultCommonSetting> CreateDefaultCommonSettingWithSyncData(BCC01_DefaultCommonSetting setting)
        {
            var parameters = new DynamicParameters(
                new
                {
                    id = setting.id,
                    setting_key = setting.setting_key,
                    value = setting.value,
                    description = setting.description,
                    common_type = setting.common_type,
                    setting_for = setting.setting_for,
                    only_root = setting.only_root,
                    create_by = setting.create_by,
                    tenant_root_id = setting.tenant_id
                });

            _storeProcedureExcute.ExecuteNotReturn("usp_DefaultCommonSetting_Create_And_Sync_Data", parameters,1);

            return setting;
        }

        public virtual async Task<BCC01_DefaultCommonSetting> UpdateDefaultCommonSettingWithSyncData(BCC01_DefaultCommonSetting setting)
        {
            var parameters = new DynamicParameters(
                new
                {
                    id = setting.id,
                    setting_key = setting.setting_key,
                    value = setting.value,
                    description = setting.description,
                    common_type = setting.common_type,
                    setting_for = setting.setting_for,
                    only_root = setting.only_root,
                    create_time = setting.create_time,
                    create_by = setting.create_by,
                    modify_by = setting.modify_by,
                    tenant_root_id = setting.tenant_id
                });

            _storeProcedureExcute.ExecuteNotReturn("usp_DefaultCommonSetting_Update_And_Sync_Data", parameters,1);

            return setting;
        }
    }
}

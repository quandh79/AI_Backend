using Common;
using Management_AI.Common;
using System;

namespace Management_AI.Models.Main
{
    public class BaseModelSQL
    {
        public Guid id { get; set; }
        public Guid tenant_id { get; set; }
        public string create_by { get; set; }
        public string modify_by { get; set; }
        public DateTime create_time { get; set; }
        public DateTime modify_time { get; set; }

        public void AddInfo()
        {
            DateTime currentDateTime = DateTime.Now;
            id = Guid.NewGuid();
            tenant_id = tenant_id == Guid.Empty ? SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID) : tenant_id;
            create_by = string.IsNullOrEmpty(create_by) ? SessionStore.Get<string>(Constants.KEY_SESSION_USER_ID) : create_by;
            modify_by = "";
            create_time = currentDateTime;
            modify_time = currentDateTime;
        }
        public void AddInfoLdap()
        {
            DateTime currentDateTime = DateTime.Now;
            id = Guid.NewGuid();
            create_by = string.IsNullOrEmpty(create_by) ? SessionStore.Get<string>(Constants.KEY_SESSION_USER_ID) : create_by;
            modify_by = "";
            create_time = currentDateTime;
            modify_time = currentDateTime;
        }
        public void UpdateInfo()
        {
            DateTime currentDateTime = DateTime.Now;
            modify_by = string.IsNullOrEmpty(modify_by) ? SessionStore.Get<string>(Constants.KEY_SESSION_USER_ID) : modify_by;
            modify_time = currentDateTime;
            tenant_id = tenant_id == Guid.Empty ? SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID) : tenant_id;
        }
    }
}

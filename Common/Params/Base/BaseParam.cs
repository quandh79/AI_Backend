using System;

namespace Common.Params.Base
{
    public class BaseParam
    {
        public Guid tenant_id { get; set; }
        public bool flag { get; set; } = true;
    }

    public class TopicParam
    {
        public Object data { get; set; }
        public string topic { get; set; }
    }
}

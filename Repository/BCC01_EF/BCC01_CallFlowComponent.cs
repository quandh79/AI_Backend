using System;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Repository.BCC01_EF
{
    public partial class BCC01_CallFlowComponent
    {
        public int id { get; set; }
        public Guid callflow_id { get; set; }
        public string component_type { get; set; }
        public string component_name { get; set; }
        public string description { get; set; }
        public string agent_extension { get; set; }
        public Guid recording_id { get; set; }
        public Guid st_hotline_id { get; set; }
        public int ivr_timeout { get; set; }
        public int ivr_retry_times { get; set; }
        public Guid tcd_timegroup_id { get; set; }
        public int queue_id { get; set; }
        public string queue_music_url { get; set; }
        public int queue_timeout { get; set; }
        public double location_x { get; set; }
        public double location_y { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; }
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid tenant_id { get; set; }
    }
}

using Common.Params.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Management_AI.Models.Main
{
    public class ParamRecodingFileCallog
    {
        public string call_id { get; set; }
        public string extension_number { get; set; }
        public string phone_number { get; set; }
        public string call_from { get; set; }
        public string call_to { get; set; }
        public DateTime? start_time { get; set; }
        public DateTime? end_time { get; set; }

        public string filename { get; set; }
        public string full_filename { get; set; }
        public string file_format { get; set; }
        public string errormessage { get; set; }
        public int? duration { get; set; }
        public string lst_path { get; set; }

        public IFormFile file { get; set; }

    }

    public class ParamUploadFileCallog
    {
        public Guid? id { get; set; }
        public string tenant_id { get; set; }
        public IFormFile file { get; set; }
        public string filename { get; set; }
        public string create_by { get; set; }

    }

    public class ResponMetaDataFileManagement_AI
    {
        public Guid? record_id { get; set; }
        public List<string> lst_path { get; set; }
    }
}

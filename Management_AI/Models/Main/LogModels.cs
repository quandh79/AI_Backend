using System;
using System.Reflection;

namespace Management_AI.Models.Main
{
    public class LogModels
    {
    }

    public class LogSystemErrorModel
    {
        public string mess { get; set; }
        public string service { get; set; }
        public DateTime create_time { get; set; }
        public Exception ex { get; set; }
        public LogSystemErrorModel(Exception ex)
        {
            mess = ex.Message;
            service = Assembly.GetCallingAssembly().GetName().Name;
            create_time = DateTime.Now;
            this.ex = ex;
        }
        public LogSystemErrorModel(string mess)
        {
            this.mess = mess;
            service = Assembly.GetCallingAssembly().GetName().Name;
            create_time = DateTime.Now;
        }
    }
}

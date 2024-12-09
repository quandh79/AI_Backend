using Common.Params.Base;
using System;
using System.Runtime.CompilerServices;

namespace Management_AI.Models.Main
{
    public class ListeningClientModel
    {
        public string userName { get; set; }
        public string callFrom { get; set; }
        public string callTo { get; set; }
        public string callId { get; set; }
        public string callIdTo { get; set; }
        public string transferTo { get; set; }
        public string callType { get; set; }
        public string extensionSup { get; set; }
        public string extensionEndCall { get; set; }

        public string extensionTranSource { get; set; }
        public string extensionTranTo { get; set; }
    }

    public class SendMessToJTAPIModel
    {
        public string actionName { get; set; }
        public string callFrom { get; set; }
        public string callTo { get; set; }
        public string callId { get; set; }
        public string callIdTo { get; set; }
        public string transferTo { get; set; }
        public string callType { get; set; }
        public string extensionSup { get; set; }
        public string extensionEndCall { get; set; }
    }

    public class ParamCallActionSupModel
    {
        public string extensionSup { get; set; }
        public string username { get; set; }
        public string extensionAgent { get; set; }
    }

    public class CallEventData
    {
        public string CallId { get; set; }
        public string CallFrom { get; set; }
        public string CallTo { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public long EventTime { get; set; }
        public int? CauseCode { get; set; }
        public string CauseText { get; set; }
        public string CallType { get; set; }
        public string CallMedial { get; set; }
        public string ExtensionEndCall { get; set; }
        public bool? IsTranIn { get; set; }
        public string Hotline { get; set; }
        public string ExtensionSup { get; set; }
        public string queue { get; set; }
        public string transferCall { get; set; }
        public string transferTo { get; set; }
    }

    public class CallLogToDBModel
    {
        public string CallId { get; set; }
        public string CallFrom { get; set; }
        public string CallTo { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public long EventTime { get; set; }
        public int? CauseCode { get; set; }
        public string CauseText { get; set; }
        public string CallType { get; set; }
        public string CallMedial { get; set; }
        public string ExtensionEndCall { get; set; }
    }
}

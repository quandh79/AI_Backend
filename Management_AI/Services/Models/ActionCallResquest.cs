namespace Management_AI.Services.Models
{
    public class ActionCallResquest
    {

        /// <summary>
        /// extension call <CallFrom>
        /// </summary>
        public string extension { get; set; }
        /// <summary>
        /// call to phone
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// extension callTo <CallTo>
        /// </summary>
        public string extensionDest { get; set; }
        /// <summary>
        /// callId of extension
        /// </summary>
        public string dialogID { get; set; }
        /// <summary>
        /// callid of exxtensionDest
        /// </summary>
        public string callidDest { get; set; }
        /// <summary>
        /// stateID
        /// </summary>
        public string stateID { get; set; }
    }
    public class GetTokenResquest
    {
        public string userName { get; set; }
        public string extension { get; set; }
        public string accessKey { get; set; }
    }
}

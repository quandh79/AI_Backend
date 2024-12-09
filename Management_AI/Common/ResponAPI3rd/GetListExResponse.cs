using System.Collections.Generic;

namespace Management_AI.Common.ResponAPI3rd
{
    public class GetListExResponse : BaseResponse
    {
        public List<ExResponse> data { get; set; }
    }
    public class ExResponse
    {
        public string userName { get; set; }
        public string extentionID { get; set; }
    }
}

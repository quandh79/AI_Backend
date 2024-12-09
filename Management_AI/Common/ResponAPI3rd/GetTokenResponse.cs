namespace Management_AI.Common.ResponAPI3rd
{
    public class GetTokenResponse : BaseResponse
    {
        public string username { get; set; }
        public string extension { get; set; }
        public string Token { get; set; }
    }
}

using System.Security.Claims;

namespace Management_AI.Models.Main
{
    public class TokenResponse
    {
        public string username { get; set; }
        public string tenant_id { get; set; }
        public string asterisk_id { get; set; }
        public bool is_administrator { get; set; }
        public bool is_rootuser { get; set; }
        public string extension_number { get; set; }
    }
    public class GetPrincipalModel
    {
        public GetPrincipalModel(ClaimsPrincipal claimsPrincipal, string signatureAlgorithm)
        {
            this.claimsPrincipal = claimsPrincipal;
            this.signatureAlgorithm = signatureAlgorithm;
        }

        public ClaimsPrincipal claimsPrincipal { get; set; }
        public string signatureAlgorithm { get; set; }
    }
    public class ClamModel
    {
        public string user_name { get; set; }
        public string tenant_id { get; set; }
        public string asterisk_id { get; set; }
        public string is_administrator { get; set; }
        public string is_rootuser { get; set; }
        public string is_supervisor { get; set; }
        public string is_agent { get; set; }
        public string roles { get; set; }
        public string extension_number { get; set; }
    }
}


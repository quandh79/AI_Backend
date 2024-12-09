using System;

namespace Management_AI.Common.ResponAPI3rd
{
    public class StateList
    {
        public Guid id { get; set; }
        public string state_key { get; set; }
        public string state_name { get; set; }
        public string description { get; set; }
        public int position { get; set; }
    }
}

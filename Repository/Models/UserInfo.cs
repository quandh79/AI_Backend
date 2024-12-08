﻿using System;

namespace Repository.Models
{
    public class UserInfo
    {
        public string username { get; set; }
        public string fullname { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string extension_number { get; set; }
        public string description { get; set; }
        public Guid role_id { get; set; }
        public bool is_administrator { get; set; }
        public bool is_rootuser { get; set; }
        public bool is_active { get; set; }
        public string avatar { get; set; }
        public string reason_deactive { get; set; }
        public Nullable<System.DateTime> block_time { get; set; }
        public int wrong_password_number { get; set; }
        public System.DateTime create_time { get; set; }
        public string create_by { get; set; }
        public System.DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public System.Guid tenant_id { get; set; }
        public string report_to { get; set; }
    }
}

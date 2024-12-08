namespace Common
{
    public static class Constants
    {
        // DateFormat
        public static readonly string DATETIME_FORMAT_CLIENT_TO_SERVER = "yyyy-MM-dd HH:mm:ss";
        public static readonly string DATETIME_FORMAT_WEBHOOK = "yyyy-MM-dd HH:mm:ss";

        public static readonly string CONF_CROSS_ORIGIN = "CROSS_ORIGIN";
        public static readonly string CONF_API_PUBLIC_KEY = "PUBLIC_KEY";
        public static readonly string CONF_KAFKA_BOOSTRAP_SERVER = "KAFKA_BOOSTRAP_SERVER";
        public static readonly int SERVICE_CODE = 1;
        public static readonly string CONF_MAX_ERROR_MESS = "MAX_ERROR_MESS";
        public static readonly string CONF_KAFKA_GROUP_ID = "KAFKA_GROUP_ID";
        public static readonly string CONF_HOST_FABIO_SERVICE = "HOST_FABIO_SERVICE";

        public static readonly string CONF_LOCAL_HOST = "LOCAL_HOST";

        // Config service
        //public static readonly string CONF_SOURCE_USER_FABIO = "SOURCE_USER_FABIO";
        //public static readonly string CONF_SOURCE_DATA_STORAGE_FABIO = "SOURCE_DATA_STORAGE_FABIO";

        // Consul
        public static readonly string CONF_FABIO = "FABIO";

        public static readonly string CONF_ADDRESS_SERVICE = "ADDRESS_SERVICE";
        public static readonly string CONF_PORT_SERVICE = "PORT_SERVICE";
        public static readonly string CONF_SOURCE_FABIO_SERVICE = "SOURCE_FABIO_SERVICE";
        public static readonly string CONF_PROTOCOL_SERVICE = "PROTOCOL_SERVICE";
        public static readonly string CONF_STATE_SOURCE = "STATE_SOURCE";
        public static readonly string STATE_SOURCE_PRODUCTION = "production";
        public static readonly string CONF_ADDRESS_DISCOVERY_SERVICE = "ADDRESS_DISCOVERY_SERVICE";


        // Authentication
        public static readonly string CONF_API_SECRET_KEY = "API_SECRET_KEY";
        public static readonly string KEY_SESSION_TOKEN = "KEY_SESSION_TOKEN";
        public static readonly string KEY_SESSION_TENANT_ID = "KEY_SESSION_TENANT_ID";
        public static readonly string KEY_SESSION_EMAIL = "KEY_SESSION_EMAIL";
        public static readonly string KEY_SESSION_USER_ID = "KEY_SESSION_USER_ID";
        public static readonly string KEY_SESSION_EXTENSION_NUMBER = "KEY_SESSION_EXTENSION_NUMBER";
        public static readonly string KEY_SESSION_IS_ADMINISTRATOR = "KEY_SESSION_IS_ADMINISTRATOR";
        public static readonly string KEY_SESSION_IS_ROOTUSER = "KEY_SESSION_IS_ROOTUSER";

        // Parallel
        public static readonly string CONF_MAX_DEGREE_PARALLELISM = "MAX_DEGREE_PARALLELISM";
        public enum AUTHOR { TOKEN, SECRET_KEY, TOKEN_OR_KEY };

        // Agent State
        public static readonly string AGENT_STATE_READY = "ready";
        public static readonly string AGENT_STATE_NOT_READY = "notready";
        public static readonly string AGENT_STATE_CALLING = "calling";
        public static readonly string AGENT_STATE_RINGING = "ringing";
        public static readonly string AGENT_STATE_TALKING = "talking";
        public static readonly string AGENT_STATE_WRAPUP = "wrapup";

        // JTAPI
        public static readonly string CONFIG_USER_JTAPI = "JTAPI";
        public static readonly string PREFIX_REDIS_JTAPI = "JTAPI";
        public static readonly string PREFIX_REDIS_RECORDING = "RECORDING";
        public static readonly string JTAPI_CALL_TYPE_INBOUND = "InBound";
        public static readonly string JTAPI_CALL_TYPE_OUTBOUND = "OutBound";
        public static readonly string JTAPI_CALL_TYPE_TRANSFER = "Transfer";
        public static readonly string JTAPI_CALL_TYPE_INTERNAL = "Internal";


        public static readonly string JTAPI_CALL_TYPE_ROB_CALL = "RobCall";
        public static readonly string JTAPI_CALL_TYPE_WHISPER_CALL = "WhisperCall";
        public static readonly string JTAPI_CALL_TYPE_INTERNAL_CONFERENCE_CALL = "Internal_ConferenceCall";
        public static readonly string JTAPI_CALL_TYPE_CONFERENCE_CALL = "ConferenceCall";

        public static readonly string PREFIX_REDIS_CALL = "CALL";
        public static readonly string PREFIX_REDIS_CALL_2 = "CALL2";

        //Call direct
        public static readonly string INBOUND = "inbound";
        public static readonly string OUTBOUND = "outbound";
        public static readonly string TRANSFER = "transfer";
        public static readonly string INTERNAL = "internal";

        public static readonly string EventPushJTAPIMessage = "PushJTAPIMessage";
        public static readonly string SupJoinConferenceCall = "SupJoinConferenceCall";
        public static readonly string PushClientRobCall = "PushClientRobCall";
        public static readonly string PushClientRegisterRecordingSuccess = "RegisterRecordingSuccess";
        // action call
        public static readonly string FUN_JTAPI_REGISTER_PHONE = "ResgiterPhone";
        public static readonly string FUN_JTAPI_UNREGISTER_PHONE = "UnResgiterPhone";
        public static readonly string FUN_JTAPI_MAKECALL = "MakeCall";
        public static readonly string FUN_JTAPI_ANSWERCALL = "AnswerCall";
        public static readonly string FUN_JTAPI_DROPCALL = "DropCall";
        public static readonly string FUN_JTAPI_HOLD_CALL = "HoldCall";
        public static readonly string FUN_JTAPI_UNHOLD_CALL = "UnHoldCall";
        public static readonly string FUN_JTAPI_TRANSDER_DIRECT = "TransferDirect";
        public static readonly string FUN_JTAPI_TRANSDER_WARM = "TransferWarm";
        public static readonly string FUN_JTAPI_COMPLETE_TRANSDER_WARM = "CompleteTransferWarm";
        public static readonly string FUN_JTAPI_WHISPER_CALL = "WhisperCall";
        public static readonly string FUN_JTAPI_ROB_CALL = "Robcall";
        public static readonly string FUN_JTAPI_CONFERENCE_CALL = "ConferenceCall";


        // Call status

        public static readonly string INIT = "init";
        public static readonly string INIVR = "inivr";
        public static readonly string START = "start";
        public static readonly string INQUEUE = "inqueue";
        public static readonly string RINGING = "ringing";
        public static readonly string ANSWER = "answer";
        public static readonly string HOLD = "hold";
        public static readonly string UNHOLD = "unhold";
        public static readonly string BLIND_TRANSFER = "blind_transfer";
        public static readonly string ATTENDED_TRANSFER = "attended_transfer";
        public static readonly string NO_ANSWER = "no_answer";
        public static readonly string HANGUP = "hangup";
        public static readonly string SUCCESS = "success";


        // key page param
        public static readonly string SORT_ASC = "ASC";
        public static readonly string SORT_DESC = "DESC";

        // Message
        public static readonly string MES_CALL_NOT_ACTIVE = "Cuộc gọi không tồn tại hoặc đã kết thúc!";
        public static readonly string MES_JTAPI_NOT_ACTIVE = "Service JTAPI không hoạt động!";

        // Message error 
        public static readonly string TOKEN_EXPIRE = "Token expire or not exist.";
        public static readonly string EXTENSION_NOT_ONLINE = "Extension not online. Please login by this extension";
        public static readonly string EXTENSION_IS_BUSY = "Extension is busy. Please try again.";
        public static readonly string UNKNOWN = "Unknown error";
        // BaseSR_claims
        public static readonly string BASESR_CLAIMS = "basesr_claims";

        // Hashtag Redis
        public static readonly string HASHTAG_CONNECTION = "connection";

        public static readonly string EMAIL_IS_ALREADY_EXISTS = "Email is already exist!!";
        public static readonly string TENANT_NOT_FOUND = "Tenant not found";

        #region cache
        public static readonly double CACHE_EXPIRATION_MINUTES = 10;
        public static readonly string CACHE_BCC01_USER = "BCC01_USER";
        public static readonly string CACHE_BCC01_STATELIST = "BCC01_STATELIST";
        public static readonly string CACHE_BCC01_COMMONSETTING = "BCC01_COMMONSETTING";
        #endregion

        public static readonly string RECORDING_USER_TYPE_WIN = "mode_windows_auth";
        public static readonly string RECORDING_USER_TYPE_C247 = "mode_c247_auth";
        public static readonly string RECORDING_USER_TYPE_EXTENSION = "mode_extension";

        #region hashtype
        public static readonly string HASH_SHA512 = "SHA512";
        public static readonly string HASH_SHA384 = "SHA384";
        public static readonly string HASH_SHA256 = "SHA256";
        #endregion

        #region default role
        public static readonly string DEFAULT_ROLE_DIRECTORS = "Directors";
        public static readonly string DEFAULT_ROLE_SUPERVISOR = "Supervisor";
        public static readonly string DEFAULT_ROLE_AGENT = "Agent";
        public static readonly string DEFAULT_ROLE_ADMIN = "Admin";
        #endregion

        #region default profile
        public static readonly string DEFAULT_PROFILE_DIRECTORS = "Directors";
        public static readonly string DEFAULT_PROFILE_SUPERVISOR = "Supervisor";
        public static readonly string DEFAULT_PROFILE_AGENT = "Agent";
        public static readonly string DEFAULT_PROFILE_ADMIN = "Admin";
        #endregion

        #region root_id
        public static readonly string ROOT_ROLE = "7F126686-0853-4EA3-B9CD-4E8F7D34A77C";
        #endregion

        #region root permission object
        public static readonly string[] ROOT_PERMISSIONS = { "Tenants", "Default", "Default Setting", "Upload File", "Setting Channel", "Root Log", "Report Category", "Report Config", "Dashboard Config" };
        #endregion

        #region agent permission object
        public static readonly string[] AGENT_PERMISSIONS = { "Message Template", "Interaction Customer", "Interaction Customer Extension", "Interaction", "Interaction Detail", "Interaction Member", "Interaction Feedback", "Interaction Note", "Interaction History Notifycation", "Ticket", "Ticket Note", "Ticket File Upload", "Dynamic Report", "Group Report" };
        #endregion

        #region module object
        public static readonly string[] MODULE_OBJECTS = { "User Management", "General Setting" };
        #endregion

        #region status
        public static readonly string ONLINE = "online";
        public static readonly string OFFLINE = "offline";
        public static readonly string BUSY = "busy";
        public static readonly string NOT_READY = "notready";
        public static readonly string AVAILABLE = "available";
        #endregion
        #region common type common setting
        public static string COMMON_TYPE_STRING = "string";
        public static string COMMON_TYPE_NUMBER = "number";
        public static string COMMON_TYPE_DATA = "date";
        public static string COMMON_TYPE_DATETIME = "datetime";
        public static string COMMON_TYPE_BOOLEAN = "boolean";
        public static string COMMON_TYPE_COLOR = "color";
        public static string COMMON_TYPE_PASSWORD = "password";

        public static string COMMON_KEY_LDAP_HOST = "ldap_host";
        public static string COMMON_KEY_LDAP_USERNAME = "ldap_username";
        public static string COMMON_KEY_LDAP_PORT = "ldap_port";
        public static string COMMON_KEY_LDAP_PASSWORD = "ldap_password";
        public static string COMMON_KEY_LDAP_DN = "ldap_dn";
        public static string COMMON_KEY_LDAP_USE = "ldap_use";
        #endregion
        // Hashtag Redis
        public static readonly string HASHTAG_WEBHOOK = "url_webhook";
        public static readonly string HASHTAG_WEBHOOK_MISSCALL = "misscall_webhook";
        public static readonly string HASHTAG_WEBHOOK_LCM_URL = "url_lcm_webhook";
        public static readonly string HASHTAG_WEBHOOK_LCM_TOKEN = "token_lcm_webhook";
        public static readonly string HASHTAG_CACHE_DB = "cache";


        public static readonly string KEY_COMMON_SETTING_WEBHOOK_URL = "url_webhook_event_call";
        public static readonly string KEY_COMMON_SETTING_WEBHOOK_MISSCALL = "push_event_misscall";
        public static readonly string KEY_COMMON_SETTING_WEBHOOK_LCM_URL = "url_lcm_webhook_event_call";
        public static readonly string KEY_COMMON_SETTING_WEBHOOK_LCM_TOKEN = "token_lcm_webhook_event_call";


        public static readonly string EMPLOYEE_CREATION_LIMIT = "Employee creation limit.";
        public static readonly string USER_NOT_FOUND = "User not found.";
        public static readonly string ROLE_NOT_FOUND = "Role not found";
        public static readonly string REMOVE_DATA_FAILED = "Remove data failed.";
        public static readonly string DATA_INVALID = "Data invalid";
        public static readonly string LOGIN_FAILED = "Invalid login information.";
        public static readonly string SECRET_KEY_ERROR = "secret key fail";

        public static readonly string MAX_TIME_FOR_TYPE_WRONG_PASSWORD = "max_time_for_type_wrong_password";
        public static readonly string NUMBER_HISTORY_PASSWORD_VALIDATE = "number_history_password_validate";
        public static readonly string TIME_BLOCK_BY_TYPE_WRONG_PASSWORD = "time_block_by_type_wrong_password ";
        public static readonly string BLOCK_BY_INPUT_WRONG_PASS = "block_by_input_wrong_pass";
        public static readonly string KEY_SESSION_ASTERISK_ID = "KEY_SESSION_ASTERISK_ID";
        public static readonly string KEY_SESSION_IS_ADMIN = "KEY_SESSION_IS_ADMIN";
        public static readonly int INT_LONG_TIME_NOT_CHANGE_PASSWORD = 22;
        public static readonly string LONG_TIME_NOT_CHANGE_PASSWORD = "long_time_not_change_password";
        public static readonly int INT_BLOCK_BY_INPUT_WRONG_PASS = 23;
        public static readonly string ACCOUNT_LOCKED = "Account locked.";
        public static readonly string CONF_LINK_FORGOT_PASSWORD_IC = "LINK_FORGOT_PASSWORD_IC";
        public static readonly string TWO_FA_CONFIG_CODE_VERIFY_NOT_FOUND = "Code verify not found or expired";
        public static readonly string KEY_SESSION_IS_ROOT = "KEY_SESSION_IS_ROOT";
        public static readonly string PROFILE_ADMIN = "Admin";
        public static readonly string ROOT = "root";
        public static readonly string ADMIN = "admin";
        public static readonly string SYSTEM = "system";
        public static readonly string COMMON_SETTING_NOT_FOUND = "Common setting not found.";
        public static readonly string SETTING_KEY_ALREADY_EXISTS = "Setting key already exists.";
        public static readonly string KEY_SESSION_IS_SUPERVISOR = "KEY_SESSION_IS_SUPERVISOR";
        public static readonly string USERNAME_OR_EXTENSION_NUMBER_IS_EXISTS = "Username or extension number is already exists.";
        public static readonly string ACCESS_DENIED = "Access denied.";

    }
}

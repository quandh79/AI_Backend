using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class MessCodes
    {
        public static readonly int ACCOUNT_LOCKED = 1;
        public static readonly int SUCCESS = 200;
        public static readonly int UNKNOWN_ERROR = 410;
        public static readonly int TOKEN_EXPIRE_OR_NOT_EXIST = 411;
        public static readonly int EXTENSION_NOT_EXIST = 412;
        public static readonly int EXTENSION_NOT_ONLINE = 413;
        public static readonly int EXTENSION_IS_BUSY = 414;

        public static readonly int READY = 0;
        public static readonly int NOT_READY = 10002;
        public static readonly int TALKING = 10003;
        public static readonly int WRAPUP = 10004;
        public static readonly int RINGING = 10005;
        public static readonly int CALLING = 10006;
        public static readonly int EMAIL_IS_EXISTS = 8;
        public static readonly int CREATE_TENANT_FAILED = 78;
        public static readonly int DATA_NOT_FOUND = 32;
        public static readonly int EMPLOYEE_CREATION_LIMIT = 21;
        public static readonly int USERNAME_IS_EXISTS = 11;
        public static readonly int DATA_INVALID = 62;
        public static readonly int LOGIN_FAILED = 208;
        public static readonly int MAX_WRONG_PASSWORD_NUMBER = 17;
        public static readonly int LOGOUT_FAIL = 6;
        public static readonly int YOU_HAVE_SUMITTED_REQUEST = 14;
        public static readonly int EMAIL_DOES_NOT_EXISTS = 12;
        public static readonly int PUBLIC_KEY_HAS_EXPIRED = 13;
        public static readonly int KEY_INVALID = 4;
        public static readonly int VERIFY_FAILED = 210;
        public static readonly int MODULE_IS_EXISTS = 10;
        public static readonly int SETTING_KEY_ALREADY_EXISTS = 79;

    }
}

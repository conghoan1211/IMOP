namespace zaloclone_test.Helper
{
    public static class ConstMessage
    {
        public static readonly string ACCOUNT_UNVERIFIED = "Tài khoản chưa được xác thực.";
        public static readonly string EMAIL_EXISTED = "Email này đã tồn tại.";


    }


    public enum UserStatus
    {
        Inactive = 0, // Người dùng không hoạt động
        Active,   // Người dùng đang hoạt động
        Banned,   // Người dùng bị cấm
        Suspended // Người dùng bị đình chỉ
    }

    public enum Role
    {
        User = 0,
        Admin,
    }

    public enum StatusMessage
    {
        NotSent = 0,
        Sent,
        Recieved,
        Read,
    }

    public enum Gender
    {
        Male = 0,
        Female,
        Other,
    }

}

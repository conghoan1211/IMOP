namespace zaloclone_test.Helper
{
    public static class Constant
    {
        public static readonly string UrlImagePath = "wwwroot/img";
        public static readonly IList<string> IMAGE_EXTENDS = new List<string> { ".png", ".jpg", ".jpeg", ".svg" }.AsReadOnly();

    }

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

    public enum MessageStatus
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

    public enum PostPrivacy
    {
        Public = 0,
        Friend ,
        Private,
    }

    public enum FriendStatus
    {
        Pending = 0,
        Accepted,
        Blocked,
    }

}

namespace zaloclone_test.ViewModels
{
    public class InvitationVM
    {
        public string? UserID { get; set; }
        public string? UserName { get; set; }  
        public string? Avatar { get; set; } = string.Empty;
        public DateTime? CreateAt { get; set; }
        public int? Status { get; set; }
    }
}

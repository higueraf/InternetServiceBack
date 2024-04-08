namespace InternetServiceBack.Models
{
    public class User
    {
        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid? RolID { get; set; }
        public DateTime? CreationDate { get; set; }
        public Guid? UserApproval { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public Guid? UserStatusID { get; set; }

    }
}

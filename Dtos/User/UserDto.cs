namespace InternetServiceBack.Dtos.User
{
    public class UserDto
    {
        public Guid userID { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime? creationDate { get; set; }
        public Guid? userApproval { get; set; }
        public DateTime? approvalDate { get; set; }
        public Guid? userStatusID { get; set; }
    }


}

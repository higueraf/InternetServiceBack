namespace InternetServiceBack.Models
{
    public class Client
    {
        public Guid ClientID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Identification { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ReferenceAddress { get; set; }

    }
}

namespace InternetServiceBack.Models
{
    public class Payment
    {
        public Guid PaymentID { get; set; }
        public DateTime PaymentDate { get; set; }
        public Guid ClientID { get; set; }
    }
}

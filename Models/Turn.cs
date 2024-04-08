namespace InternetServiceBack.Models
{
    public class Turn
    {
        public Guid TurnID { get; set; }
        public string Description { get; set; }
        public DateTime? TurnDate { get; set; }
        public Guid? CashID { get; set; }
        public Guid? UserID { get; set; }
    }
}

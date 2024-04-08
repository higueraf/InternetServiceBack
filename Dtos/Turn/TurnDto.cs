namespace InternetServiceBack.Dtos.Turn
{
    public class TurnDto
    {
        public Guid TurnID { get; set; }
        public string Description { get; set; }
        public DateTime? TurnDate { get; set; }
        public Guid? CashID { get; set; }
        public Guid? UserID { get; set; }
    }


}

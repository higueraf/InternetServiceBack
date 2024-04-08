namespace InternetServiceBack.Models
{
    public class Attention
    {
        public Guid AttentionID { get; set; }
        public Guid TurnID { get; set; }
        public Guid ClientID { get; set; }
        public Guid AttentionStatusID { get; set; }

        public virtual Turn Turn{ get; set; } = null!;

    }
}

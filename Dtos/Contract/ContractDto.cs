namespace InternetServiceBack.Dtos.Contract
{
    public class ContractDto
    {
        public Guid ContractID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndtDate { get; set; }
        public Guid ServiceID { get; set; }
        public Guid ContractStatusID { get; set; }
        public Guid ClientID { get; set; }
        public Guid MethodPaymentID { get; set; }

    }
}

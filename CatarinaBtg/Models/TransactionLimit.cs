namespace CatarinaBtg.Models{
    public class TransactionLimit
    {
        public string CPF { get; set; }
        public string AgencyNumber { get; set; }
        public string AccountNumber { get; set; }
        public decimal PIXLimit { get; set; }
    }
}

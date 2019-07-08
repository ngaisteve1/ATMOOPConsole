namespace ATM.Domain.Entities
{
    public class UserBankAccount
    {
        public long Id { get; set; }
        private long cardNumber;
        public long CardNumber { get; set; }
        
        public long CardPin { get; set; }

        public string FullName { get; set; }

        public long AccountNumber { get; set; }
        public decimal AccountBalance { get; set; }

        public int TotalLogin { get; set; }

        public bool IsLocked { get; set; }
    }
}

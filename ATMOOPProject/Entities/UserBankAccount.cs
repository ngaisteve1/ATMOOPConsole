namespace ATMOOPProject.Entities
{
    public class UserBankAccount
    {
        public long Id { get; set; }
        private long cardNumber;
        public long CardNumber
        {
            get
            {
                return cardNumber;
            }
            set
            {
                // if (value.ToString().Length < 5)
                //     throw new ArgumentException("Card number cannot be less than 5 digit.");
                // Console.ReadKey();

                cardNumber = value;
            }
        }
        public long CardPin { get; set; }

        public string FullName { get; set; }

        public long AccountNumber { get; set; }
        public decimal AccountBalance { get; set; }

        public int TotalLogin { get; set; }

        public bool IsLocked { get; set; }
    }
}

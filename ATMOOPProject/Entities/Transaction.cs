using ATMOOPProject.Enum;
using ATMOOPProject.StaticClass;
using System;

namespace ATMOOPProject.Entities
{
    public class Transaction
    {
        public long TransactionId { get; private set; }

        public long UserBankAccountId { get; set; }

        public DateTime TransactionDate { get; set; }

        public TransactionType TransactionType { get; set; }

        public string Description { get; set; }

        public decimal TransactionAmount { get; set; }

        public Transaction()
        {
            TransactionId = Utility.GetTransactionId();
        }
    }

}

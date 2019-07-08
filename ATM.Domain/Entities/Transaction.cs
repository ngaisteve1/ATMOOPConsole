using ATM.Domain.Enum;
using System;

namespace ATM.Domain.Entities
{
    public class Transaction
    {
        public long TransactionId { get; set; }

        public long UserBankAccountId { get; set; }

        public DateTime TransactionDate { get; set; }

        public TransactionType TransactionType { get; set; }

        public string Description { get; set; }

        public decimal TransactionAmount { get; set; }

        // Moved to ATMApp to removed dependency.
        //public Transaction()
        //{
        //    TransactionId = Utility.GetTransactionId();
        //}
    }

}

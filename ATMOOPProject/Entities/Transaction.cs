using ATMOOPProject.Enum;
using System;

namespace ATMOOPProject.Entities
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        public DateTime TransactionDate { get; set; }

        public TransactionType TransactionType { get; set; }

        public string Description { get; set; }

        public decimal TransactionAmount { get; set; }
    }
}

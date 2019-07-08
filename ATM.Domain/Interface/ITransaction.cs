using ATM.Domain.Entities;
using ATM.Domain.Enum;

namespace ATM.Domain.Interface
{
    public interface ITransaction
    {
        void InsertTransaction(long _UserBankAccountId, TransactionType _tranType, decimal _tranAmount, string _desc);
        void ViewTransaction();
    }
}

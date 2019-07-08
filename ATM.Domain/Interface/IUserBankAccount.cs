using System;
using System.Collections.Generic;
using System.Text;

namespace ATM.Domain.Interface
{
    public interface IUserBankAccount
    {
        void CheckBalance();
        void PlaceDeposit();
        void MakeWithdrawal();
    }
}

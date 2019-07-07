using System;
using System.Collections.Generic;
using System.Text;

namespace ATMOOPProject.Interface
{
    public interface IUserBankAccount
    {
        void CheckBalance();
        void PlaceDeposit();
        void MakeWithdrawal();
    }
}

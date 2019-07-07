using ATMOOPProject.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace ATMOOPProject.Interface
{
    public interface ITransaction
    {
        void InsertTransaction(Transaction transaction);
        void ViewTransaction();
    }
}

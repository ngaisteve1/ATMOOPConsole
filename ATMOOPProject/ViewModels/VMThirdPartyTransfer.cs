using System;
using System.Collections.Generic;
using System.Text;

namespace ATMOOPProject.ViewModels
{
    public class VMThirdPartyTransfer
    {
        public decimal TransferAmount { get; set; }
        public long RecipientBankAccountNumber { get; set; }

        public string RecipientBankAccountName { get; set; }
    }
}

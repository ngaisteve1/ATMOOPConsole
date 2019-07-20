using ATM.ConsoleApplication.ViewModels;
using ATM.ConsoleApplicationLib;
using ATM.Domain.Entities;
using ATM.Domain.Enum;
using ATM.Domain.Interface;
using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ATM.ConsoleApplication
{
    public class AtmApp : IATMApp, ITransaction, IUserBankAccount
    {
        // This class in charge of main application where by Initialization and Execute 
        // method will be the only methods to be called when client code run this application.

        // This list is used in replace of database in this version.
        private List<UserBankAccount> _accountList;
        private UserBankAccount selectedAccount;
        private const decimal minimum_kept_amt = 20;
        private List<Transaction> _listOfTransactions;
        private readonly AtmScreen screen;

        public AtmApp()
        {
            screen = new AtmScreen();
        }

        public void Initialization()
        {
            _accountList = new List<UserBankAccount>
            {
                new UserBankAccount() { Id=1, FullName = "Peter Parker", AccountNumber=333111, CardNumber = 123123, CardPin = 111111, AccountBalance = 2000.00m, IsLocked = false },
                new UserBankAccount() { Id=2, FullName = "Bruce Bane", AccountNumber=111222, CardNumber = 456456, CardPin = 222222, AccountBalance = 1500.30m, IsLocked = true },
                new UserBankAccount() { Id=3, FullName = "Clark Kent", AccountNumber=888555, CardNumber = 789789, CardPin = 333333, AccountBalance = 2900.12m, IsLocked = false }
            };

            _listOfTransactions = new List<Transaction>();
        }

        public void Execute()
        {
            AtmScreen.WelcomeATM();

            CheckCardNoPassword();
            AtmScreen.WelcomeCustomer(selectedAccount.FullName);

            while (true)
            {
                AtmScreen.DisplaySecureMenu();
                ProcessMenuOption();
            }
        }

        public void CheckCardNoPassword()
        {
            bool isLoginPassed = false;

            while (isLoginPassed == false)
            {
                var inputAccount = screen.LoginForm();

                AtmScreen.LoginProgress();

                foreach (UserBankAccount account in _accountList)
                {
                    selectedAccount = account;
                    if (inputAccount.CardNumber.Equals(account.CardNumber))
                    {
                        selectedAccount.TotalLogin++;

                        if (inputAccount.CardPin.Equals(account.CardPin))
                        {
                            selectedAccount = account;
                            if (selectedAccount.IsLocked)
                            {
                                // This is when database is used and when the app is restarted.
                                // Even user login with the correct card number and pin,
                                // If IsLocked status is locked, user still will be still blocked.
                                AtmScreen.PrintLockAccount();
                            }
                            else
                            {
                                selectedAccount.TotalLogin = 0;
                                isLoginPassed = true;
                                break;
                            }
                        }
                    }
                }

                if (isLoginPassed == false)
                {
                    Utility.PrintMessage("Invalid card number or PIN.", false);

                    // Lock the account if user fail to login more than 3 times.
                    selectedAccount.IsLocked = selectedAccount.TotalLogin == 3;
                    if (selectedAccount.IsLocked)
                        AtmScreen.PrintLockAccount();
                }

                Console.Clear();
            }
        }

        private void ProcessMenuOption()
        {
            switch (Validator.Convert<int>("your option"))
            {
                case (int)SecureMenu.CheckBalance:
                    CheckBalance();
                    break;
                case (int)SecureMenu.PlaceDeposit:
                    PlaceDeposit();
                    break;
                case (int)SecureMenu.MakeWithdrawal:
                    MakeWithdrawal();
                    break;
                case (int)SecureMenu.ThirdPartyTransfer:
                    
                    var vMThirdPartyTransfer = screen.ThirdPartyTransferForm();
                    PerformThirdPartyTransfer(vMThirdPartyTransfer);
                    break;
                case (int)SecureMenu.ViewTransaction:
                    ViewTransaction();
                    break;

                case (int)SecureMenu.Logout:
                    AtmScreen.LogoutProgress();
                    Utility.PrintConsoleWriteLine("You have succesfully logout. Please collect your ATM card.");
                    ClearSession();
                    Execute();
                    break;
                default:
                    Utility.PrintMessage("Invalid Option Entered.", false);

                    break;
            }
        }

        public void CheckBalance()
        {
            AtmScreen.PrintCheckBalanceScreen();
            Utility.PrintConsoleWriteLine(Utility.FormatAmount(selectedAccount.AccountBalance), false);
        }

        public void PlaceDeposit()
        {
            // Note: Actual ATM system will just let you
            // place bank notes into physical ATM machine.

            Utility.PrintConsoleWriteLine("\nNote: Actual ATM system will just\nlet you " +
            "place bank notes into physical ATM machine. \n");

            var transaction_amt = Validator.Convert<int>($"amount {AtmScreen.cur}");

            Utility.PrintUserInputLabel("\nCheck and counting bank notes.");
            Utility.printDotAnimation();
            Console.SetCursorPosition(0, Console.CursorTop-3);
            Console.WriteLine("");

            // Guard clause
            if (transaction_amt <= 0)
            {
                Utility.PrintMessage("Amount needs to be more than zero. Try again.", false);
                return;
            }

            if (transaction_amt % 10 != 0)
            {
                Utility.PrintMessage($"Key in the deposit amount only with multiply of 10. Try again.", false);
                return;
            }

            if (PreviewBankNotesCount(transaction_amt) == false)
            {
                Utility.PrintMessage($"You have cancelled your action.", false);
                return;
            }

            // Bind transaction_amt to Transaction object
            // Add transaction record - Start            
            InsertTransaction(selectedAccount.Id, TransactionType.Deposit, +
                transaction_amt, "");
            // Add transaction record - End

            // Another method to update account balance.
            selectedAccount.AccountBalance = selectedAccount.AccountBalance + transaction_amt;

            Utility.PrintMessage($"You have successfully deposited {Utility.FormatAmount(transaction_amt)}. " +
                "Please collect the bank slip. ", true);
        }

        public void MakeWithdrawal()
        {
            Console.WriteLine("\nNote: For GUI or actual ATM system, user can ");
            Console.Write("choose some default withdrawal amount or custom amount. \n\n");

            var transaction_amt = Validator.Convert<int>($"amount {AtmScreen.cur}");

            // Input data validation - Start
            if (transaction_amt <= 0)
            {
                Utility.PrintMessage("Amount needs to be more than zero. Try again.", false);
                return;
            }

            if (transaction_amt % 10 != 0)
            {
                Utility.PrintMessage($"Key in the deposit amount only with multiply of 10. Try again.", false);
                return;
            }
            // Input data validation - End


            // Business rules validation - Start
            if (transaction_amt > selectedAccount.AccountBalance)
            {
                Utility.PrintMessage($"Withdrawal failed. You do not have enough fund to withdraw {Utility.FormatAmount(transaction_amt)}", false);
                return;
            }

            if ((selectedAccount.AccountBalance - transaction_amt) < minimum_kept_amt)
            {
                Utility.PrintMessage($"Withdrawal failed. Your account needs to have minimum {Utility.FormatAmount(minimum_kept_amt)}", false);
                return;
            }
            // Business rules validation - End


            // Bind transaction_amt to Transaction object
            // Add transaction record - Start
            InsertTransaction(selectedAccount.Id, TransactionType.Withdrawal, +
                -transaction_amt,"");
            // Add transaction record - End

            // Another method to update account balance.
            selectedAccount.AccountBalance = selectedAccount.AccountBalance - transaction_amt;

            Utility.PrintMessage("Please collect your money. You have successfully withdraw " +
                $"{Utility.FormatAmount(transaction_amt)}. Please collect your bank slip.", true);

        }

        public void PerformThirdPartyTransfer(VMThirdPartyTransfer vMThirdPartyTransfer)
        {
            if (vMThirdPartyTransfer.TransferAmount <= 0)
            {
                Utility.PrintMessage("Amount needs to be more than zero. Try again.", false);
                return;
            }

            // Check giver's account balance - Start
            if (vMThirdPartyTransfer.TransferAmount > selectedAccount.AccountBalance)
            {
                Utility.PrintMessage("Withdrawal failed. You do not have enough " +
                    $"fund to withdraw {Utility.FormatAmount(vMThirdPartyTransfer.TransferAmount)}", false);
                return;
            }

            if (selectedAccount.AccountBalance - vMThirdPartyTransfer.TransferAmount < minimum_kept_amt)
            {
                Utility.PrintMessage($"Withdrawal failed. Your account needs to have " +
                    $"minimum {Utility.FormatAmount(minimum_kept_amt)}", false);
                return;
            }
            // Check giver's account balance - End

            // Check if receiver's bank account number is valid.
            var selectedBankAccountReceiver = (from b in _accountList
                                               where b.AccountNumber == vMThirdPartyTransfer.RecipientBankAccountNumber
                                               select b).FirstOrDefault();

            if (selectedBankAccountReceiver == null)
            {
                Utility.PrintMessage($"Third party transfer failed. Receiver bank account number is invalid.", false);
                return;
            }

            if (selectedBankAccountReceiver.FullName != vMThirdPartyTransfer.RecipientBankAccountName)
            {
                Utility.PrintMessage($"Third party transfer failed. Recipient's account name does not match.", false);
                return;
            }

            // Bind transaction_amt to Transaction object
            // Add transaction record (Giver) - Start            
            InsertTransaction(selectedAccount.Id, TransactionType.ThirdPartyTransfer, + 
                -vMThirdPartyTransfer.TransferAmount, "Transfered " + 
                $" to {selectedBankAccountReceiver.AccountNumber} ({selectedBankAccountReceiver.FullName})");
            // Add transaction record (Giver) - End

            // Update balance amount (Giver)
            selectedAccount.AccountBalance = selectedAccount.AccountBalance - vMThirdPartyTransfer.TransferAmount;

            // Add transaction record (Receiver) - Start
            InsertTransaction(selectedBankAccountReceiver.Id, TransactionType.ThirdPartyTransfer, +
                vMThirdPartyTransfer.TransferAmount, "Transfered " +
                $" from {selectedAccount.AccountNumber} ({selectedAccount.FullName})");
            // Add transaction record (Receiver) - End

            // Update balance amount (Receiver)
            selectedBankAccountReceiver.AccountBalance = selectedBankAccountReceiver.AccountBalance + vMThirdPartyTransfer.TransferAmount;

            Utility.PrintMessage("You have successfully transferred out " + 
                $" {Utility.FormatAmount(vMThirdPartyTransfer.TransferAmount)} to {vMThirdPartyTransfer.RecipientBankAccountName}", true);
        }

        private bool PreviewBankNotesCount(decimal amount)
        {
            int hundredNotesCount = (int)amount / 100;
            int fiftyNotesCount = ((int)amount % 100) / 50;
            int tenNotesCount = ((int)amount % 50) / 10;

            Utility.PrintUserInputLabel("\nSummary                                                  ",true);
            Utility.PrintUserInputLabel("-------", true);
            Utility.PrintUserInputLabel($"{AtmScreen.cur} 100 x {hundredNotesCount} = {100 * hundredNotesCount}", true);
            Utility.PrintUserInputLabel($"{AtmScreen.cur} 50 x {fiftyNotesCount} = {50 * fiftyNotesCount}", true);
            Utility.PrintUserInputLabel($"{AtmScreen.cur} 10 x {tenNotesCount} = {10 * tenNotesCount}", true);
            Utility.PrintUserInputLabel($"Total amount: {Utility.FormatAmount(amount)}\n\n", true);

            char opt = Validator.Convert<char>("1 to confirm");
            return opt.Equals('1');
        }

        public void ViewTransaction()
        {
            // Filter transaction list
            var filteredTransactionList = _listOfTransactions.Where(t => t.UserBankAccountId == selectedAccount.Id).ToList();

            if (filteredTransactionList.Count <= 0)
                Utility.PrintMessage($"There is no transaction yet.", true);
            else
            {
                var table = new ConsoleTable("Id","Transaction Date", "Type", "Descriptions", "Amount " + AtmScreen.cur);

                foreach (var tran in filteredTransactionList)
                {
                    table.AddRow(tran.TransactionId, tran.TransactionDate, tran.TransactionType, tran.Description, tran.TransactionAmount);
                }
                table.Options.EnableCount = false;
                table.Write();
                Utility.PrintMessage($"You have {filteredTransactionList.Count} transaction(s).", true);
            }
        }

        public void InsertTransaction(long _UserBankAccountId, TransactionType _tranType, decimal _tranAmount, string _desc)
        {
            var transaction = new Transaction()
            {
                TransactionId = Utility.GetTransactionId(),
                UserBankAccountId = _UserBankAccountId,
                TransactionDate = DateTime.Now,
                TransactionType = _tranType,
                TransactionAmount = _tranAmount,
                Description = _desc
            };

            _listOfTransactions.Add(transaction);
        }

        private void ClearSession()
        {
            // No session is used in this version.
        }

    }
}

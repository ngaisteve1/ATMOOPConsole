using ATM.ConsoleApplication.ViewModels;
using ATM.ConsoleApplicationLib;
using ATM.Domain.Entities;
using System;

namespace ATM.ConsoleApplication
{
    internal class AtmScreen
    {
        // This class in charge of printing out text in user interface.

        internal const string cur = "RM ";

        internal static void WelcomeATM()
        {
            Console.Clear();
            Console.Title = "Meybank ATM System.";
            Console.WriteLine("Welcome to Meybank ATM.\n");
            Console.WriteLine("Please insert your ATM card.");
            Utility.PrintEnterMessage();
        }

        internal static void WelcomeCustomer(string fullName)
        {            
            Utility.PrintConsoleWriteLine("Welcome back, " + fullName);
        }


        internal static void PrintLockAccount()
        {
            Console.Clear();
            Utility.PrintMessage("Your account is locked. Please go to " +
                "the nearest branch to unlocked your account. Thank you.", true);

            Utility.PrintEnterMessage();
            Environment.Exit(1);
        }

        internal UserBankAccount LoginForm()
        {
            var vmUserBankAccount = new UserBankAccount();

            // Actual ATM system will accept and validate physical ATM card.
            // Card validation includes read card number and check bank account status
            // and other security checking.

            vmUserBankAccount.CardNumber = Validator.Convert<long>("card number");
                     
            vmUserBankAccount.CardPin = Convert.ToInt32(Utility.GetHiddenConsoleInput("Enter card pin: "));          

            return vmUserBankAccount;
        }

        internal static void LoginProgress()
        {
            Console.Write("\nChecking card number and card pin.");
            Utility.printDotAnimation();
            Console.Clear();
        }

        internal static void LogoutProgress()
        {
            Console.WriteLine("Thank you for using Meybank ATM system.");
            Utility.printDotAnimation();
            Console.Clear();
        }


        internal static void DisplaySecureMenu()
        {
            Console.Clear();
            Console.WriteLine(" ---------------------------");
            Console.WriteLine("| Meybank ATM Secure Menu    |");
            Console.WriteLine("|                            |");
            Console.WriteLine("| 1. Balance Enquiry         |");
            Console.WriteLine("| 2. Cash Deposit            |");
            Console.WriteLine("| 3. Withdrawal              |");
            Console.WriteLine("| 4. Third Party Transfer    |");
            Console.WriteLine("| 5. Transactions            |");
            Console.WriteLine("| 6. Logout                  |");
            Console.WriteLine("|                            |");
            Console.WriteLine(" ---------------------------");

            // The menu selection is tied to Enum:SecureMenu.
        }

        internal static void PrintCheckBalanceScreen()
        {
            Console.Write("Account balance amount: ");
        }
        
        // This is the only non-static method.
        // Reason is this method needs to return an object.
        // ToDo: Find other way to solve this design issue.
        internal VMThirdPartyTransfer ThirdPartyTransferForm()
        {
            var vMThirdPartyTransfer = new VMThirdPartyTransfer();

            //vMThirdPartyTransfer.RecipientBankAccountNumber = Validator.GetValidIntInputAmt("recipient's account number");
            vMThirdPartyTransfer.RecipientBankAccountNumber = Validator.Convert<long>("recipient's account number");

            //vMThirdPartyTransfer.TransferAmount = Validator.GetValidDecimalInputAmt($"amount {AtmScreen.cur}");            
            vMThirdPartyTransfer.TransferAmount = Validator.Convert<decimal>($"amount {cur}");

            vMThirdPartyTransfer.RecipientBankAccountName = Utility.GetRawInput("recipient's account name");
            // no validation here yet.

            return vMThirdPartyTransfer;
        }

        // moved to Utility class. See remarks there.
        //internal static void PrintEnterMessage()
        //{
        //    Console.WriteLine("\nPress enter to continue.");
        //    Console.ReadKey();
        //}
    }
}
